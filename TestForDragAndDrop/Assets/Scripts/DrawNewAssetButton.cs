using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNewAssetButton : MonoBehaviour
{
    #region Variables
    [SerializeField] private CardSetManager cardSetManager;
    [SerializeField] private bool hasNewLoaded = false;
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
        cardSetManager = GameObject.FindGameObjectWithTag("cardSetCollectionManager").GetComponent<CardSetManager>();

        string newAsset1;
        string newAsset2;

        if (API.instance.data.chosenGameMode == 2)
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
            loadAsset.loadAssetBundles(newAsset1, newAsset2, false);
            //loadAsset.loadAssetBundle(newAsset1, false);
            //loadAsset.loadAssetBundle(newAsset2, false);
        }
        else
        {
            newAsset1 = cardSetManager.drawAsset();
            GameController.instance.assetName1 = newAsset1;
            loadAsset.unloadAsset();
            loadAsset.loadAssetBundle(newAsset1, false);

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
