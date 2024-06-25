using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private string playerName;
    private bool isGameOver = false;

    void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "Player"); // Récupérer le nom du joueur ou utiliser "Player" par défaut
        GenerateRandomScore();
        UpdateScoreUI();
    }

    void GenerateRandomScore()
    {
        score = Random.Range(0, 1001); // Génère un score aléatoire entre 0 et 1000
        Debug.Log("Generated Random Score: " + score);
        UpdateScoreUI();
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
        else
        {
            Debug.LogWarning("Score Text is not assigned!");
        }
    }

    public void OnEnemyKilled()
    {
        AddScore(10); // Ajoute 10 points pour chaque ennemi tué
    }

    public void OnPlayerDeath()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            SendScore();
            Invoke("EndLevel", 2.0f); // Attendre 2 secondes avant de changer de scène
        }
    }

    void SendScore()
    {
        var scoreClient = FindObjectOfType<ScoreClient>();
        if (scoreClient != null)
        {
            scoreClient.SendScore(playerName, score);
        }
        else
        {
            Debug.LogError("ScoreClient not found!");
        }
    }

    void EndLevel()
    {
        SceneManager.LoadScene("ScoreDisplayScene"); // Assurez-vous que le nom de la scène est correct
    }
}
