using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAsset : MonoBehaviour
{
    public GameController gc;
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


    public void loadAsset(string assetName, bool gameMode)
    {
        string path = @"C:\Users\SteveP1\Desktop\AssetBundles\" + assetName;
        if(gameMode == false)   //Többi game
        {
            myLoadedAssetBundle1 = AssetBundle.LoadFromFile(path);
        }
        else  //Pair game
        {
            myLoadedAssetBundle2 = AssetBundle.LoadFromFile(path);
        }
        Debug.Log(myLoadedAssetBundle1 == null && myLoadedAssetBundle2 == null ? "Valami hiba történt, a hibás path: " + path + " volt." : " Sikeres betöltés, a " + assetName + " betöltődött.");
    }

    public void loadNewArrival()
    {
        cardManager.drawDifferentCard(gc.assetName1);   //gc.assetName1 volt
        Texture2D newTexture = myLoadedAssetBundle1.LoadAsset(cardManager.cardList1[cardManager.cardList1.Count - 1]) as Texture2D;
        gc.textures1.Add(newTexture);
        gc.setIdOfNewArrival(cardManager.cardList1Ids[cardManager.cardList1Ids.Count - 1]);
        Debug.Log("Id of new arrival: " + cardManager.cardList1Ids[cardManager.cardList1Ids.Count - 1]);
    }

    public void loadAllCards(bool gameMode)
    {
        //Put the textures from the first bundle to a list
        cardManager.drawDifferentCards(gc.getGameLevel(), gc.assetName1, true);
        for (int i = 0; i < cardManager.cardList1.Count; i++)
        {
            Texture2D loadedAsset = myLoadedAssetBundle1.LoadAsset(cardManager.cardList1[i]) as Texture2D;
            gc.textures1.Add(loadedAsset);
        }

        //Put the textures from the second bundle to a list if needed
        if (gameMode == false)    //false == pair game
        {
            cardManager.drawDifferentCards(gc.getGameLevel(), gc.assetName2, false);
            {
                for (int i = 0; i < cardManager.cardList2.Count; i++)
                {
                    Texture2D loadedAsset = myLoadedAssetBundle1.LoadAsset(cardManager.cardList2[i]) as Texture2D;
                    gc.textures2.Add(loadedAsset);
                }
            }
        }
    }
}