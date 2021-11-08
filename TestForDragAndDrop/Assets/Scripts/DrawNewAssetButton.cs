using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNewAssetButton : MonoBehaviour
{
    private CardSetManager cardSetManager;
    [SerializeField] private GameController gc;
    [SerializeField] private StartButton startButton;
    [SerializeField] private LoadAsset loadAsset;

    public void drawNewAsset()
    {
        string newAsset1;
        string newAsset2;

        if(startButton.caseSwitch == 2)
        {
            newAsset1 = cardSetManager.drawAsset();
            newAsset2 = cardSetManager.drawAsset();
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
}
