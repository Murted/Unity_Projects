using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMissiles : MonoBehaviour
{
    [SerializeField] public List<MissileStorage> storage;
    [System.Serializable]
    public class ListClass
    {
        public List<GameObject> sampleList;
    }
    [SerializeField] public List<ListClass> currentMissiles = new List<ListClass>();
    [SerializeField] private float timeSpawn;
    [Header("Position")]
    [SerializeField] private float rightX;
    [SerializeField] private float leftX;
    [SerializeField] private float upperY;
    [SerializeField] private float middleY;
    [SerializeField] private float lowerY;
    [SerializeField] private float rightEdgeX;
    [SerializeField] private float leftEdgeX;

    private bool isSpawned;
    private System.Random rand = new System.Random();
    private int positionNumber;

    public IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSpawn);
            for (int i = 0; i < storage.Count; i++)
            {
                isSpawned = false;

                for (int j = 0; j < currentMissiles[i].sampleList.Count; j++)
                {
                    if (currentMissiles[i].sampleList.Count == 0)
                    {
                        break;
                    }
                    if (!currentMissiles[i].sampleList[j].activeSelf)
                    {
                        isSpawned = true;
                        currentMissiles[i].sampleList[j].transform.position = Position(storage[i].rightSide);
                        currentMissiles[i].sampleList[j].SetActive(true);
                        break;
                    }
                }

                if (!isSpawned)
                {
                    GameObject obj = storage[i].CreateObject();
                    currentMissiles[i].sampleList.Add(obj);
                    obj.transform.position = Position(storage[i].rightSide);
                }                
            }
        }
    }

    public IEnumerator Move()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < currentMissiles.Count; i++)
            {
                for (int j = 0; j < currentMissiles[i].sampleList.Count; j++)
                {
                    if (storage[i].rightSide)
                    {
                        if (currentMissiles[i].sampleList[j].transform.position.x >= rightEdgeX && storage[i].rightSide)
                        {
                            currentMissiles[i].sampleList[j].SetActive(false);
                        }
                    }
                    else
                    {
                        if (currentMissiles[i].sampleList[j].transform.position.x <= leftEdgeX)
                        {
                            currentMissiles[i].sampleList[j].SetActive(false);
                        }
                    }
                }
            }
        }
    }

    private Vector2 Position(bool side)
    {
        positionNumber = rand.Next(1, 4);

        if (!side)
        {
            switch (positionNumber)
            {
                case 1:
                    return new Vector2(rightX, upperY);
                case 2:
                    return new Vector2(rightX, middleY);
                default:
                    return new Vector2(rightX, lowerY);
            }
        }

        else
        {
            switch (positionNumber)
            {
                case 1:
                    return new Vector2(leftX, upperY);
                case 2:
                    return new Vector2(leftX, middleY);
                default:
                    return new Vector2(leftX, lowerY);
            }
        }
    }
}