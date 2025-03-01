using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameSceneController : MonoBehaviour
{
    [Header("Timer")]
    [SerializeField] private int second;
    [SerializeField] private int minute;
    [SerializeField] private TextMeshProUGUI timer;
    [Header("FruitSelection")]
    [SerializeField] private int count;
    [SerializeField] private TextMeshProUGUI fruitCount;
    public int selected = 0;
    [Header("Victory")]
    [SerializeField] private GameObject victory;
    [SerializeField] private int reward;
    [Header("GameOver")]
    [SerializeField] private GameObject gameOver;
    [SerializeField] public TextMeshProUGUI reason;
    [Header("Level")]
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private int level;
    [Header("Buttons")]
    [SerializeField] private List<Button> buttons;
    [SerializeField] private Button firstButton;
    [Header("Panel")]
    [SerializeField] private GameObject panel;

    public int basket = 1;

    public bool game = true;

    private void Start()
    {
        levelText.text = "Level: " + level.ToString();
        firstButton.enabled = false;
        firstButton.image.color = Color.gray;
        StartCoroutine(TikTak());
    }
    private void Update()
    {
        if (game)
        {
            if (minute < 10 && second > 10)
            {
                timer.text = "0" + minute.ToString() + ":" + second.ToString();
            }
            else if (minute > 10 && second < 10)
            {
                timer.text = minute.ToString() + ":0" + second.ToString();
            }
            else if(minute < 10 && second < 10)
            {
                timer.text = "0" + minute.ToString() + ":0" + second.ToString();
            }
            else
            {
                timer.text = minute.ToString() + ":" + second.ToString();
            }
            fruitCount.text = selected.ToString() + "/" + count.ToString();
            if (selected == count)
            {
                victory.SetActive(true);
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + reward);
                panel.SetActive(false);
                StopAllCoroutines();
                DisableButtons();
                gameObject.SetActive(false);
            }
        }
        else
        {
            gameOver.SetActive(true);
            panel.SetActive(false);
            StopAllCoroutines();
            DisableButtons();
            gameObject.SetActive(false);
        }
    }

    public IEnumerator TikTak()
    {
        while (true)
        {
            if (game)
            {
                yield return new WaitForSeconds(1f);
                if (second == 0)
                {
                    if (minute == 0)
                    {
                        game = false;
                        reason.text = "Time is up";
                    }
                    else
                    {
                        minute--;
                        second = 59;
                    }
                }
                else
                {
                    second--;
                }
            }
            else
            {
                break;
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(level);
    }

    public void DisableButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].enabled = false;
            buttons[i].image.color = Color.gray;
        }
    }
}
