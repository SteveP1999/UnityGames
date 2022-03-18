using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNewAssetButton : MonoBehaviour
{
    #region Variables
    private CardSetManager cardSetManager;
    private bool hasNewLoaded = false;
    [SerializeField] private StartButton startButton;
    [SerializeField] private LoadAsset loadAsset;
    #endregion

    public void Awake()
    {
        cardSetManager = GameObject.FindGameObjectWithTag("cardSetCollectionManager").GetComponent<CardSetManager>();
    }

    public void drawAssetClicked()
    {
        hasNewLoaded = true;
    }

    public void DrawNewAsset()
    {
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

            GameController.instance.assetName1 = newAsset1;
            GameController.instance.assetName2 = newAsset2;

            loadAsset.unloadAsset();
            loadAsset.loadAssetBundle(newAsset1, false, false);
            loadAsset.loadAssetBundle(newAsset2, true, false);
        }
        else
        {
            newAsset1 = cardSetManager.drawAsset();
            GameController.instance.assetName1 = newAsset1;
            loadAsset.unloadAsset();
            loadAsset.loadAssetBundle(newAsset1, false, false);

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
