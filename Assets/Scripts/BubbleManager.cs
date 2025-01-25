using System.Collections;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private float decreaseRate;
    [SerializeField] private Bubble currentBubble; // la bolla abitata attualmente dal player
    [SerializeField] private Bubble startingBubble;
    private float _reserveValue = 0.5f;

    private void Start()
    {
        currentBubble = startingBubble;
    }

    private void Update()
    {
        if (currentBubble == null) return;

        float minBubbleValue = currentBubble.MinValue;
        currentBubble.Value = Mathf.Max(currentBubble.Value - decreaseRate * Time.deltaTime, minBubbleValue);
        
        if (currentBubble.Value < minBubbleValue)
        {
            Destroy(currentBubble.gameObject);
            return;
        }
        
        if (currentBubble.Value <= minBubbleValue + _reserveValue)
        {
            currentBubble.BorderAlpha = (currentBubble.Value - minBubbleValue) / _reserveValue;
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