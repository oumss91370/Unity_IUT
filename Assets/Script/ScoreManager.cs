using UnityEngine;
using TMPro;
using System.Net.Sockets;
using System.Text;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UI pour afficher le score
    private int score = 0;
    private string playerName = "Player";
    private TcpClient client;
    private NetworkStream stream;
    private string serverAddress = "127.0.0.1"; // L'adresse IP du serveur
    private int port = 9999; // Le port du serveur

    void Start()
    {
        // Initialisation du client socket
        ConnectToServer();
        UpdateScoreUI();
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
            Debug.Log("SocketException: " + e);
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void SendScore()
    {
        if (client == null || !client.Connected)
        {
            ConnectToServer();
        }

        string scoreData = playerName + ":" + score.ToString();
        byte[] data = Encoding.UTF8.GetBytes(scoreData);
        stream.Write(data, 0, data.Length);

        // Lire la réponse du serveur
        byte[] responseData = new byte[1024];
        int bytes = stream.Read(responseData, 0, responseData.Length);
        string response = Encoding.UTF8.GetString(responseData, 0, bytes);

        // Afficher le top 10 des scores
        Debug.Log("Top 10 scores: " + response);
    }

    void OnApplicationQuit()
    {
        if (stream != null) stream.Close();
        if (client != null) client.Close();
    }
}