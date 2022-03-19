using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class contains the data of a cardSet
[System.Serializable]
public class CardSet
{
    #region Variables
    [SerializeField] private string cardSetName;
    [SerializeField] private bool sortable;
    [SerializeField] private string age;
    [SerializeField] private int size;
    #endregion

    #region Getters / Setters
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

    public int getSize()
    {
        return size;
    }
    #endregion

    public void loadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}
