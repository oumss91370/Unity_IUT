using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    void Start()
    {
        rb.velocity = transform.right * 50 ;
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        Health enemy = collision.GetComponent<Health>();
        if (collision.tag=="Enemy" )
        {
            enemy.TakeDamage(30);
            
            Debug.Log("Touche");
        }
        Destroy(gameObject);
        Debug.Log("BalleDetruit");
    }
    
}
