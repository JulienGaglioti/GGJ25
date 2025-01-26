using System;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private Bubble currentBubble;
    [SerializeField] private Bubble startingBubble;
    public float DecreaseRate = 0.5f;
    [field: SerializeField] public float MinValue { get; set; }

    private void Start()
    {
        currentBubble = startingBubble;
    }

    private void Update()
    {
        if (currentBubble == null) return;

        currentBubble.Oxygen -= DecreaseRate * Time.deltaTime;

        if (currentBubble.Oxygen < MinValue)
        {
            Destroy(currentBubble.gameObject);
            currentBubble = null;
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