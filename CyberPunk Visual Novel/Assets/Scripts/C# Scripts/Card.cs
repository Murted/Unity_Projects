using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    public Image cardImage;
    public Sprite frontSprite;
    public Sprite backSprite;
    public Button cardButton;
    public bool isFlipped = false;

    private MemoryGame gameManager;

    private void Start()
    {
        cardButton.onClick.AddListener(OnCardClicked);
        cardImage.sprite = backSprite;
        gameManager = FindObjectOfType<MemoryGame>();
    }

    public void OnCardClicked()
    {
        if (isFlipped || gameManager.isChecking) return;

        FlipCard();
        gameManager.CardClicked(this);
    }

    public void FlipCard()
    {
        cardImage.sprite = frontSprite;
        isFlipped = true;
    }

    public void FlipBack()
    {
        cardImage.sprite = backSprite;
        isFlipped = false;
    }
}