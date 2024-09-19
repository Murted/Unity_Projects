using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveTrack : MonoBehaviour
{
    [SerializeField] private GameSceneController gameSceneController;

    private void OnTriggerEnter(Collider fruit)
    {
        if (fruit.gameObject.tag == "Fruit")
        {
            if (fruit.gameObject.GetComponent<FruitNumber>().fruitNumber != gameSceneController.basket)
            {
                gameSceneController.reason.text = "You have picked a wrong fruit";
                gameSceneController.game = false;
            }
            else
            {
                Destroy(fruit.gameObject);
                gameSceneController.selected++;
            }
        }
    }
}
