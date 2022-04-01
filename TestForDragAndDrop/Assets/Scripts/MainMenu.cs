using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Sprite[] backGroundSpirtes;
    [SerializeField] private Image backGround;

    private void Start()
    {
        //setBackGround();
    }

    public void setBackGround()
    {
        Debug.Log("chosenGame: " + API.instance.data.chosenGameMode);
        switch (API.instance.data.chosenGameMode)
        {
            case 1:
                backGround.sprite = backGroundSpirtes[0];
                break;
            case 2:
                backGround.sprite = backGroundSpirtes[1];
                break;
            case 3:
                backGround.sprite = backGroundSpirtes[2];
                break;
            default:
                Debug.LogError("Error with chosenGameMode value");
                break;
        }
    }

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
