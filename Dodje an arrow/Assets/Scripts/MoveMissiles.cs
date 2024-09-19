using System.Collections;
using UnityEngine;

public class MoveMissiles : MonoBehaviour
{
    [SerializeField] private SpawnMissiles spawnMisseles;

    private Vector2 rightDirection = new Vector2(1, 0);
    private Vector2 leftDirection = new Vector2(-1, 0);

    public IEnumerator Movement(float frameTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(frameTime);

            for (int i = 0; i < spawnMisseles.currentMissiles.Count; i++)
            {
                for (int j = 0; j < spawnMisseles.currentMissiles[i].sampleList.Count; j++)
                {
                    if (spawnMisseles.storage[i].rightSide)
                    {
                        spawnMisseles.currentMissiles[i].sampleList[j].transform.Translate(rightDirection * spawnMisseles.storage[i].speed);
                    }
                    else
                    {
                        spawnMisseles.currentMissiles[i].sampleList[j].transform.Translate(leftDirection * spawnMisseles.storage[i].speed);
                    }
                }
            }
        }
    }
}
