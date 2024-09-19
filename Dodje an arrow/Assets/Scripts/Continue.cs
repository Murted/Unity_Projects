using UnityEngine;

public class Continue : MonoBehaviour
{
    [SerializeField] GameObject pause;

    public void ContinueToPlay()
    {
        pause.SetActive(false);
        Time.timeScale = 1f;
    }
}
