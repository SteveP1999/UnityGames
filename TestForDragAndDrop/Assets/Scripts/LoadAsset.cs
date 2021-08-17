using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadAsset : MonoBehaviour
{
    public GameController gc;
    AssetBundle myLoadedAssetBundle;
    private string path;
    [System.NonSerialized]
    public Texture2D texture;
    private string switchCase = "Francia";
    [SerializeField]
    private ArrayList ids = new ArrayList();
    private Object[] assetList;
    private Object[] assetListForPairs;

    public ArrayList getIds()
    {
        return ids;
    }

    void Start()
    {
        switch (switchCase)
        {
            case "AbcL_EN":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\abcl_en";
                //loadAssetBundle(path);
                //generateNewIds(gc.gameLevel, ids, 26);
                //loadTextures();
                break;
            case "AbcL_HU":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\abcl_hu";
                //loadAssetBundle(path);
                //generateNewIds(gc.gameLevel, ids, 44);
                //loadTextures();
                break;
            case "AbcU_EN":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\abcu_en";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 26);
                //loadTextures();
                break;
            case "AbcU_HU":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\abcu_hu";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 44);
                //loadTextures();
                break;
            case "Domino01":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino01";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino02":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino02";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino03":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino03";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino04":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino04";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino05":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domnio05";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino06":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino06";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino07":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino07";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino08":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domnio08";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino09":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domini09";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino10":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino10";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino11":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino11";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino12":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino12";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino13":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino13";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino14":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino14";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino15":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino15";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino16":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino16";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "Domino17":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\domino17";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 9);
                //loadTextures();
                break;
            case "DuplaDomino":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\dupladomino";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 100);
                //loadTextures();
                break;
            case "Francia":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\francia";
                loadAssetBundle(path);
                generateNewIds(gc.getGameLevel(), ids);
                loadTextures();
                break;
            case "Num20":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\num20";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 20);
                //loadTextures();
                break;
            case "Num100":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\num100";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 100);
                //loadTextures();
                break;
            case "OraSzamlap":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\oraszamlap";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 12);
                //loadTextures();
                break;
            case "RomaiGyufa":
                path = @"C:\Users\SteveP1\Desktop\AssetBundles\romaigyufa";
                //loadAssetBundle(path);
                // generateNewIds(gc.gameLevel, ids, 15);
                //loadTextures();
                break;
            default:
                Debug.Log("Nem megfelelő mappanév!");
                break;
        }
    }

    void loadAssetBundle(string buildUrl)
    {
        myLoadedAssetBundle = AssetBundle.LoadFromFile(buildUrl);
        assetList = myLoadedAssetBundle.LoadAllAssets<Texture2D>();
        Debug.Log(myLoadedAssetBundle == null ? "Valami hiba történt, a hibás path: " + path + " volt." : " Sikeres betöltés");
    }

    public void loadTextures()
    {
        int i = 0;
        foreach (Object o in assetList)
        {
            if (o.GetType() == typeof(Texture2D))
            {
                if (ids.Contains(i))
                {
                    texture = o as Texture2D;
                    gc.textures.Add(texture);
                }
                i++;
            }
        }
    }

    public void generateNewIds(int numOfNumbers, ArrayList ids)
    {
        var rand = new System.Random();
        for (int j = 0; j < numOfNumbers; j++)
        {
            bool canBePlaced = true;
            while (canBePlaced != false)
            {
                int number = rand.Next(assetList.Length);
                if (!ids.Contains(number))
                {
                    ids.Add(number);
                    canBePlaced = false;
                }
            }
        }
        ids.Sort();
    }

    public void addNewArrival()
    {
        bool canBePlaced = true;
        var rand = new System.Random();
        while (canBePlaced != false)
        {
            int number = rand.Next(assetList.Length);
            if (!ids.Contains(number))
            {
                ids.Add(number);
                gc.setIdOfNewArrival(number) ;
                canBePlaced = false;
            }
        }
        loadOneTexture();
    }

    public void loadOneTexture()
    {
        int i = 0;
        foreach (Object o in assetList)
        {
            if (o.GetType() == typeof(Texture2D))
            {
                if (i == gc.getIdOfNewArrival())
                {
                    texture = o as Texture2D;
                    gc.textures.Add(texture);
                }
                i++;
            }
        }
    }
}