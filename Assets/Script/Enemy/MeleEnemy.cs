using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 1f;
    [SerializeField] private int dommage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //attack que qu&nd le player est dans le champ de vision
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //Attck 
            }
        }
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size,
            0, Vector2.left, 0,playerLayer );
        //Raycast pour voir si le player est dans le champ de vision
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color=Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
    }
}