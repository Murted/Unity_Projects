using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] public int amount;
    [SerializeField] private Text text;

    private void Update()
    {
        text.text = amount.ToString();
    }
}
