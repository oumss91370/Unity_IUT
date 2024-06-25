using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool IsInRange;
    public Animator animator;
    public KeyManager keyManager;
    public bool isOpened = false;

    void Awake()
    {
        // Assurez-vous que keyManager est correctement assigné dans l'inspecteur Unity
        if (keyManager == null)
        {
            Debug.LogError("KeyManager reference not assigned in Chest script.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && IsInRange)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        // Vérifiez si le coffre a déjà été ouvert
        if (!isOpened)
        {
            animator.SetTrigger("OpenChest");

            // Collecte une clé en appelant la fonction dans KeyManager
            keyManager.CollectKey();
            isOpened = true; // Marquez le coffre comme ouvert
        }
        else
        {
            Debug.Log("Chest already opened.");
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
        }
    }
}
