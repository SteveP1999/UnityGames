using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System;

public class API : MonoBehaviour
{
    public DataFromAPI data;

    public IEnumerator getData(string path)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://laravel.etalonapps.hu/api/games/config/13");
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        data = JsonUtility.FromJson<DataFromAPI>(json);
        yield return JsonUtility.FromJson<DataFromAPI>(json);
    }

    public testFORJSON getCardSetJSON(string path)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<testFORJSON>(json);
    }

    public testFORJSON getCardsJSON(string path)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string json = reader.ReadToEnd();
        return JsonUtility.FromJson<testFORJSON>(json);
    }
}
