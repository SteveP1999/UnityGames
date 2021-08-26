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

    public void loadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}
