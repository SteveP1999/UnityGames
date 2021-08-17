using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Age
{
    young,
    old,
    none
}

public enum Player
{
    player1,
    player2,
    player3,
    none
}

public class Profile
{
    private string name;

    private Age age;

    private int level;

    private Player player;

    public void setName(string name)
    {
        this.name = name;
    }

    public string getName()
    {
        return name; ;
    }

    public Age Age
    {
        get => Age;

        set
        {
            Age = value;
        }
    }

    public int Level
    {
        get => level;

        set
        {
            level = value;
        }
    }

    public Player Player
    {
        get => player;

        set
        {
            player = value;
        }
    }

}

