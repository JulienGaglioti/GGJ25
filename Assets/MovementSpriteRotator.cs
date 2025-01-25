using System;
using UnityEngine;

public class MovementSpriteRotator : MonoBehaviour
{
    void Awake()
    {
        GetComponent<Movement2D>().OnMove.AddListener(RotateSprite);
    }

    void RotateSprite(Vector2 movement)
    {
        transform.localScale = new Vector3(1 * (movement.x < 0 ? -1 : 1), transform.localScale.y, transform.localScale.z);
    }
}
