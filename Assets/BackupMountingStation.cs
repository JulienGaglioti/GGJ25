using System;
using UnityEngine;

public class BackupMountingStation : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            var modeSwitcher = Player.Instance.GetComponent<ModeSwitcher>();
            if (modeSwitcher.IsShooting())
            {
                modeSwitcher.Mount();
            }
        }
    }
}
