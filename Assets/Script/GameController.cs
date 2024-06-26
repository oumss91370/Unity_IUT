using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI enemyCountText; // Ajoutez cette ligne pour le texte du compteur d'ennemis
    private int score = 0;
    private int enemyCount = 0; // Compteur d'ennemis
    private string playerName;
    private bool isGameOver = false;
    private float startTime;
    private float endTime;

    void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "Player"); // Récupérer le nom du joueur ou utiliser "Player" par défaut
        startTime = Time.time; // Enregistrer le temps de début
        UpdateUI();
    }

    void Update()
    {
        if (!isGameOver)
        {
            UpdateUI();
        }
    }

    public void AddScore(int points)
    {
        score += points;
        UpdateUI();
    }

    public int GetScore()
    {
        return score;
    }

    void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("Score Text is not assigned!");
        }

        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemies Killed: " + enemyCount;
        }
        else
        {
            Debug.LogWarning("Enemy Count Text is not assigned!");
        }
    }

    public void OnEnemyKilled()
    {
        AddScore(100); // Ajoute 100 points pour chaque ennemi tué
        enemyCount++; // Incrémente le compteur d'ennemis
        UpdateUI();
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

    public void OnLevelComplete()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            endTime = Time.time; // Enregistrer le temps de fin
            int timeScore = Mathf.RoundToInt((endTime - startTime) * 10); // Calculer le score basé sur le temps, par exemple 10 points par seconde
            AddScore(timeScore); // Ajouter le score du temps au score total
            SendScore();
            Invoke("EndLevel", 2.0f); // Attendre 2 secondes avant de changer de scène
        }
    }

    void EndLevel()
    {
        SceneManager.LoadScene("ScoreDisplayScene"); // Assurez-vous que le nom de la scène est correct
    }
}
