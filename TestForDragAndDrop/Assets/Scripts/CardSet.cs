using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class contains the data of a cardSet
[System.Serializable]
public class CardSet
{
    [SerializeField] private string cardSetName;
    [SerializeField] private bool sortable;
    [SerializeField] private string age;

    public string getCardSetName()
    {
        return cardSetName;
    }

    public bool getSortable()
    {
        return sortable;
    }

    public string getAge()
    {
        return age;
    }

    public void loadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}
