using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CardsDeckData))]
public class CardDeckEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        CardsDeckData deck = (CardsDeckData)target;

        if (GUILayout.Button("Generate Deck"))
        {
            deck.cardDatas.Clear();

            GenerateDeck(deck);
        }

        EditorUtility.SetDirty(target);
    }

    public void GenerateDeck(CardsDeckData deck)
    {
        Sprite[] sprites = GetSprites(deck.cardsSprite);

        int suitLength = Enum.GetNames(typeof(CardSuit)).Length;
        int rankLength = Enum.GetNames(typeof(CardRank)).Length;
        for (int suitIndex = 0; suitIndex < suitLength; suitIndex++)
        {          
            for (int rankIndex = 0; rankIndex < rankLength; rankIndex++)
            {
                deck.cardDatas.Add(new CardData((CardSuit)suitIndex, (CardRank)rankIndex, sprites[rankIndex + suitIndex * rankLength]));
            }
        }

    }

    public Sprite[] GetSprites(Texture2D texture)
    {
        string spriteSheet = AssetDatabase.GetAssetPath(texture);
        Sprite[] sprites = AssetDatabase.LoadAllAssetsAtPath(spriteSheet).OfType<Sprite>().ToArray();

        return sprites;
    }
}