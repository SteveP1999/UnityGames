using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        for(int i = 0; i < cardSets.Count; i++)
        {
            if (cardSets[i].getAge() == profileManager.profiles[profileManager.getActivePlayerIndex()].getAge() || cardSets[i].getAge() == "KN" || cardSets[i].getAge() == "N" || cardSets[i].getAge() == "K")
            {
                possibleSets.Add(cardSets[i]);
            }
        }

        var rand = new System.Random();
        int number = rand.Next(possibleSets.Count);
        return possibleSets[number].getCardSetName();
    }
}
