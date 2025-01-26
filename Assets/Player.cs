using TMPro;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    public float leaveBubbleValue = 3;
    public TextMeshProUGUI ScoreText;
    public int score = 0;
    
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

    public void AddScore(int n)
    {
        score += n;
        ScoreText.text = score.ToString();
    }
}
