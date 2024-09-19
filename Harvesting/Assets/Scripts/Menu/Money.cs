using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCoin;
    public static int coins;

    private void Update()
    {
        coins = PlayerPrefs.GetInt("Coins");
        textCoin.text = coins.ToString();
    }

}
