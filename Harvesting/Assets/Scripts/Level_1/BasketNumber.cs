using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasketNumber : MonoBehaviour
{
    [SerializeField] public int basketNumber;
    [SerializeField] private GameSceneController gameSceneController;
    [SerializeField] private Button button;
    [SerializeField] private List<Button> enableButtons;

    public void ChangeBasket()
    {
        gameSceneController.basket = basketNumber;
        button.image.color = Color.gray;
        for (int i = 0; i < enableButtons.Count; i++)
        {
            enableButtons[i].enabled = true;
            enableButtons[i].image.color = Color.white;
        }
    }
}
