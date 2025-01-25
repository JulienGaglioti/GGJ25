using System;
using UnityEngine;

public class SwimmerEnemy : Enemy
{
    [SerializeField] private float speed = 1;
    protected override void MoveToTarget(GameObject target)
    {
        Rigidbody2D.linearVelocity = (target.transform.position - transform.position).normalized * speed;
    }
}
