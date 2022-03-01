using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class contains the data of a card
[System.Serializable]
public class Card
{
    #region Variables

    //The id of the card but not in its own set but all the cards
    [SerializeField] private int uniqueId;

    //Name of the card
    [SerializeField] private string cardName;

    //The value of the card in its set
    [SerializeField] private int cardId;

    //Name of the cardset of the card
    [SerializeField] private string cardSet;
    #endregion


    //Loads in data form a json file
    public void loadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }



    //Getters and setters for the variables
    public string getCardName()
    {
        return cardName;
    }

    public int getUniqueId()
    {
        return uniqueId;
    }

    public int getCardId()
    {
        return cardId;
    }

    public string getCardSet()
    {
        return cardSet;
    }
}
