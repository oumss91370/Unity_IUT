import socket

def send_message(message):
    host = 'localhost'
    port = 9999
    with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
        s.connect((host, port))
        s.sendall(message.encode())
        data = s.recv(1024)
    print('Received', data.decode())

# Test getting scores
send_message("GET_SCORES")

# Test sending a score
send_message("player1:100")
