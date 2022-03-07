using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    #region Variables
    public testFORJSON jsonContainer = new testFORJSON();
    public CardManager cardManager;
    public CardSetManager cardSetManager;
    [SerializeField] private DataFromAPI data;
    [SerializeField] private string cardsPath1 = @"C:\Users\SteveP1\Desktop\egyetem\unity\KártyákJSON\CardsJSON.json";
    [SerializeField] private string cardsPath2 = @"C:\Users\SteveP1\Desktop\egyetem\unity\KártyákJSON\CardSetJSON.json";
    public static JSONReader jsonReader;
    #endregion

    void Awake()
    {
        if (jsonReader == null)
        {
            DontDestroyOnLoad(gameObject);
            jsonReader = this;
        }
        else if (jsonReader != this)
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        string URL = Application.absoluteURL;

        Debug.Log(URL);

        data = API.getData();

        jsonContainer = API.getCardsJSON(data.assets[1].path);

        for (int i = 0; i < jsonContainer.cardSet.Length; i++)
            cardSetManager.addCardSet(jsonContainer.cardSet[i]);

        jsonContainer = API.getCardSetJSON(data.assets[2].path);

        for (int i = 0; i < jsonContainer.cards.Length; i++)
            cardManager.cards.Add(jsonContainer.cards[i]);
    }

    public int getGameMode()
    {
        return data.chosenGameMode;
    }
}