using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void setGameIDNewArrival()
    {
        GameData.instance.setGameID(1);
    }

    public void setGameIDPair()
    {
        GameData.instance.setGameID(2);
    }

    public void setGameIDOrder()
    {
        GameData.instance.setGameID(3);
    }

    public void QuiGamet()
    {
        Application.Quit();
    }

}
