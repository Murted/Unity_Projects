using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnEnemies : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Score score;
    public void OnPointerClick(PointerEventData eventData)
    {
        gameObject.SetActive(false);
        score.amount++;
    }
}
