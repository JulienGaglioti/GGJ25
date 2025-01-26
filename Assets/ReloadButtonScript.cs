using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadButtonScript : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Destroy(Player.Instance.gameObject);
            Destroy(BubbleStation.Instance.gameObject);
            Destroy(InputManager.Instance.gameObject);
            Destroy(WaveManager.Instance.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
