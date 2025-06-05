using UnityEngine;
using UnityEngine.UI;
using YG;
using System.Collections;
using UnityEngine.EventSystems;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private ArrowSpawner arrowSpawner;
    [SerializeField] private GameObject sceneObjects;
    [SerializeField] private Transform playerStartPos;
    [SerializeField] private PlayerController player;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private Text timeText;
    [SerializeField] private Text bestTimeText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button secondChanceButton;
    [SerializeField] private YandexGame sdk;

    public bool IsGameOver = false;

    private bool _isPaused;
    private bool _isSecondChanceUsed = false;
    private float _currentTime;
    private float _bestTime;
    private Coroutine _timerCoroutine;
    private int deathCount = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (_musicSource != null)
        {
            _musicSource.ignoreListenerPause = true;
        }

        _bestTime = PlayerPrefs.GetFloat("BestTime", 0f);


        playButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        resumeButton.onClick.AddListener(ResumeGame);
        pauseButton.onClick.AddListener(PauseGame);
        secondChanceButton.onClick.AddListener(SecondChanceAd);

        mainMenuUI.SetActive(true);
        gameUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);

        sceneObjects.SetActive(false);
    }

    private IEnumerator TimerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _currentTime++;
            timeText.text = $"Время: {_currentTime}";
        }
    }

    public void PauseGame()
    {
        if (_isPaused) return;
        _isPaused = true;

        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseUI.SetActive(true);

        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);
    }

    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        gameUI.SetActive(true);
        sceneObjects.SetActive(true);

        _currentTime = 0;
        timeText.text = "Время: 0";

        _timerCoroutine = StartCoroutine(TimerRoutine());
    }

    public void ResumeGame()
    {
        if (!_isPaused) return;
        _isPaused = false;

        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseUI.SetActive(false);

        _timerCoroutine = StartCoroutine(TimerRoutine());
    }

    public void GameOver()
    {
        if (_isPaused) return;
        _isPaused = true;

        Time.timeScale = 0;
        player.ResetSpeed();
        AudioListener.pause = true;
        gameUI.SetActive(false);
        if(_isSecondChanceUsed) secondChanceButton.gameObject.SetActive(false);

        if (_currentTime > _bestTime)
        {
            _bestTime = _currentTime;
            PlayerPrefs.SetFloat("BestTime", _bestTime);
            PlayerPrefs.Save();
        }

        bestTimeText.text = $"Лучшее время: {_bestTime}";

        gameOverUI.SetActive(true);

        if (!_isSecondChanceUsed)
        {
            deathCount++;
            if (deathCount >= 2)
            {
                if (YandexGame.EnvironmentData.isDesktop)
                {
                    Debug.Log("Тест: Показ интерстициальной рекламы (в редакторе)");
                    deathCount = 0;
                }
                else
                {
                    YandexGame.Instance._FullscreenShow();
                    deathCount = 0;
                }
            }
        }
    }

    public void RestartGame()
    {
        _isPaused = false;
        _isSecondChanceUsed = false;
        secondChanceButton.gameObject.SetActive(true);

        Time.timeScale = 1;

        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);
        gameUI.SetActive(true);

        _currentTime = 0;
        timeText.text = "Время: 0";

        player.gameObject.transform.position = playerStartPos.position;


        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine);

        _timerCoroutine = StartCoroutine(TimerRoutine());
        arrowSpawner.ResetSpawner();
        IsGameOver = false;

    }

    public void GetSecondChance()
    {
        _isPaused = false;
        _isSecondChanceUsed = true;

        Time.timeScale = 1;

        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);
        gameUI.SetActive(true);

        player.gameObject.transform.position = playerStartPos.position;

        arrowSpawner.ResetSecondChance();
        IsGameOver = false;
    }

    public void SecondChanceAd()
    {
        sdk._RewardedShow(1);
    }
}