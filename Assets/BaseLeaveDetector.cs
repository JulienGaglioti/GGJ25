using System;
using UnityEngine;

public class BaseLeaveDetector : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    [SerializeField] private float leaveBubbleValue = 3;
    private static bool ColliderIsPlayer(Collider2D collider)
    {
        return collider.CompareTag("Player");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log(other.name + " gameobject exit");
        if (ColliderIsPlayer(other))
        {
            Debug.Log("Player exit");
            var bubbleManager = other.GetComponent<BubbleManager>();
            var newBubbleObj = Instantiate(bubblePrefab, other.transform);
            var newBubble = newBubbleObj.GetComponent<Bubble>();
            newBubble.SetCanMerge(false);
            var baseBubble = transform.parent.GetComponent<Bubble>();
            bubbleManager.SetCurrentBubble(newBubble);
            newBubbleObj.transform.position = other.transform.position;
            Destroy(newBubbleObj.GetComponent<Rigidbody2D>());
            newBubbleObj.layer = LayerMask.NameToLayer("PlayerBubble");
            newBubble.Oxygen = leaveBubbleValue;
            baseBubble.Oxygen -= leaveBubbleValue;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name + " gameobject enter");
        if (ColliderIsPlayer(other))
        {
            Debug.Log("Player enter");
            var bubbleManager = other.GetComponent<BubbleManager>();
            var otherBubble = bubbleManager.GetCurrentBubble();
            var baseBubble = transform.parent.GetComponent<Bubble>();
            if (otherBubble == baseBubble) return;
            Debug.Log("Player enter 2");
            bubbleManager.SetCurrentBubble(baseBubble);
            Destroy(otherBubble.gameObject);
            baseBubble.Oxygen += otherBubble.Oxygen;
        }
    }
}
