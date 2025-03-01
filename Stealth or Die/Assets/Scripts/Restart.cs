using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Restart : MonoBehaviour
{
    [SerializeField]
    public Button restartButton;
    [SerializeField]
    private string sceneName;

    void Start()
    {
        restartButton.onClick.AddListener(OnButtonClick);
    }

    public void OnButtonClick()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}
