using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour, IService
{
    public List<CardComponent> cards;
    public Canvas canvas;
    private void Awake()
    {
        ServiceLocator.I.RegisterSingle<CardDeck>(this);
    }

    private void Start()
    {
        CreateDeck();
        ShuffleDeck();

        ServiceLocator.I.Single<Table>().InitTable(cards);
    }

    private void CreateDeck()
    {
        cards = ServiceLocator.I.Single<CardsFactory>().CreateDeck(canvas.transform);
    }

    private void ShuffleDeck()
    {
        int count = cards.Count - 1;

        while (count > 0)
        {
            int index = Random.Range(0, count);
            CardComponent tempCard = cards[count];
            cards[count] = cards[index];
            cards[index] = tempCard;
            count--;
        }
    }    
}