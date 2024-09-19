using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomBonus : MonoBehaviour
{
    [SerializeField] private List<Button> BlockButton;
    [SerializeField] private List<Text> bonusText;
    [SerializeField] private TextMeshProUGUI takeMoney;
    [SerializeField] private int bonusMoney;
    [SerializeField] private Button next;
    private int lemon = 0, apple = 0, banana = 0;
    private string result;
    public int attempt = 0;
    private bool checkForEnd = true;
    private System.Random rand = new System.Random();


    private void Start()
    {
        int temp = 0;
        for (int i = 0; i < bonusText.Count; i++)
        {
            temp = RandomNumber();
            switch (temp)
            {
                case 1:
                    {
                        bonusText[i].text = "LEMON";
                        break;
                    }
                case 2:
                    {
                        bonusText[i].text = "APPLE";
                        break;
                    }
                default:
                    {
                        bonusText[i].text = "BANANA";
                        break;
                    }
            }
        }

        StartCoroutine(CheckForWin());
    }

    private int RandomNumber()
    {
        int temp = 0;
        temp = rand.Next(1, 4);
        if (temp == 1 && lemon != 3)
        {
            lemon++;
            return temp;
        }
        else if (temp == 2 && apple != 3)
        {
            apple++;
            return temp;
        }
        else if (temp == 3 && banana != 3)
        {
            banana++;
            return temp;
        }
        else
        {
            return RandomNumber();
        }
    }

    private IEnumerator CheckForWin()
    {
        while (checkForEnd)
        {
            yield return new WaitForFixedUpdate();
            if (attempt == 3)
            {
                for (int i = 0; i < BlockButton.Count; i++)
                {
                    if (BlockButton[i].gameObject.activeSelf)
                    {
                        BlockButton[i].enabled = false;
                    }
                    else
                    {
                        result += bonusText[i].text[0];
                    }

                }

                if (result == "BBB" || result == "AAA" || result == "LLL")
                {
                    takeMoney.text = bonusMoney.ToString();
                    takeMoney.gameObject.SetActive(true);
                    PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + bonusMoney);
                    next.gameObject.SetActive(true);
                    checkForEnd = false;
                }
                else
                {
                    takeMoney.text = "0";
                    takeMoney.gameObject.SetActive(true);
                    next.gameObject.SetActive(true);
                    checkForEnd = false;
                }
            }
        }
    }
}
