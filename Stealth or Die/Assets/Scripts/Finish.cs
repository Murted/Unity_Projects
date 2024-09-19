using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField]
    private Collider finish;
    [SerializeField]
    private Collider player;
    [SerializeField]
    private TextMeshProUGUI winText;
    [SerializeField]
    private TextMeshProUGUI loseText;
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private List<PatrolEnemy> enemies;

    void Start()
    {
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (IsColliding(finish, player))
        {
            WinGame();
        }
        if(timer.time == 0)
        {
            LoseGame();
        }
        foreach(var enemy in enemies) 
        {
            if(enemy.lose)
            {
                LoseGame();
            }
        }
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        winText.gameObject.SetActive(true);
    }

    public void LoseGame()
    {
        Time.timeScale = 0f;
        loseText.gameObject.SetActive(true);
    }

    bool IsColliding(Collider collider1, Collider collider2)
    {
        return collider1.bounds.Intersects(collider2.bounds);
    }
}
