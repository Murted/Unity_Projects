using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Naninovel.Commands;
using UnityEngine.SceneManagement;
using Naninovel;

public class MemoryGame : MonoBehaviour
{
    [SerializeField]
    private List<Card> cards = new List<Card>();
    [SerializeField]
    private Sprite cardBackSprite;
    [SerializeField]
    private List<Sprite> cardFrontSprites;
    [SerializeField]
    private Text victory;
    [SerializeField]
    private int waitTime;

    public bool isChecking = false;

    private Card firstCard, secondCard;
    private int matchCount = 0;

    void Start()
    {
        SetupGame();
    }

    void SetupGame()
    {
        foreach (var card in cards)
        {
            card.cardImage.sprite = cardBackSprite;
        }

        List<Sprite> frontSprites = new List<Sprite>();
        foreach (var sprite in cardFrontSprites)
        {
            frontSprites.Add(sprite);
            frontSprites.Add(sprite);
        }

        ShuffleList(frontSprites);

        for (int i = 0; i < cards.Count; i++)
        {
            Card card = cards[i];
            card.frontSprite = frontSprites[i];
        }
    }

    void ShuffleList(List<Sprite> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            Sprite temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void CardClicked(Card clickedCard)
    {
        if (firstCard == null)
        {
            firstCard = clickedCard;
            firstCard.FlipCard();
        }
        else
        {
            secondCard = clickedCard;
            secondCard.FlipCard();
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        isChecking = true;
        yield return new WaitForSeconds(1);

        if (firstCard.frontSprite == secondCard.frontSprite)
        {
            firstCard = null;
            secondCard = null;
            matchCount++;
            if (matchCount == cards.Count / 2)
            {
                victory.gameObject.SetActive(true);
                StartCoroutine(Waiter());
            }
        }
        else
        {
            firstCard.FlipBack();
            secondCard.FlipBack();
            firstCard = null;
            secondCard = null;
        }

        isChecking = false;
    }

    IEnumerator Waiter()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("TitleScene");
    }

}