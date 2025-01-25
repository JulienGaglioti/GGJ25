using System.Collections;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private float decreaseRate;
    [SerializeField] private Bubble currentBubble; // la bolla abitata attualmente dal player
    [SerializeField] private Bubble startingBubble;
    [SerializeField] private float minBubbleValue;
    private WaitForSeconds _decreaseValueTime;

    private void Start() 
    {
        currentBubble = startingBubble;
        _decreaseValueTime = new WaitForSeconds(0.1f);
        StartCoroutine(DecreaseValueCoroutine());
    }

    private IEnumerator DecreaseValueCoroutine()
    {
        while(true)
        {
            if(currentBubble != null)
            {
                currentBubble.Value -= decreaseRate;
                if(currentBubble.Value < minBubbleValue)
                {
                    currentBubble.Value = minBubbleValue;
                }
                yield return _decreaseValueTime;
            }            
        }
    }
}
