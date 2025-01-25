using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private Bubble currentBubble; // la bolla abitata attualmente dal player
    [SerializeField] private Bubble startingBubble;
    [SerializeField] private float reserveValue = 0.5f;
    [SerializeField] private float decreaseRate = 0.5f;
    [field: SerializeField] public float MinValue { get; set; }

    private void Start()
    {
        currentBubble = startingBubble;
    }

    private void Update()
    {
        if (currentBubble == null) return;

        float minBubbleValue = MinValue;
        currentBubble.Oxygen -= decreaseRate * Time.deltaTime;
        
        if (currentBubble.Oxygen < minBubbleValue)
        {
            Destroy(currentBubble.gameObject);
            return;
        }
        
        if (currentBubble.Oxygen <= minBubbleValue + reserveValue)
        {
            currentBubble.BorderAlpha = (currentBubble.Oxygen - minBubbleValue) / reserveValue;
        }
        else
        {
            currentBubble.BorderAlpha = 1;
        }
    }

    public void SetCurrentBubble(Bubble bubble)
    {
        currentBubble = bubble;
    }

    public Bubble GetCurrentBubble()
    {
        return currentBubble;
    }
}