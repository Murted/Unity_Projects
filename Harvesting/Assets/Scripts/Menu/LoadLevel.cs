using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private int level;

    public void Load()
    {
        SceneManager.LoadScene(level);
    }
}
