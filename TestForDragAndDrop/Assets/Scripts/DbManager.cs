using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DbManager : MonoBehaviour
{
    private string URL = "http://localhost/unity/getUsers.php";
    [SerializeField] private ProfileManager profileManager;
    string trimmedjsonArray;

    void Start()
    {
        StartCoroutine(getUsers(URL));
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
                //string player1 = trimmedjsonArray.Split('}')[0];
                //string player2 = trimmedjsonArray.Split('}')[1];
                //string player3 = trimmedjsonArray.Split('}')[2];
                //player2 = player2.Substring(1);
                //Debug.Log(player1);
                //Debug.Log(player2);
                //Debug.Log(player3);
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

        for(int i = 0; i < profileManager.profiles.Length; i++)
        {
            Debug.Log(profileManager.profiles[i].getName());
            Debug.Log(profileManager.profiles[i].getAge());
            Debug.Log(profileManager.profiles[i].getActive());
            Debug.Log(profileManager.profiles[i].getLevel());
            Debug.Log(profileManager.profiles[i].getPlayer());
        }
    }
}
