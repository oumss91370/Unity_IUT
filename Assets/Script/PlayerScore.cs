using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    public TextMeshProUGUI playerNameText; // Utiliser TextMeshProUGUI pour TextMeshPro
    public int playerScore;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "DefaultName");
        if (playerNameText != null)
        {
            playerNameText.text = "Player: " + playerName;
        }
        else
        {
            Debug.LogWarning("Player Name Text is not assigned.");
        }
    }

    public void UpdateScore(int score)
    {
        playerScore += score;
        // Mettez à jour l'affichage du score ou autres logiques liées au score
    }
}
