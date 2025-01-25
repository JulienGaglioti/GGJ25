using System;
using UnityEngine;

public class WalkerEnemy : Enemy
{
    
    protected override void MoveToTarget(GameObject target)
    {
        Rigidbody2D.linearVelocity = ((target.transform.position - transform.position) * new Vector2(1, 0)).normalized * speed;
    }
}
