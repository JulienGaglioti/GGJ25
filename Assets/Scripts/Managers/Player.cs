using TMPro;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    public float leaveBubbleValue = 3;
    public TextMeshProUGUI ScoreText;
    public GameObject GameOverText;
    public int score = 0;
    public int wave = 0;

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

    private void UpdateText()
    {
        ScoreText.text = "WAVE: " + wave + " - SCORE: " + score;
    }

    public void AddScore(int n)
    {
        score += n;
        UpdateText();
    }

    public void SetWave(int n)
    {
        wave = n;
        UpdateText();
    }
}
