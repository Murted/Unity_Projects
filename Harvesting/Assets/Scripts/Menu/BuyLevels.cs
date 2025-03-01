using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyLevels : MonoBehaviour
{
    [SerializeField] string levelName;
    [SerializeField] private int price;
    [SerializeField] private int access;
    [SerializeField] private Button level;
    [SerializeField] private Button buy;
    [SerializeField] private Money coins;

    private void Update()
    {
        if (PlayerPrefs.GetInt(levelName + "Access") == 0)
        {
            level.enabled = false;
            level.image.color = Color.gray;
            if (PlayerPrefs.GetInt("Coins") < price)
            {
                buy.enabled = false;
            }
        }
        else
        {
            level.enabled = true;
            level.image.color = Color.white;
            gameObject.SetActive(false);
        }
    }

    public void BuyLevel()
    {
        if(access == 0)
        {
            if(Money.coins >= price)
            {
                PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") - price);
                PlayerPrefs.SetInt(levelName + "Access", 1);
                access = 1;
            }
        }
    }
}
