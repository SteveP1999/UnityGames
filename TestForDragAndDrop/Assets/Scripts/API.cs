using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System;
using UnityEngine.Networking;

public class API : MonoBehaviour
{
    public static API api;
    public CardManager cardManager;
    public CardSetManager cardSetManager;
    public DataFromAPI data;
    testFORJSON jsonTEST;
    int gameId;
    int userId;
    string token;
    string path;

    public void Awake()
    {
        if (api == null)
        {
            DontDestroyOnLoad(gameObject);
            api = this;
        }
        else if (api != this)
        {
            Destroy(gameObject);
        }

        //string httpsLink = Application.absoluteURL;
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
        string path = data.config + data.gameID;
        jsonTEST = new testFORJSON();
        StartCoroutine(getData("asd"));
    }

    public IEnumerator getData(string _path)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get("https://laravel.etalonapps.hu/api/games/config/13"))
        {
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
            {
                Debug.Log("Error: " + unityWebRequest.error);
            }
            else
            {
                Debug.Log("Recieved: " + unityWebRequest.downloadHandler.text);
                string json = unityWebRequest.downloadHandler.text;
                data = JsonUtility.FromJson<DataFromAPI>(json);
                data.gameID = gameId;
                data.userID = userId;
                data.token = token;
                data.config = path;
                StartCoroutine(getCardSetJSON(data.assets[1].path));
                StartCoroutine(getCardsJSON(data.assets[2].path));
            }
        }
    }

    public IEnumerator getCardsJSON(string path)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(path))
        {
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
            {
                Debug.Log("Error: " + unityWebRequest.error);
            }
            else
            {
                Debug.Log("Recieved in getcards: " + unityWebRequest.downloadHandler.text);
                string json = unityWebRequest.downloadHandler.text;
                json = json.Remove(0, 1);
                jsonTEST = JsonUtility.FromJson<testFORJSON>(json);
                for (int i = 0; i < jsonTEST.cards.Length; i++)
                    cardManager.addCard(jsonTEST.cards[i]);
            }
        }
    }

    public IEnumerator getCardSetJSON(string path)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(path))
        {
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.isNetworkError || unityWebRequest.isHttpError)
            {
                Debug.Log("Error: " + unityWebRequest.error);
            }
            else
            {
                Debug.Log("Recieved in card set: " + unityWebRequest.downloadHandler.text);
                string json = unityWebRequest.downloadHandler.text;
                json = json.Remove(0, 1);
                jsonTEST = JsonUtility.FromJson<testFORJSON>(json);
                for (int i = 0; i < jsonTEST.cardSet.Length; i++)
                    cardSetManager.addCardSet(jsonTEST.cardSet[i]);
            }
        }
    }

    public void loadRemainingData()
    {

    }






    public testFORJSON getCards(string path)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        Debug.Log("Json in getCards: " + json);
        return JsonUtility.FromJson<testFORJSON>(json);
    }
}
