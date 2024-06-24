using UnityEngine;
using TMPro;
using System.Net.Sockets;
using System.Text;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] scoreTexts; // Assigner les éléments TextMeshProUGUI dans l'inspecteur
    private string serverAddress = "127.0.0.1"; // L'adresse IP du serveur
    private int port = 9999; // Le port du serveur

    void Start()
    {
        ConnectToServerAndRetrieveScores();
    }

    void ConnectToServerAndRetrieveScores()
    {
        try
        {
            TcpClient client = new TcpClient(serverAddress, port);
            NetworkStream stream = client.GetStream();

            // Envoyer une requête pour obtenir les scores
            byte[] request = Encoding.UTF8.GetBytes("GET_SCORES");
            stream.Write(request, 0, request.Length);

            // Lire la réponse du serveur
            byte[] responseData = new byte[1024];
            int bytes = stream.Read(responseData, 0, responseData.Length);
            string response = Encoding.UTF8.GetString(responseData, 0, bytes);

            // Afficher les scores
            DisplayScores(response);

            stream.Close();
            client.Close();
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
    }

    void DisplayScores(string scores)
    {
        string[] scoreEntries = scores.Split('\n');
        for (int i = 0; i < scoreEntries.Length && i < scoreTexts.Length; i++)
        {
            scoreTexts[i].text = scoreEntries[i];
        }
    }
}
