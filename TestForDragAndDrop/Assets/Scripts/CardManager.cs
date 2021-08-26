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
}
