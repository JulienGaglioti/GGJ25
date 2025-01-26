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
        if (movement.x != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(movement.x), transform.localScale.y, transform.localScale.z);
        }
    }
}
