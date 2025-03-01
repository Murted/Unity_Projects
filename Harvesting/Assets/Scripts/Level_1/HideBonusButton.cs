using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideBonusButton : MonoBehaviour
{
    [SerializeField] private RandomBonus bonus;
    public void Hide()
    {
        gameObject.SetActive(false);
        bonus.attempt++;
    }
}
