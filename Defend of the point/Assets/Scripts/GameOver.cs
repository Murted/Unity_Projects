using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
