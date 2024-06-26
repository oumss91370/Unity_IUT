using UnityEngine;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    public GameObject endLevelUI; // Référence à l'UI de fin de niveau
    public TextMeshProUGUI endLevelMessage; // Référence au texte du message de fin de niveau
    public TextMeshProUGUI scoreText; // Référence au texte du score

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Appeler la méthode de fin de niveau dans le GameController
            GameController gameController = FindObjectOfType<GameController>();
            if (gameController != null)
            {
                int finalScore = gameController.GetScore();
                DisplayEndLevelMessage(finalScore);
                gameController.OnLevelComplete();
            }
        }
    }

    private void DisplayEndLevelMessage(int score)
    {
        endLevelUI.SetActive(true);
        endLevelMessage.text = "Level Complete!";
        scoreText.text = "Score: " + score;
    }
}
