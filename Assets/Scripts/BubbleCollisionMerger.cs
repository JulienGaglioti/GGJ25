using System;
using UnityEngine;

public class BubbleCollisionMerger : MonoBehaviour
{
    public Bubble.MergePosition mergePosition = Bubble.MergePosition.Midpoint;

    // private void OnCollisioEnter2D(Collision2D other)
    // {
    //     if (!other.gameObject.CompareTag("Bubble")) return;

    //     var bubble = GetComponent<Bubble>();
    //     var otherBubble = other.gameObject.GetComponent<Bubble>();
    //     if (bubble.priority > otherBubble.priority)
    //     {
    //         bubble.MergeWith(otherBubble);
    //     }
    //     else if (bubble.priority == otherBubble.priority)
    //     {
    //         if (bubble.GetInstanceID() > otherBubble.GetInstanceID())
    //         {
    //             bubble.MergeWith(otherBubble, mergePosition);
    //         }
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!other.gameObject.CompareTag("Bubble")) return;

        var bubble = GetComponent<Bubble>();
        var otherBubble = other.gameObject.GetComponent<Bubble>();
        if (bubble.priority > otherBubble.priority)
        {
            bubble.MergeWith(otherBubble, mergePosition);
        }
        else if (bubble.priority == otherBubble.priority)
        {
            if (bubble.GetInstanceID() > otherBubble.GetInstanceID())
            {
                bubble.MergeWith(otherBubble, mergePosition);
            }
        }        
    }
}