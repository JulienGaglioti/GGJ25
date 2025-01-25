using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public abstract class Enemy : MonoBehaviour
{
    public EnemyType EnemyType;
    public int DifficultyValue;
    public int FirstAppearance;
    private GameObject _playerObj;
    [SerializeField] protected float speed = 1;
    [SerializeField] protected float minSpeedVariation = 0.85f;
    [SerializeField] protected float maxSpeedVariation = 1.15f;
    protected Rigidbody2D Rigidbody2D;
    private void Awake()
    {
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.gravityScale = 0;
        Rigidbody2D.freezeRotation = true;
        RandomizeSpeed();
    }

    private void Update()
    {
        if (_playerObj != null)
        {
            MoveToTarget(_playerObj);
        }
    }

    public void RandomizeSpeed()
    {
        speed *= Random.Range(minSpeedVariation, maxSpeedVariation);
    }

    protected abstract void MoveToTarget(GameObject target);
}

public enum EnemyType
{
    Walker,
    Swimmer,
    Shooter
}
