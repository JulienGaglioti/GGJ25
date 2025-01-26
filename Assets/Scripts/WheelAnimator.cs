using TMPro;
using UnityEngine;

public class WheelAnimator : MonoBehaviour
{
    [SerializeField] private GameObject wheel;
    [SerializeField] private RuntimeAnimatorController staticAnimation;
    [SerializeField] private RuntimeAnimatorController steerAnimation;

    public void StartStaticAnimation()
    {
        wheel.GetComponent<Animator>().runtimeAnimatorController = staticAnimation;
    }

    public void StartSteerAnimation()
    {
        wheel.GetComponent<Animator>().runtimeAnimatorController = steerAnimation;
    }
}