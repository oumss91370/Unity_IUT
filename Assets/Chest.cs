
using UnityEngine;

public class Chest : MonoBehaviour
{
    public bool IsInRange;
    public Animator animator;

    void Awake()
    {
        
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
        animator.SetTrigger("OpenChest");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IsInRange = true;
        }
    }
}
