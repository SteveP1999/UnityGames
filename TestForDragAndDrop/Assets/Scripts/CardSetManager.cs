using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardSetManager : MonoBehaviour
{
    [SerializeField] private List<CardSet> cardSets;

    public void addCardSet(CardSet cardset)
    {
        cardSets.Add(cardset);
    }
}
