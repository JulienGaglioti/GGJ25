using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Enemy : MonoBehaviour
{
    public EnemyType EnemyType;
    public int DifficultyValue;
    public int FirstAppearance;
    private GameObject _playerObj;
    protected Rigidbody2D Rigidbody2D;
    private void Awake()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.gravityScale = 0;
        Rigidbody2D.freezeRotation = true;
    }

    private void Update()
    {
        if (_playerObj != null)
        {
            MoveToTarget(_playerObj);
        }
    }

    protected abstract void MoveToTarget(GameObject target);
}

public enum EnemyType
{
    Walker,
    Swimmer,
    Shooter
}
