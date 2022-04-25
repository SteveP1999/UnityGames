#define win 

using System.Collections;
using UnityEngine;
using System.Net;
using System.IO;
using UnityEngine.Networking;
using System.Text;

public class API : MonoBehaviour
{
    public GameObject MainMenu;
    public static API api;
    public CardManager cardManager;
    public CardSetManager cardSetManager;
    public DataFromAPI data;
    public DataToServer dataToServer;
    testFORJSON jsonTEST;
    int gameId;
    int userId;
    string token;
    string path;
    public static API instance;


    public void Awake()
    {
        instance = this;
        if (api == null)
        {
            DontDestroyOnLoad(gameObject);
            api = this;
        }
        else if (api != this)
        {
            Destroy(gameObject);
        }

    #if win
        string httpsLink = "https://laravel.etalonapps.hu/games/default/?user_id=973&game_id=2&token=0&config_url=https://laravel.etalonapps.hu/api/games/config/";
    #else
        string httpsLink = Application.absoluteURL;
    #endif
        string p = httpsLink.Split('?')[1];
        string user = p.Split('=')[1];
        string game = p.Split('=')[2];
        token = p.Split('=')[3].Split('&')[0];
        path = p.Split('=')[4];
        int.TryParse(game.Split('&')[0], out gameId);
        int.TryParse(user.Split('&')[0], out userId);
        data.gameID = gameId;
        data.userID = userId;
        data.token = token;
        data.config = path;
    }

    public void Start()
    {
        string pathToFiles;
        jsonTEST = new testFORJSON();
        if(data.config[data.config.Length - 1] == '/')
        {
            pathToFiles = data.config + data.gameID;
        }
        else
        {
            pathToFiles = data.config + "/" + data.gameID;
        }
        StartCoroutine(getData(pathToFiles));
    }

    public IEnumerator getData(string _path)
    {
#if win
        _path = "https://laravel.etalonapps.hu/api/games/config/25";
#endif
        Debug.Log("The path: " + _path);
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(_path))
        {
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + unityWebRequest.error);
            }
            else
            {
                string json = unityWebRequest.downloadHandler.text;
                data = JsonUtility.FromJson<DataFromAPI>(json);
                data.gameID = gameId;
                data.userID = userId;
                data.token = token;
                data.config = path;
#if win
                data.chosenGameMode = 1;
#endif
                MainMenu.GetComponent<MainMenu>().setBackGround();
                StartCoroutine(getCardSetJSON(data.assets[1].path));
                StartCoroutine(getCardsJSON(data.assets[2].path));
                StartCoroutine(sendData());
            }
        }
    }

    public IEnumerator getCardsJSON(string path)
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(path))
        {
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + unityWebRequest.error);
            }
            else
            {
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
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + unityWebRequest.error);
            }
            else
            {
                string json = unityWebRequest.downloadHandler.text;
                json = json.Remove(0, 1);
                jsonTEST = JsonUtility.FromJson<testFORJSON>(json);
                for (int i = 0; i < jsonTEST.cardSet.Length; i++)
                    cardSetManager.addCardSet(jsonTEST.cardSet[i]);
            }
        }
    }

    public IEnumerator getUsers()
    {
        using (UnityWebRequest unityWebRequest = UnityWebRequest.Get(path))
        {
            yield return unityWebRequest.SendWebRequest();
            if (unityWebRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error: " + unityWebRequest.error);
            }
            else
            {
                string json = unityWebRequest.downloadHandler.text;
                json = json.Remove(0, 1);
                dataToServer = JsonUtility.FromJson<DataToServer>(json);
            }
        }
    }

    public IEnumerator sendData()
    {
        dataToServer.scores = ProfileManager.profileManager.scores;
        dataToServer.custom = ProfileManager.profileManager.subUsers;
#if win
        dataToServer.gameId = 25;
        dataToServer.userId = 973;
        dataToServer.token = "ujAE5CZrqdqhgWFfHUuHx3YeCjPunfaj";
#else
        dataToServer.gameId = data.gameID;
        dataToServer.userId = data.userID;
        dataToServer.token = data.token;
#endif

        string json = JsonUtility.ToJson(dataToServer);
        
        var request = new UnityWebRequest("https://laravel.etalonapps.hu/api/games/result", "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Accept", "application/json");
        yield return request.SendWebRequest();
    }

    public testFORJSON getCards(string path)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<testFORJSON>(json);
    }
}
