import socket
import threading
import sqlite3

# Connexion à la base de données SQLite
conn = sqlite3.connect('scores.db', check_same_thread=False)
cursor = conn.cursor()

# Créer la table des scores si elle n'existe pas
cursor.execute('''CREATE TABLE IF NOT EXISTS scores
                  (id INTEGER PRIMARY KEY, name TEXT, score INTEGER)''')
conn.commit()

# Fonction pour gérer les connexions des clients
def handle_client(client_socket):
    try:
        # Réception des données du client
        data = client_socket.recv(1024).decode('utf-8')
        if data:
            # Vérifier si la commande est pour obtenir les scores
            if data == "GET_SCORES":
                print("Received request to get scores")
                # Récupérer les 10 meilleurs scores
                cursor.execute('SELECT name, score FROM scores ORDER BY score DESC LIMIT 10')
                top_scores = cursor.fetchall()
                # Construire la réponse
                response = "\n".join([f"{s[0]}:{s[1]}" for s in top_scores])
                client_socket.send(response.encode('utf-8'))
            else:
                # Parsing de la chaîne de caractères pour extraire le nom et le score
                if ':' in data:
                    name, score = data.split(':')
                    score = int(score)
                    print(f"Received score data: Name={name}, Score={score}")
                    # Insertion du score dans la base de données
                    cursor.execute('INSERT INTO scores (name, score) VALUES (?, ?)', (name, score))
                    conn.commit()
                    # Récupérer les 10 meilleurs scores
                    cursor.execute('SELECT name, score FROM scores ORDER BY score DESC LIMIT 10')
                    top_scores = cursor.fetchall()
                    # Construire la réponse
                    response = "\n".join([f"{s[0]}:{s[1]}" for s in top_scores])
                    client_socket.send(response.encode('utf-8'))
                else:
                    print(f"Invalid data received: {data}")
    except Exception as e:
        print(f"Error: {e}")
    finally:
        client_socket.close()

# Configuration du serveur
server = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
server.bind(("0.0.0.0", 9999))
server.listen(5)
print("Server listening on port 9999")

# Boucle pour accepter les connexions des clients
while True:
    client_socket, addr = server.accept()
    print(f"Accepted connection from {addr}")
    client_handler = threading.Thread(target=handle_client, args=(client_socket,))
    client_handler.start()
