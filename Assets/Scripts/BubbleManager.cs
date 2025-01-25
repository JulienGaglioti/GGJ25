using System;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private Bubble currentBubble;
    [SerializeField] private Bubble startingBubble;
    [SerializeField] private float decreaseRate = 0.5f;
    [field: SerializeField] public float MinValue { get; set; }

    private void Start()
    {
        currentBubble = startingBubble;
    }

    private void Update()
    {
        if (currentBubble == null) return;

        currentBubble.Oxygen -= decreaseRate * Time.deltaTime;

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