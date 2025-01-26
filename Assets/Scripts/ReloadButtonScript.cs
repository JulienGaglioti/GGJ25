using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButtonScript : MonoBehaviour
{
    private void Start()
    {
        InputManager.Instance.ResetAction += ResetGame;
    }

    private void OnDestroy()
    {
        InputManager.Instance.ResetAction -= ResetGame;
    }

    private void ResetGame()
    {
        MyAudioManager.Instance.OnDynamicValueUpdate(0);
        Destroy(Player.Instance.gameObject);
        Destroy(BubbleStation.Instance.gameObject);
        Destroy(InputManager.Instance.gameObject);
        Destroy(WaveManager.Instance.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
