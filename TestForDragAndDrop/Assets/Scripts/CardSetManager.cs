using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class contains the list of cardSets
[System.Serializable]
public class CardSetManager : MonoBehaviour
{
    #region Variables
    [SerializeField] public List<CardSet> cardSets;
    [SerializeField] private ProfileManager profileManager;
    public static CardSetManager cardSetManager;
    #endregion

    void Awake()
    {
        if (cardSetManager == null)
        {
            DontDestroyOnLoad(gameObject);
            cardSetManager = this;
        }
        else if (cardSetManager != this)
        {
            Destroy(gameObject);
        }
    }

    public void addCardSet(CardSet cardset)
    {
        cardSets.Add(cardset);
    }

    public string drawAsset()
    {
        List<CardSet> possibleSets = new List<CardSet>();
        string age = "K";  //The starting age is kicsi
        if (profileManager.profiles[profileManager.getActivePlayerIndex()].getAge() == "old")
        {
            age = "N";  //If not Kicsi then Nagy
        }
        for(int i = 0; i < cardSets.Count; i++)
        {
            if (cardSets[i].getAge() == age || cardSets[i].getAge() == "KN")
            {
                possibleSets.Add(cardSets[i]);
            }
        }

        var rand = new System.Random();
        int number = rand.Next(possibleSets.Count);
        return possibleSets[number].getCardSetName();
    }
}
