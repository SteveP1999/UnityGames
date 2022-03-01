﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    #region Variables
    public testFORJSON jsonContainer = new testFORJSON();
    public CardManager cardManager;
    public CardSetManager cardSetManager;
    private DataFromAPI data;
    [SerializeField] private string cardsPath1 = @"C:\Users\SteveP1\Desktop\egyetem\unity\KártyákJSON\CardsJSON.json";
    [SerializeField] private string cardsPath2 = @"C:\Users\SteveP1\Desktop\egyetem\unity\KártyákJSON\CardSetJSON.json";
    #endregion


    public void Start()
    {
        //using (StreamReader stream = new StreamReader(cardsPath1))
        //{
        //    string json = stream.ReadToEnd();
        //    jsonContainer = JsonUtility.FromJson<testFORJSON>(json);
        //}

        //for (int i = 0; i < jsonContainer.cards.Length; i++)
        //    cardManager.cards.Add(jsonContainer.cards[i]);

        //using (StreamReader stream = new StreamReader(cardsPath2))
        //{
        //    string json = stream.ReadToEnd();
        //    jsonContainer = JsonUtility.FromJson<testFORJSON>(json);
        //}
        //for (int i = 0; i < jsonContainer.cardSet.Length; i++)
        //    cardSetManager.addCardSet(jsonContainer.cardSet[i]);

        data = API.getData();

        jsonContainer = API.getCardsJSON(data.assets[1].path);

        for (int i = 0; i < jsonContainer.cardSet.Length; i++)
            cardSetManager.addCardSet(jsonContainer.cardSet[i]);

        jsonContainer = API.getCardSetJSON(data.assets[2].path);

        for (int i = 0; i < jsonContainer.cards.Length; i++)
            cardManager.cards.Add(jsonContainer.cards[i]);
    }
}