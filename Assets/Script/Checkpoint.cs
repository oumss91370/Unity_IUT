using UnityEngine;
using TMPro;

public class Checkpoint : MonoBehaviour
{
    public GameObject endLevelUI; // Référence à l'UI de fin de niveau
    public TextMeshProUGUI endLevelMessage; // Référence au texte du message de fin de niveau
    public TextMeshProUGUI scoreText; // Référence au texte du score
    public int requiredKeys = 2; // Nombre de clés requises pour terminer le niveau

    private KeyManager keyManager;

    void Start()
    {
        keyManager = FindObjectOfType<KeyManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (keyManager != null && keyManager.GetKeyCount() >= requiredKeys)
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
            else
            {
                Debug.Log("Player does not have enough keys!");
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