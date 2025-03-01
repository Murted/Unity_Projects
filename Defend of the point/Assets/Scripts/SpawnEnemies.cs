using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField] private float xEdge;
    [SerializeField] private float yEdge;
    [SerializeField] private GameObject enemy;
    [SerializeField] public List<GameObject> enemies;
    [SerializeField] private float timeSpawn;
    [SerializeField] private float boardX;
    [SerializeField] private float boardY;
    [SerializeField] private GameObject gameOver;

    private System.Random rand = new System.Random();
    private double x, y;
    private bool isSpawn;

    private void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(CheckCenter());
    }
    public IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeSpawn);
            isSpawn = false;

            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies.Count == 0)
                {
                    break;
                }
                if (!enemies[i].activeSelf)
                {
                    isSpawn = true;
                    enemies[i].transform.position = Position();
                    enemies[i].SetActive(true);
                    break;
                }
            }

            if (!isSpawn)
            {
                GameObject obj = Instantiate(enemy);
                enemies.Add(obj);
                obj.transform.position = Position();
            }
        }
    }

    public IEnumerator CheckCenter()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i].transform.position == new Vector3(0, 0) && enemies[i].activeSelf)
                {
                    Time.timeScale = 0f;
                    for (int j = 0; j < enemies.Count; j++)
                    {
                        enemies[j].SetActive(false);
                    }
                    gameOver.SetActive(true);
                }
            }
        }
    }

    private Vector2 Position()
    {
        x = (rand.NextDouble() * (xEdge - (-xEdge)) + (-xEdge));
        y = (rand.NextDouble() * (yEdge - (-yEdge)) + (-yEdge));
        if ((x < boardX && x > -boardX) && (y < boardY && y > -boardY))
        {
            while ((x < boardX && x > -boardX) && (y < boardY && y > -boardY))
            {
                x = (rand.NextDouble() * (xEdge - (-xEdge)) + (-xEdge));
                y = (rand.NextDouble() * (yEdge - (-yEdge)) + (-yEdge));
            }
        }

        return new Vector2((float)x, (float)y);
    }
}
