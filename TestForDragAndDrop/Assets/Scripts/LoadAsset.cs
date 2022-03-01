using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAsset : MonoBehaviour
{
    private AssetBundle myLoadedAssetBundle1;
    private AssetBundle myLoadedAssetBundle2;
    private string path;
    [System.NonSerialized]
    public Texture2D texture;
    private GameObject cardSetCollectionManager;
    private GameObject cardCollectionManager;
    private CardSetManager cardSetManager;
    private CardManager cardManager;


    public AssetBundle getAssetBundle1()
    {
        return myLoadedAssetBundle1;
    }

    public AssetBundle getAssetBundle2()
    {
        return myLoadedAssetBundle2;
    }

    void Awake()
    {
        cardSetCollectionManager = GameObject.FindGameObjectWithTag("cardSetCollectionManager");
        cardCollectionManager = GameObject.FindGameObjectWithTag("cardCollectionManager");
        cardManager = cardCollectionManager.GetComponent<CardManager>();
        cardSetManager = cardSetCollectionManager.GetComponent<CardSetManager>();
    }


    public void unloadAsset()
    {
        if(myLoadedAssetBundle1 != null)
        {
            myLoadedAssetBundle1.Unload(false);
        }
        if(myLoadedAssetBundle2 != null)
        {
            myLoadedAssetBundle2.Unload(false);
        }
    }


    public void loadAsset(string assetName)
    {
        string path = @"C:\Users\SteveP1\Desktop\AssetBundles\" + assetName;
        if(GameData.instance.getGameID() != 2)
        {
            myLoadedAssetBundle1 = AssetBundle.LoadFromFile(path);
        }
        else
        {
            myLoadedAssetBundle2 = AssetBundle.LoadFromFile(path);
        }
        Debug.Log(myLoadedAssetBundle1 == null && myLoadedAssetBundle2 == null ? "Valami hiba történt, a hibás path: " + path + " volt." : " Sikeres betöltés, a " + assetName + " betöltődött.");
    }

    public void loadNewArrival()
    {
        cardManager.drawDifferentCard(GameController.instance.assetName1);
        Texture2D newTexture = myLoadedAssetBundle1.LoadAsset(cardManager.containerOfCards1[cardManager.containerOfCards1.Count - 1].getCardName()) as Texture2D;
        GameController.instance.textures1.Add(newTexture);
        GameController.instance.setIdOfNewArrival(cardManager.containerOfCards1[cardManager.containerOfCards1.Count - 1].getCardId());
        Debug.Log("Id of new arrival: " + cardManager.containerOfCards1[cardManager.containerOfCards1.Count - 1].getCardId());
    }

    public void loadAllCards()
    {
        //Put the textures from the first bundle to a list
        cardManager.drawDifferentCards(GameController.instance.getGameLevel(), GameController.instance.assetName1, true);
        for (int i = 0; i < cardManager.containerOfCards1.Count; i++)
        {
            Texture2D loadedAsset = myLoadedAssetBundle1.LoadAsset(cardManager.containerOfCards1[i].getCardName()) as Texture2D;
            GameController.instance.textures1.Add(loadedAsset);
        }

        //Put the textures from the second bundle to a list if needed
        if (GameData.instance.getGameID() == 2)
        {
            cardManager.drawDifferentCards(GameController.instance.getGameLevel(), GameController.instance.assetName2, false);
            {
                for (int i = 0; i < cardManager.containerOfCards2.Count; i++)
                {
                    Texture2D loadedAsset = myLoadedAssetBundle2.LoadAsset(cardManager.containerOfCards2[i].getCardName()) as Texture2D;
                    GameController.instance.textures2.Add(loadedAsset);
                }
            }
        }
    }
}