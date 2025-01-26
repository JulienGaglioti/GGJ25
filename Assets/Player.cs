using TMPro;
using UnityEngine;

public class Player : MonoBehaviourSingleton<Player>
{
    public float leaveBubbleValue = 3;
    public TextMeshProUGUI ScoreText;
    public int score = 0;


    public void AddScore(int n)
    {
        score += n;
        ScoreText.text = score.ToString();
    }
}
