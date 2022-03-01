using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataFromAPI
{
    public Assets[] assets;
    public int chosenGameMode;
}

[System.Serializable]
public class Assets
{
    public string name;
    public string path;
}
