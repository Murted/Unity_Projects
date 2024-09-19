using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject pause;

    public void StartPause()
    {
        pause.SetActive(true);
        Time.timeScale = 0f;
    }
}
