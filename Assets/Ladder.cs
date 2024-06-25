using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool IsInRange;
    private PlayerMovement playerMovement;
    public BoxCollider2D boxCollider; // Assurez-vous que cela est public

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

        // Assurez-vous que boxCollider est bien assigné dans l'inspecteur
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D is not assigned. Please assign it in the inspector.");
        }
    }

    void Update()
    {
        if (playerMovement != null)
        {
            if (IsInRange && Input.GetKey(KeyCode.E))
            {
                playerMovement.isClimbing = true;
                if (boxCollider != null)
                {
                    boxCollider.isTrigger = true; // Activer le mode "trigger" pendant l'escalade
                }
            }
            else if (!IsInRange || Input.GetKeyUp(KeyCode.E))
            {
                playerMovement.isClimbing = false;
                if (boxCollider != null)
                {
                    boxCollider.isTrigger = false; // Désactiver le mode "trigger" après l'escalade
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsInRange = false;
            if (boxCollider != null)
            {
                boxCollider.isTrigger = false; // Désactiver le mode "trigger" après avoir quitté l'échelle
            }
        }
    }
}
