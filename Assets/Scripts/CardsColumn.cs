using System.Collections.Generic;
using UnityEngine;

public class CardsColumn : MonoBehaviour
{
    private List<CardComponent> cards;

    private void Awake()
    {
        cards = new List<CardComponent>();
    }

    public void InitColumn(List<CardComponent> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            bool isCardActive = i == cards.Count - 1;
            cards[i].SetCardActive(isCardActive);

            cards[i].transform.SetParent(transform);
            this.cards.Add(cards[i]);
        }
    }

    public void AddCard(CardComponent card)
    {
        cards.Add(card);
    }

    public void RemoveCard()
    {
        cards.RemoveAt(cards.Count - 1);
    }

    public bool IsAbleToStack(CardComponent card)
    {
        if (cards.Count == 0)
        {
            if (card.cardData.cardRank.Equals(CardRank.King))
            {
                return true;
            }

            return false;
        }

        CardComponent lastCard = cards[cards.Count - 1];

        var isLowerRank = card.cardData.cardRank == lastCard.cardData.cardRank - 1;
        var isDifferentColorSuit = (int)card.cardData.cardSuit % 2 != (int)lastCard.cardData.cardSuit % 2;
    
        return isLowerRank && isDifferentColorSuit;
    }

    public void ShowLastCard()
    {
        if (cards.Count > 0)
        {
            cards[cards.Count - 1].SetCardActive(true);
        }
    }   
}

