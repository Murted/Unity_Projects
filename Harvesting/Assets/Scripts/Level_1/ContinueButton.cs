using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] private GameObject random;
    [SerializeField] private GameObject victory;

    public void ContinueToPlay()
    {
        victory.SetActive(false);
        random.SetActive(true);
    }
}
