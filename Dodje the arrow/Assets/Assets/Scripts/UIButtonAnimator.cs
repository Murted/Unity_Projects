using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class UIButtonAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] private float hoverScale = 1.2f;
    [SerializeField] private float clickScale = 0.9f;
    [SerializeField] private float animationDuration = 0.2f;

    private Vector3 originalScale;

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void OnEnable()
    {
        transform.localScale = originalScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(originalScale * hoverScale, animationDuration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(originalScale, animationDuration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOScale(originalScale * clickScale, animationDuration / 2)
            .SetEase(Ease.InOutQuad)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                transform.DOScale(originalScale * hoverScale, animationDuration / 2).SetUpdate(true);
            });
    }
}
