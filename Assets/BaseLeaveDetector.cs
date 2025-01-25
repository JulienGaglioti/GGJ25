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
            Player.Instance.GetComponent<ModeSwitcher>().Mount();
            var bubbleManager = other.GetComponent<BubbleManager>();
            var otherBubble = bubbleManager.GetCurrentBubble();
            var baseBubble = transform.parent.GetComponent<Bubble>();
            if (otherBubble == baseBubble) return;
            bubbleManager.SetCurrentBubble(baseBubble);
            Destroy(otherBubble.gameObject);
            baseBubble.Oxygen += otherBubble.Oxygen;
        }
    }
}
