using System.Collections.Generic;
using UnityEngine;

public class CardsFactory : MonoBehaviour, IService
{
    public CardsDeckData deck;

    private void Awake()
    {
        ServiceLocator.I.RegisterSingle<CardsFactory>(this);
    }

    public List<CardComponent> CreateDeck(Transform parent = null)
    {
        List<CardComponent> cards = new List<CardComponent>();

        foreach (CardData data in deck.cardDatas)
        {
            CardComponent card = CreateCard(data, parent);
            cards.Add(card);
        }

        return cards;
    }

    private CardComponent CreateCard(CardData data, Transform parent = null)
    {
        var cardObj = Instantiate(deck.cardPrefab, parent);
        var card = cardObj.GetComponent<CardComponent>();
        card.Set(data);

        cardObj.name = $"{card.cardData.cardRank}Of{card.cardData.cardSuit}";

        return card;
    }
}

