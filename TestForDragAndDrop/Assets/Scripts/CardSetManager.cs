using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class contains the list of cardSets
[System.Serializable]
public class CardSetManager : MonoBehaviour
{
    [SerializeField] public List<CardSet> cardSets;
    [SerializeField] private ProfileManager profileManager;
    public static CardSetManager cardSetManager;

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
        string age = "K";
        if (profileManager.profiles[profileManager.getActivePlayerIndex()].getAge() == "old")
        {
            age = "N";
        }
        for(int i = 0; i < cardSets.Count; i++)
        {
            if ((cardSets[i].getAge() == age || cardSets[i].getAge() == "KN") && cardSets[i].getCardSetName() != "Kids" && cardSets[i].getCardSetName() != "Fruit" && cardSets[i].getCardSetName() != "Animal" && cardSets[i].getCardSetName() != "Island" && cardSets[i].getCardSetName() != "Hut")
            {
                possibleSets.Add(cardSets[i]);
                Debug.Log("Egy lehetséges set: " + cardSets[i].getCardSetName());
            }
        }

        var rand = new System.Random();
        int number = rand.Next(possibleSets.Count);
        Debug.Log(possibleSets[number].getCardSetName());
        return possibleSets[number].getCardSetName();
    }
}
