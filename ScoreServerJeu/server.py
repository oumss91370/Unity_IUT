import socket
import threading
import sqlite3

# Connect to the SQLite database
conn = sqlite3.connect('scores.db', check_same_thread=False)
cursor = conn.cursor()

# Create the scores table if it doesn't exist
cursor.execute('''CREATE TABLE IF NOT EXISTS scores
                  (id INTEGER PRIMARY KEY, name TEXT, score INTEGER)''')
conn.commit()

# Function to handle client connections
def handle_client(client_socket):
    try:
        # Receive data from the client
        data = client_socket.recv(1024).decode('utf-8').strip()
        if data:
            # Check if the command is to get scores
            if data == "GET_SCORES":
                print("Received request to get scores")
                # Retrieve the top 10 scores
                cursor.execute('SELECT name, score FROM scores ORDER BY score DESC LIMIT 10')
                top_scores = cursor.fetchall()
                print(f"Top scores: {top_scores}")  # Debug print
                # Build the response
                response = "\n".join([f"{s[0]}:{s[1]}" for s in top_scores])
                print(f"Response: {response}")  # Debug print
                client_socket.send(response.encode('utf-8'))
            else:
                # Parse the data to extract name and score
                if ':' in data:
                    name, score = data.split(':')
                    score = int(score)
                    print(f"Received score data: Name={name}, Score={score}")
                    # Insert the score into the database
                    cursor.execute('INSERT INTO scores (name, score) VALUES (?, ?)', (name, score))
                    conn.commit()
                    # Retrieve the top 10 scores
                    cursor.execute('SELECT name, score FROM scores ORDER BY score DESC LIMIT 10')
                    top_scores = cursor.fetchall()
                    # Build the response
                    response = "\n".join([f"{s[0]}:{s[1]}" for s in top_scores])
                    client_socket.send(response.encode('utf-8'))
                else:
                    print(f"Invalid data received: {data}")
    except Exception as e:
        print(f"Error: {e}")
    finally:
        client_socket.close()

# Configure the server
server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind(("0.0.0.0", 9999))
server.listen(5)
print("Server listening on port 9999")

# Loop to accept client connections
while True:
    client_socket, addr = server.accept()
    print(f"Accepted connection from {addr}")
    client_handler = threading.Thread(target=handle_client, args=(client_socket,))
    client_handler.start()
