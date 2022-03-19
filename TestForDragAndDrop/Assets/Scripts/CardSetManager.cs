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

        switch (GameData.instance.getGameID())
        {
            case 1:
                for (int i = 0; i < cardSets.Count; i++)
                {
                    if ((cardSets[i].getAge() == age && cardSets[i].getSize() >= 14) || (cardSets[i].getAge() == "KN" && cardSets[i].getSize() >= 14))  //New arrival
                    {
                        possibleSets.Add(cardSets[i]);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < cardSets.Count; i++)
                {
                    if ((cardSets[i].getAge() == age && cardSets[i].getSize() >= 10) || (cardSets[i].getAge() == "KN" && cardSets[i].getSize() >= 10))    //PairGame
                    {
                        possibleSets.Add(cardSets[i]);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < cardSets.Count; i++)
                {
                    if ((cardSets[i].getAge() == age && cardSets[i].getSortable() == true) || (cardSets[i].getAge() == "KN" && cardSets[i].getSortable() == true))  //OrderGame
                    {
                        possibleSets.Add(cardSets[i]);
                    }
                }
                break;
            default:
                Debug.Log("No such case as given");
                break;
        }

        //for(int i = 0; i < possibleSets.Count; i++)
        //{
        //    Debug.Log("Egy lehetséges set: " + possibleSets[i].getCardSetName());
        //}

        var rand = new System.Random();
        int number = rand.Next(possibleSets.Count);
        return possibleSets[number].getCardSetName();
    }
}
