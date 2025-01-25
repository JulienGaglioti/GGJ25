using System.Collections;
using UnityEngine;

public class BubbleManager : MonoBehaviour
{
    [SerializeField] private float oxygenDecreaseRate;
    [SerializeField] private Bubble currentBubble; // la bolla abitata attualmente dal player
    [SerializeField] private Bubble startingBubble;
    [SerializeField] private float minOxygenValue;
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
                currentBubble.Oxygen -= oxygenDecreaseRate;
                if(currentBubble.Oxygen < minOxygenValue)
                {
                    currentBubble.Oxygen = minOxygenValue;
                }
                yield return _decreaseValueTime;
            }            
        }
    }
}
