using UnityEngine;

public class Ladder : MonoBehaviour
{
    private bool isInRange;
    private PlayerMovement playerMovement;
    public BoxCollider2D ladderTrigger; // Le BoxCollider2D enfant

    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement>();
            if (playerMovement == null)
            {
                Debug.LogError("PlayerMovement script not found on the player object.");
            }
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found.");
        }

        if (ladderTrigger == null)
        {
            Debug.LogError("Ladder Trigger is not assigned. Please assign it in the inspector.");
        }
    }

    void Update()
    {
        if (playerMovement != null && isInRange)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                playerMovement.StartClimbing();
                ladderTrigger.enabled = false; // Désactiver le BoxCollider2D enfant pendant l'escalade
            }
            else if (Input.GetKeyUp(KeyCode.E))
            {
                playerMovement.StopClimbing();
                ladderTrigger.enabled = true; // Réactiver le BoxCollider2D enfant après l'escalade
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = true;
            playerMovement.SetCurrentLadder(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isInRange = false;
            playerMovement.StopClimbing();
            playerMovement.SetCurrentLadder(null);
            ladderTrigger.enabled = true; // Réactiver le BoxCollider2D enfant après avoir quitté l'échelle
        }
    }
}