using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class JSONReader : MonoBehaviour
{
    #region Variables
    public testFORJSON jsonContainer = new testFORJSON();
    public CardManager cardManager;
    public API api;
    public CardSetManager cardSetManager;
    [SerializeField] private DataFromAPI data;
    public static JSONReader jsonReader;
    int userId = 0;
    int gameId = 0;
    string token;
    string path;
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

        string httpsLink = "https://laravel.etalonapps.hu/games/default/?user_id=973&game_id=1&token=0&config_url=https://laravel.etalonapps.hu/api/games/config/";
        string p = httpsLink.Split('?')[1];
        string user = p.Split('=')[1];
        string game = p.Split('=')[2];
        token = p.Split('=')[3].Split('&')[0];
        path = p.Split('=')[4];
        int.TryParse(game.Split('&')[0], out gameId);
        int.TryParse(user.Split('&')[0], out userId);
    }

    
    public void Start()
    {
        //string URL = Application.absoluteURL;
        //Debug.Log(URL);

        path = path + gameId;


        StartCoroutine(api.getData(path));
        data = api.data;

        //jsonContainer = API.getCardsJSON(data.assets[1].path);

        for (int i = 0; i < jsonContainer.cardSet.Length; i++)
            cardSetManager.addCardSet(jsonContainer.cardSet[i]);

        //jsonContainer = API.getCardSetJSON(data.assets[2].path);

        for (int i = 0; i < jsonContainer.cards.Length; i++)
            cardManager.cards.Add(jsonContainer.cards[i]);

        data.gameID = gameId;
        data.userID = userId;
        data.token = token;
        data.config = path;
    }
    

    public int getGameMode()
    {
        return data.chosenGameMode;
    }

    public DataFromAPI getData()
    {
        return data;
    }
}