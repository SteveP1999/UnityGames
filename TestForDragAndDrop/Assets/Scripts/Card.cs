using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class contains the data of a card
[System.Serializable]
public class Card
{
    [SerializeField] private int uniqueId;

    [SerializeField] private string cardName;

    //The value of the card in its set
    [SerializeField] private int cardId;

    [SerializeField] private string cardSet;



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
