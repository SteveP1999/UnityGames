using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class DataToServer 
{
    public string token;
    public int gameId;
    public int userId;
    public Scores[] scores = new Scores[3];
    public SubUser[] custom = new SubUser[3];
}
