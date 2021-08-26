using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Card
{
    [SerializeField] private int uniqueId;
    [SerializeField] private string cardName;
    [SerializeField] private int cardId;
    [SerializeField] private string cardSet;

    public string getCardName()
    {
        return cardName;
    }

    public void setUniqueId(int number)
    {
        uniqueId = number;
    }

    public void setCardName(string name)
    {
        cardName = name;
    }

    public void setCardId(int id)
    {
        cardId = id;
    }

    public void setCardSet(string set)
    {
        cardSet = set;
    }

    public void loadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}
