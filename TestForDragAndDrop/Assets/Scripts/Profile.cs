using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Profile
{
    [SerializeField] private string name;

    [SerializeField] private string age;

    [SerializeField] private int level;

    [SerializeField] private string player;

    [SerializeField] private bool active;   //0 - hamis 1 - igaz

    private bool dataChanged;

    private int gameLevelOrder;

    private int gameLevelPair;

    private int gameLevelNewArrival;

    public void setProfileInfo(string name, string age, int level, string player, bool active)
    {
        this.name = name;
        this.age = age;
        this.level = level;
        this.player = player;
        this.active = active;
    }

    public void setDataChanged(bool val)
    {
        dataChanged = val;
    }

    public bool getDataChanged()
    {
        return dataChanged;
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

    public string getPlayer()
    {
        return player;
    }

    public int getLevel()
    {
        return level;
    }

    public void loadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}

