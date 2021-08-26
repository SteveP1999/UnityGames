using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> cards = new List<Card>();

    public void addCard(Card card)
    {
        cards.Add(card);
    }

    public string drawCard(string cardSetName)
    {
        List<Card> possibleCards = new List<Card>();
        for(int i = 0; i < cards.Count; i++)
        {
            if(cards[i].getCardSet() == cardSetName)
            {
                possibleCards.Add(cards[i]);
            }
        }
        var rand = new System.Random();
        int number = rand.Next(possibleCards.Count);
        return cards[number].getCardName();
    }
}
