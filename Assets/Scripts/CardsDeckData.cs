using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScribtableObjects/CardsDeckData")]
public class CardsDeckData : ScriptableObject
{
    public Texture2D cardsSprite;
    public GameObject cardPrefab;
    public List<CardData> cardDatas;
}