using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private InputField[] inputFields;

    public Profile[] profiles = new Profile[3];
    private string secretKey = "mySecretKey";
    public string addPlayerURL =
            "http://localhost/UnityGame/addplayer.php?";
    public string connectionURL =
             "http://localhost/UnityGame/connection.php";


    public void activityChanged(int id)
    {
        if(profiles[id].getActive() == false)
        {
            for(int i = 0; i < 2; i++)
            {
                profiles[i].setActive(false);
            }
            profiles[id].setActive(true);
        }
        else
        {
            profiles[id].setActive(false);
        }
    }

    public void dataChange(int id)
    {
        profiles[id].setDataChanged(true);
    }

    public void usernameValue(int id)
    {
        profiles[id].setName(inputFields[id].text);
    }


    public void test()
    {
        displayUsers();
    }

    private void displayUsers()
    {
        StartCoroutine(getUsers());
    }

    private void saveUsers()
    {
        //1-es számu user:
        StartCoroutine(postUsers(profiles[0].getName(), profiles[0].Age.ToString(), profiles[0].Level, "Player1"));

        //2-es számu user:
        StartCoroutine(postUsers(profiles[1].getName(), profiles[1].Age.ToString(), profiles[1].Level, "Player2"));

        //3-as számu user:
        StartCoroutine(postUsers(profiles[2].getName(), profiles[2].Age.ToString(), profiles[2].Level, "Player3"));
    }

    IEnumerator postUsers(string name, string age, int level, string player)
    {
        string hash = HashInput(name + age + level + player + secretKey);
        string post_url = addPlayerURL + "name=" + 
               UnityWebRequest.EscapeURL(name) + "age=" + UnityWebRequest.EscapeURL(age) + "&level="
               + level + "player=" + UnityWebRequest.EscapeURL(player) + "&hash=" + hash;
        UnityWebRequest hs_post = UnityWebRequest.Post(post_url, hash);
        yield return hs_post.SendWebRequest();
        if (hs_post.error != null)
            Debug.Log("There was an error posting the players' data: "
                    + hs_post.error);
    }


    IEnumerator getUsers()
    {
        UnityWebRequest hs_get = UnityWebRequest.Get(connectionURL);
        yield return hs_get.SendWebRequest();
        if (hs_get.error != null)
            Debug.Log("There was an error getting the players' data: "
                    + hs_get.error);
        else
        {
            string dataText = hs_get.downloadHandler.text;
            MatchCollection mc = Regex.Matches(dataText, @"_");
            if (mc.Count > 0)
            {
                string[] splitData = Regex.Split(dataText, @"_");
                for (int i = 0; i < mc.Count; i++)
                {
                    if (i % 4 == 0)
                    {
                        //profiles[0].setName(splitData[i]);
                    }
                    else if(i % 4 == 1)
                    {
                        //profiles[0].Age = (Age)splitData[i];
                    }
                    else if(i % 4 == 2)
                    {
                        //profiles[0].Level = splitData[i];
                    }
                    else
                    {
                        //profiles[0].Player = splitData[i];
                    }
                }
            }
        }
    }


    public string HashInput(string input)
    {
        SHA256Managed hm = new SHA256Managed();
        byte[] hashValue =
                hm.ComputeHash(System.Text.Encoding.ASCII.GetBytes(input));
        string hash_convert =
                 BitConverter.ToString(hashValue).Replace("-", "").ToLower();
        return hash_convert;
    }


    private void resetProfile(Profile prof)
    {
        prof.Level = 4;
        prof.setName("");
        prof.Player = Player.none;
        prof.Age = Age.none;
    }

    private void addProfile(int index, string name, Age age, int level, Player player)
    {
        ;
    }
    private void setName(Profile prof, string name)
    {
        prof.setName(name);
    }

    private void setPlayer(Profile prof, Player player)
    {
        prof.Player = player;
    }

    private void setAge(Profile prof, Age age)
    {
        prof.Age = age;
    }

}
