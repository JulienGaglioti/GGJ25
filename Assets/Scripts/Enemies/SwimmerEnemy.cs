using System;
using UnityEngine;

public class SwimmerEnemy : Enemy
{
    protected override Vector2 MoveToTarget(GameObject target)
    {
        return Rigidbody2D.linearVelocity = (target.transform.position - transform.position).normalized * speed;
    }
}
