using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNewAssetButton : MonoBehaviour
{
    private CardSetManager cardSetManager;
    [SerializeField] private GameController gc;
    [SerializeField] private StartButton startButton;
    [SerializeField] private LoadAsset loadAsset;
    private bool hasNewLoaded = false;

    public void Awake()
    {
        cardSetManager = GameObject.FindGameObjectWithTag("cardSetCollectionManager").GetComponent<CardSetManager>();
    }

    public void drawAssetClicked()
    {
        Debug.Log("Value changed");
        hasNewLoaded = true;
    }

    public void DrawNewAsset()
    {
        Debug.Log("Beértünk a new asset sorsolásába.");
        string newAsset1;
        string newAsset2;

        if (GameData.instance.getGameID() == 2)
        {
            newAsset1 = cardSetManager.drawAsset();
            newAsset2 = cardSetManager.drawAsset();
            while(newAsset2 == newAsset1)
            {
                newAsset2 = cardSetManager.drawAsset();
            }
            gc.assetName1 = newAsset1;
            gc.assetName2 = newAsset2;
            loadAsset.unloadAsset();
            loadAsset.loadAsset(newAsset1, false);
            loadAsset.loadAsset(newAsset2, true);
        }
        else
        {
            newAsset1 = cardSetManager.drawAsset();
            gc.assetName1 = newAsset1;
            loadAsset.unloadAsset();
            loadAsset.loadAsset(newAsset1, false);

        }
    }

    public bool getDrawNewAssetValue()
    {
        return hasNewLoaded;
    }

    public void setDrawNewAssetValue(bool value)
    {
        hasNewLoaded = value;
    }
}
