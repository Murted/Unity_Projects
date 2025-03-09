using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private ArrowSpawner arrowSpawner;
    [SerializeField] private GameObject sceneObjects;   // Игрок
    [SerializeField] private Transform playerStartPos; // Начальная позиция игрока
    [SerializeField] private GameObject player;
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameOverUI; // Панель поражения
    [SerializeField] private GameObject pauseUI; // Панель паузы
    [SerializeField] private Text timeText; // Текст с временем
    [SerializeField] private Text bestTimeText; // Текст с лучшим временем
    [SerializeField] private Button restartButton; // Кнопка рестарта
    [SerializeField] private Button resumeButton; // Кнопка продолжения игры
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button playButton;

    public bool IsGameOver = false;

    private bool _isPaused;
    private float _currentTime;
    private float _bestTime;
    private Coroutine _timerCoroutine;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (_musicSource != null)
        {
            _musicSource.ignoreListenerPause = true; // Музыка продолжает играть при паузе
        }

        _bestTime = PlayerPrefs.GetFloat("BestTime", 0f); // Загружаем лучшее время


        playButton.onClick.AddListener(StartGame);
        restartButton.onClick.AddListener(RestartGame);
        resumeButton.onClick.AddListener(ResumeGame);
        pauseButton.onClick.AddListener(PauseGame);

        // Показываем главное меню, выключаем игру
        mainMenuUI.SetActive(true);
        gameUI.SetActive(false);
        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);

        sceneObjects.SetActive(false); // Отключаем игрока
    }

    private IEnumerator TimerRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Ждём 1 секунду
            _currentTime++;
            timeText.text = $"Время: {_currentTime}"; // Обновляем текст
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
            StopCoroutine(_timerCoroutine); // Останавливаем таймер
    }

    public void StartGame()
    {
        // Запуск игры
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

        _timerCoroutine = StartCoroutine(TimerRoutine()); // Возобновляем таймер
    }

    public void GameOver()
    {
        if (_isPaused) return;
        _isPaused = true;

        Time.timeScale = 0;
        AudioListener.pause = true;
        gameUI.SetActive(false);

        // Проверяем рекорд
        if (_currentTime > _bestTime)
        {
            _bestTime = _currentTime;
            PlayerPrefs.SetFloat("BestTime", _bestTime);
            PlayerPrefs.Save();
        }

        bestTimeText.text = $"Лучшее время: {_bestTime}";

        gameOverUI.SetActive(true);
    }

    public void RestartGame()
    {
        _isPaused = false;

        Time.timeScale = 1;

        gameOverUI.SetActive(false);
        pauseUI.SetActive(false);
        gameUI.SetActive(true);

        _currentTime = 0;
        timeText.text = "Время: 0";

        player.transform.position = playerStartPos.position; //  Сбрасываем позицию игрока


        if (_timerCoroutine != null)
            StopCoroutine(_timerCoroutine); //  Останавливаем старый таймер

        _timerCoroutine = StartCoroutine(TimerRoutine()); //  Запускаем новый таймер
        arrowSpawner.ResetSpawner();
        IsGameOver = false;

    }
}
