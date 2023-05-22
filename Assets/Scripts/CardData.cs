using System;
using UnityEngine;

[Serializable]
public class CardData
{
    public CardSuit cardSuit;
    public CardRank cardRank;
    public Sprite cardImage;

    public CardData(CardSuit cardSuit, CardRank cardRank, Sprite cardImage)
    {
        this.cardSuit = cardSuit;
        this.cardRank = cardRank;
        this.cardImage = cardImage;
    }
}

