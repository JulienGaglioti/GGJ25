using System;
using UnityEngine;

public class BaseLeaveDetector : MonoBehaviour
{
    private static bool ColliderIsPlayer(Collider2D collider)
    {
        return collider.CompareTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (ColliderIsPlayer(other))
        {
            other.GetComponent<ModeSwitcher>().Mount();
        }
    }
}
