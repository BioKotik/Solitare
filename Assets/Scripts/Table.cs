using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour, IService
{
    public List<CardsColumn> columns;
    public DeckComponent deck;

    private void Awake()
    {
        ServiceLocator.I.RegisterSingle<Table>(this);
    }

    public void InitTable(List<CardComponent> cards)
    {
        int totalCards = 0;

        for (int i = 0; i < columns.Count; i++)
        {
            columns[i].InitColumn(cards.GetRange(totalCards, i + 1));

            totalCards += i + 1;
        }

        deck.InitDeck(cards.GetRange(totalCards, cards.Count - totalCards));
    }
}