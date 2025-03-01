using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int time;
    [SerializeField]
    private Move player;

    private TextMeshProUGUI timeText;


    void Start()
    {
        timeText = GetComponent<TextMeshProUGUI>();
        timeText.text = time.ToString();
        StartCoroutine(TikTak());
    }

    IEnumerator TikTak()
    {
        while (time != 0)
        {
            yield return new WaitForSeconds(1);
            if (player.isRevealed)
            {
                break;
            }

            time--;
            timeText.text = time.ToString();

            if (time == 0)
            {
                Time.timeScale = 0;
                break;
            }
        }
    }
}
