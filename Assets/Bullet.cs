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
    
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        EnnemyPatrol enemy = hitInfo.GetComponent<EnnemyPatrol>();
        if (enemy != null)
        {
            enemy.TakeDamage(10);
        }
        Destroy(gameObject);
        Debug.Log("BalleDetruit");
    }
    
}
