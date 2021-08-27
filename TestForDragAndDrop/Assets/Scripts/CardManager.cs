using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public List<string> cardList1 = new List<string>();
    public List<int> cardList1Ids = new List<int>();
    public List<string> cardList2 = new List<string>();
    public List<int> cardList2Ids = new List<int>();
    public static CardManager cardManager;

    void Awake()
    {
        if (cardManager == null)
        {
            DontDestroyOnLoad(gameObject);
            cardManager = this;
        }
        else if (cardManager != this)
        {
            Destroy(gameObject);
        }
    }

    public void addCard(Card card)
    {
        cards.Add(card);
    }

    public void drawDifferentCard(string cardSetName)
    {
        List<Card> possibleCards = new List<Card>();
        for(int i = 0; i < cards.Count; i++)
        {
            if(cards[i].getCardSet() == cardSetName)
            {
                possibleCards.Add(cards[i]);
                Debug.Log("Lehetséges kártyák száma: " + possibleCards.Count);
            }
        }
        while(true)
        {
            var rand = new System.Random();
            int number = rand.Next(possibleCards.Count);
            if(!cardList1.Contains(possibleCards[number].getCardName()))
            {
                cardList1.Add(possibleCards[number].getCardName());
                cardList1Ids.Add(possibleCards[number].getUniqueId());
            }
        }
    }

    public void drawDifferentCards(int gameLevel, string cardSetName, bool whichList)  //true = cardList1 
    {
        List<Card> possibleCards = new List<Card>();
        List<int> numberDrawn = new List<int>();
        for (int i = 0; i < cards.Count; i++)
        {
            if (cards[i].getCardSet() == "Francia")   //cardSetName helyett van most a Francia
            {
                possibleCards.Add(cards[i]);
            }
        }
        int j = 0;
        while(j != gameLevel)
        {
            var rand = new System.Random();
            int number = rand.Next(possibleCards.Count);
            if(!numberDrawn.Contains(number))
            {
                numberDrawn.Add(number);
                if(whichList == true)
                {
                    cardList1.Add(possibleCards[number].getCardName());
                    cardList1Ids.Add(possibleCards[number].getUniqueId());
                }
                else
                {
                    cardList2.Add(possibleCards[number].getCardName());
                    cardList2Ids.Add(possibleCards[number].getUniqueId());
                }
                j++;
            }
        }
    }
}
