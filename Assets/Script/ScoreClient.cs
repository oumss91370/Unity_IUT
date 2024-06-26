using UnityEngine;
using System.Net.Sockets;
using System.Text;

public class ScoreClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;
    private string serverAddress = "127.0.0.1"; // L'adresse IP du serveur
    private int port = 9999; // Le port du serveur

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverAddress, port);
            stream = client.GetStream();
            Debug.Log("Connected to server");
        }
        catch (SocketException e)
        {
            Debug.LogError("SocketException: " + e);
        }
    }

    public void SendScore(string playerName, int score)
    {
        if (client == null || !client.Connected)
        {
            ConnectToServer();
        }

        string scoreData = playerName + ":" + score.ToString();
        byte[] data = Encoding.UTF8.GetBytes(scoreData);
        stream.Write(data, 0, data.Length);
        Debug.Log("Score data sent: " + scoreData);

        // Lire la réponse du serveur
        byte[] responseData = new byte[1024];
        int bytes = stream.Read(responseData, 0, responseData.Length);
        string response = Encoding.UTF8.GetString(responseData, 0, bytes);

        Debug.Log("Server response: " + response);
    }

    void OnApplicationQuit()
    {
        if (stream != null) stream.Close();
        if (client != null) client.Close();
    }
}
