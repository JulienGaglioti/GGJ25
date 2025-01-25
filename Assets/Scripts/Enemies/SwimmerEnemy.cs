using System;
using UnityEngine;

public class SwimmerEnemy : Enemy
{
    protected override void MoveToTarget(GameObject target)
    {
        Rigidbody2D.linearVelocity = (target.transform.position - transform.position).normalized * speed;
    }
}
