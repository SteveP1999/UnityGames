using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DbManager : MonoBehaviour
{
    [SerializeField] private ProfileManager profileManager;
    [SerializeField] StartManager startManager;
    [SerializeField] CardManager cardManager;
    [SerializeField] CardSetManager cardSetManager;

    private string getusersURL = "http://localhost/unity/getUsers.php";
    private string getcardsURL = "http://localhost/unity/getCards.php";
    private string getassetsURL = "http://localhost/unity/getCardSets.php";
    private string postURL = "http://localhost/unity/updateuser.php";
    string trimmedjsonArray;
    public static DbManager dbManager;
    
    void Awake()
    {
        if (dbManager == null)
        {
            DontDestroyOnLoad(gameObject);
            dbManager = this;
        }
        else if (dbManager != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(getUsers(getusersURL));
        StartCoroutine(getCards(getcardsURL));
        StartCoroutine(getAssets(getassetsURL));
    }

    IEnumerator getUsers(string URL)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("There was a mistake");
            }
            else
            {
                //Debug.Log(www.downloadHandler.text);
                string jsonArray = www.downloadHandler.text;
                string trimjsonArray = jsonArray.Replace("[", "");
                trimmedjsonArray = trimjsonArray.Replace("]", "");
            }
        }

        for (int i = 0; i < profileManager.profiles.Length; i++)
        {
            string player1 = trimmedjsonArray.Split('}')[i];
            if (i == 0)
            {
                player1 += "}";
            }
            else
            {
                player1 = player1.Substring(1);
                player1 += "}";
            }
            Profile profile1 = new Profile();
            profile1.loadFromJson(player1);
            profileManager.profiles[i] = profile1;
        }
        startManager.setUpMenu();
    }

    public IEnumerator saveUser(string name, string age, int level, string player, bool active)
    {
        int valueActive = 0;
        if (active == true)
            valueActive = 1;
            
        WWWForm form = new WWWForm();
        form.AddField("saveName", name);
        form.AddField("saveAge", age);
        form.AddField("saveLevel", level);
        form.AddField("savePlayer", player);
        form.AddField("saveActive", valueActive);

        using (UnityWebRequest www = UnityWebRequest.Post(postURL, form))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("There was a mistake");
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator getCards(string URL)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("There was a mistake");
            }
            else
            {
                string jsonArray = www.downloadHandler.text;
                string trimjsonArray = jsonArray.Replace("[", "");
                trimmedjsonArray = trimjsonArray.Replace("]", "");
            }
        }

        int i = 0;

        while (trimmedjsonArray.Split('}')[i] != "")
        {
            string cardString = trimmedjsonArray.Split('}')[i];
            if (i == 0)
            {
                cardString += "}";
            }
            else
            {
                cardString = cardString.Substring(1);
                cardString += "}";
            }
            Card card = new Card();
            card.loadFromJson(cardString);
            cardManager.addCard(card);
            i++;
        }
    }

    public IEnumerator getAssets(string URL)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(URL))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log("There was a mistake");
            }
            else
            {
                string jsonArray = www.downloadHandler.text;
                string trimjsonArray = jsonArray.Replace("[", "");
                trimmedjsonArray = trimjsonArray.Replace("]", "");
            }
        }

        int i = 0;

        while (trimmedjsonArray.Split('}')[i] != "")
        {
            string json = trimmedjsonArray.Split('}')[i];
            if (i == 0)
            {
                json += "}";
            }
            else
            {
                json = json.Substring(1);
                json += "}";
            }
            CardSet cardSet = new CardSet();
            cardSet.loadFromJson(json);
            cardSetManager.addCardSet(cardSet);
            i++;
        }
    }
}
