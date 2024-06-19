using System;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    [Header("Enemy")] [SerializeField] private Transform enemy;
    
    [Header("Movement parameters")]
    [SerializeField] private float speed;

    private Vector3 initScale;
    private bool movingLeft;
    
    [Header("Enemy Animator")]
    private Animator animator;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
            {
                DircetionChange(); 
            }
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
            {
                DircetionChange();
            }
        }
    }
    
    private void DircetionChange()
    {
        animator.SetBool("moving", true);
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        animator.SetBool("moving", true);
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction, initScale.y, initScale.z);
        
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed ,
            enemy.position.y, enemy.position.z);
    }
}