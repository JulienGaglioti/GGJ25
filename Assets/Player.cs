using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    public float leaveBubbleValue = 3;
    
    [SerializeField] private RuntimeAnimatorController swimAnimation;
    [SerializeField] private RuntimeAnimatorController steerAnimation;

    public void StartSwimAnimation()
    {
        GetComponent<Animator>().runtimeAnimatorController = swimAnimation;
    }

    public void StartSteerAnimation()
    {
        GetComponent<Animator>().runtimeAnimatorController = steerAnimation;
    }
}
