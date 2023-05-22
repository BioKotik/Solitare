using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckComponent : MonoBehaviour
{
    List<CardComponent> cards;

    public void InitDeck(List<CardComponent> cards)
    {
        this.cards = new List<CardComponent>();

        this.cards.AddRange(cards);

        foreach (CardComponent card in cards)
        {
            card.transform.SetParent(transform);
            RectTransform rt = card.GetComponent<RectTransform>();
            rt.offsetMax = Vector3.zero;
            rt.offsetMin = Vector3.zero;
        }
    }
}
