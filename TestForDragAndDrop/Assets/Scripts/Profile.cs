using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class contains a player's data
[System.Serializable]
public class Profile
{
    #region Variables
    [SerializeField] private string name;

    [SerializeField] private string age;

    [SerializeField] private int level;

    [SerializeField] private string player;

    [SerializeField] private bool active;   //0 - false 1 - true

    private int gameLevelOrder;

    private int gameLevelPair;

    private int gameLevelNewArrival;
    #endregion

    #region Getters / Setters
    public void setProfileInfo(string name, string age, int level, string player, bool active)
    {
        this.name = name;
        this.age = age;
        this.level = level;
        this.player = player;
        this.active = active;
    }

    public void setActive(bool val)
    {
        active = val;
    }

    public bool getActive()
    {
        return active;
    }

    public void setName(string name)
    {
        this.name = name;
    }

    public string getName()
    {
        return name;
    }

    public string getAge()
    {
        return age;
    }

    public void setAge(string age)
    {
        this.age = age;
    }

    public string getPlayer()
    {
        return player;
    }

    public int getLevel()
    {
        return level;
    }

    public void setLevel(int level)
    {
        this.level = level;
    }
    #endregion


    //This functions loads in the data from a json file into the variables
    public void loadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}

