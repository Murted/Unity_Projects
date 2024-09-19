using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    [Header("Links")]
    [SerializeField] SpawnMissiles spawnMissiles;
    [SerializeField] MoveMissiles moveMissiles;
    [SerializeField] float frameTime;
    [SerializeField] Text score;

    private int timeScore = 0;

    private void Awake()
    {
        score.text = "Score: " + timeScore.ToString();
        StartCoroutine(spawnMissiles.Spawn());
        StartCoroutine(spawnMissiles.Move());
        StartCoroutine(moveMissiles.Movement(frameTime));
        StartCoroutine(Score());
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private IEnumerator Score()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            score.text = "Score: " + timeScore.ToString();
            timeScore++;
        }
    }
}
