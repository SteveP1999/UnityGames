using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class contains all the cards and ids and values drawn for a certain game 
public class CardManager : MonoBehaviour
{
    #region Variables
    //Cards drawn for the game
    public List<Card> cards = new List<Card>();

    public List<Card> containerOfCards1 = new List<Card>();

    public List<Card> containerOfCards2 = new List<Card>();

    public List<int> normalIds = new List<int>();

    public List<string> cardList1 = new List<string>();

    public List<int> cardList1Ids = new List<int>();

    public List<string> cardList2 = new List<string>();

    public List<int> cardList2Ids = new List<int>();

    public static CardManager cardManager;
    #endregion


    //This function guarantees that only one CardManager exists and that it can't be destroyed when switching scenes
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


    //Adds a card to the list
    public void addCard(Card card)
    {
        cards.Add(card);
    }

    //Draw a card for the game that is unique (used in the new arrival game)
    public void drawDifferentCard(string cardSetName)
    {
        List<Card> possibleCards = new List<Card>();
        for(int i = 0; i < cards.Count; i++)
        {
            if(cards[i].getCardSet() == cardSetName)
            {
                possibleCards.Add(cards[i]);
            }
        }

        int k = 0;
        while(k != 1)
        {
            var rand = new System.Random();
            int number = rand.Next(possibleCards.Count);
            if(!cardList1.Contains(possibleCards[number].getCardName()))
            {
                containerOfCards1.Add(possibleCards[number]);

                cardList1.Add(possibleCards[number].getCardName());
                cardList1Ids.Add(possibleCards[number].getUniqueId());
                k++;
            }
        }
    }

    //Draw the cards equal to the gameLevel and set up their values
    public void drawDifferentCards(int gameLevel, string cardSetName, bool whichList)  //true = cardList1 
    {
        List<Card> possibleCards = new List<Card>();
        List<int> numberDrawn = new List<int>();

        for (int i = 0; i < cards.Count; i++)
        {
            if(cards[i].getCardSet() == cardSetName)
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
                Debug.Log(possibleCards[number].getCardName());
                numberDrawn.Add(number);
                if(whichList == true)
                {
                    containerOfCards1.Add(possibleCards[number]);

                    cardList1.Add(possibleCards[number].getCardName());
                    cardList1Ids.Add(possibleCards[number].getUniqueId());
                    normalIds.Add(possibleCards[number].getCardId());
                }
                else
                {
                    containerOfCards2.Add(possibleCards[number]);

                    cardList2.Add(possibleCards[number].getCardName());
                    cardList2Ids.Add(possibleCards[number].getUniqueId());
                    normalIds.Add(possibleCards[number].getCardId());
                }
                j++;
            }
        }
    }
}
