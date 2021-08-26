using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DbManager : MonoBehaviour
{
    private string getURL = "http://localhost/unity/getUsers.php";
    private string postURL = "http://localhost/unity/updateuser.php";
    [SerializeField] private ProfileManager profileManager;
    string trimmedjsonArray;
    public static DbManager dbManager;
    [SerializeField] StartManager startManager;
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
        StartCoroutine(getUsers(getURL));
    }
    IEnumerator getUsers(string URL)
    {
        WWWForm form = new WWWForm();
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
}
