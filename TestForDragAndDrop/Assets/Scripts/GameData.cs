using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private int gameID;

    #region instance
    public static GameData instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    public void setGameID(int id)
    {
        gameID = id;
    }

    public int getGameID()
    {
        return gameID;
    }


}
