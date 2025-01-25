using System;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Movement2D : MonoBehaviour
{
    [Serializable] public class MoveEvent : UnityEvent<Vector2> {}

    [Header("Movement Settings")]
    [SerializeField] private float maxSpeed = 2f;
    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float deceleration = 4f;

    public bool CanMove { get; set; } = true;

    public MoveEvent OnMove;

    public Vector2 Velocity => _currentVelocity;
    public Vector2 NormalizedVelocity => _currentVelocity.normalized;
    public BoxCollider2D Collider => _collider;
    public Rigidbody2D Rigidbody => _rigidbody;

    private Vector2 _currentVelocity;
    private Vector2 _targetVelocity;

    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (CanMove)
        {
            Move(InputManager.Instance.MoveInput);
        }
        else
        {
            Move(Vector2.zero);
        }
    }

    public void Move(Vector2 direction)
    {
        _targetVelocity = direction.normalized * maxSpeed;
    }

    private void FixedUpdate()
    {
        float currentAcceleration = _targetVelocity.sqrMagnitude > 0.0001f ? acceleration : deceleration;

        _currentVelocity = Vector2.MoveTowards(
            _currentVelocity,
            _targetVelocity,
            currentAcceleration * Time.fixedDeltaTime
        );

        Rigidbody.linearVelocity = _currentVelocity;

        OnMove.Invoke(_currentVelocity);
    }
}