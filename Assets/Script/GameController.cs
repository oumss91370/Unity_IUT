using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText; 
    private ScoreClient scoreClient;
    private int score = 0;
    private string playerName = "Player";

    void Start()
    {
        scoreClient = FindObjectOfType<ScoreClient>();
        GenerateRandomScore();
        UpdateScoreUI();
    }

    void GenerateRandomScore()
    {
        score = Random.Range(0, 1001); // Génère un score aléatoire entre 0 et 1000
        Debug.Log("Generated Random Score: " + score);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            scoreClient.SendScore(playerName, score);
            SceneManager.LoadScene("ScoreDisplayScene"); 
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
        else
        {
            Debug.LogWarning("Score Text is not assigned!");
        }
    }

    // Appel lorsque l'ennemi est tué
    public void OnEnemyKilled()
    {
        AddScore(10); // Ajoute 10 points pour chaque ennemi tué
    }
}
