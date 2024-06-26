using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPatrol : MonoBehaviour
{
    public SpriteRenderer graphics;
    public float speed;
    public Transform[] waypoints;

    private Transform target;
    public int health = 100;
    public GameObject deathEffect;
    private GameController gameController; // Référence au GameController

    private int destPoint = 0;

    void Start()
    {
        target = waypoints[0];
        gameController = FindObjectOfType<GameController>(); // Trouver le GameController dans la scène
    }

    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            graphics.flipX = !graphics.flipX;
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
        Destroy(deathEffect, 10f);

        // Ajouter des points lorsqu'un ennemi est tué
        if (gameController != null)
        {
            gameController.OnEnemyKilled();
        }
    }
}
