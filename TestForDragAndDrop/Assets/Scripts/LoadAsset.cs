#define win

using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAsset : MonoBehaviour
{
    #region Variables
    [SerializeField] private AssetBundle myLoadedAssetBundle1;
    [SerializeField] private AssetBundle myLoadedAssetBundle2;
    [System.NonSerialized] public Texture2D texture;
    private GameObject cardCollectionManager;
    private CardManager cardManager;
    #endregion

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
        cardCollectionManager = GameObject.FindGameObjectWithTag("cardCollectionManager");
        cardManager = cardCollectionManager.GetComponent<CardManager>();
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

    public void loadAssetBundle(string assetName, bool calledFromGameControllerStart)
    {
#if win
        string path = "https://laravel.etalonapps.hu/public/files/dev/" + assetName.ToLower(); //Windows version
#else
        string path = API.instance.data.assets[0].path + "/" + assetName.ToLower(); //WebGL version
#endif
        StartCoroutine(loadBundleFromWeb(path, calledFromGameControllerStart));
    }

    IEnumerator loadBundleFromWeb(string path, bool calledFromGameControllerStart)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path);
        yield return www.SendWebRequest();

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
        if (bundle == null)
        {
            Debug.Log("Error while loading the assetbundle, the following path is not right: " + path);
        }
        else
        {
            Debug.Log("Flawless loading");
            myLoadedAssetBundle1 = bundle;
        }
        loadAllCards(calledFromGameControllerStart);
    }

    IEnumerator loadBundlesFromWeb(string path1, string path2, bool calledFromGameControllerStart)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path1);
        yield return www.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);

        if (bundle == null)
        {
            Debug.Log("Error while loading the assetbundle, the following path is not right: " + path1);
        }
        else
        {
            myLoadedAssetBundle1 = bundle;
            StartCoroutine(loadSecondBundle(path2, calledFromGameControllerStart));
        }
    }

    IEnumerator loadSecondBundle(string path, bool calledFromGameControllerStart)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path);
        yield return www.SendWebRequest();
        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
        if (bundle == null)
        {
            Debug.Log("Error while loading the assetbundle, the following path is not right: " + path);
        }
        else
        {
            myLoadedAssetBundle2 = bundle;
            loadAllCards(calledFromGameControllerStart);
        }
    }


    public void loadAssetBundles(string assetName1, string assetName2, bool calledFromGameControllerStart)
    {
#if win
        string path1 = "https://laravel.etalonapps.hu/public/files/dev/" + assetName1.ToLower(); //Windows version
        string path2 = "https://laravel.etalonapps.hu/public/files/dev/" + assetName2.ToLower(); //Windows version
#else
        string path1 = API.instance.data.assets[0].path + "/" + assetName1.ToLower(); //WebGL version
        string path2 = API.instance.data.assets[0].path + "/" + assetName2.ToLower(); //WebGL version
#endif

        StartCoroutine(loadBundlesFromWeb(path1, path2, calledFromGameControllerStart));
    }

    public void loadNewArrival()
    {
        cardManager.drawDifferentCard(GameController.instance.assetName1);
        Texture2D newTexture = myLoadedAssetBundle1.LoadAsset(cardManager.containerOfCards1[cardManager.containerOfCards1.Count - 1].getCardName()) as Texture2D;
        GameController.instance.textures1.Add(newTexture);
        GameController.instance.setIdOfNewArrival(cardManager.containerOfCards1[cardManager.containerOfCards1.Count - 1].getUniqueId());
        Debug.Log("Id of new arrival: " + cardManager.containerOfCards1[cardManager.containerOfCards1.Count - 1].getUniqueId());
    }

    public void loadAllCards(bool calledFromGameControllerStart)
    {
        if (API.instance.data.chosenGameMode == 2)
        {
            //Put the textures from the first bundle to a list
            cardManager.drawDifferentCards(10, GameController.instance.assetName1, true);
            for (int i = 0; i < 10; i++)
            {
                Texture2D loadedAsset = myLoadedAssetBundle1.LoadAsset(cardManager.containerOfCards1[i].getCardName()) as Texture2D;
                GameController.instance.textures1.Add(loadedAsset);
            }

            //Put the textures from the second bundle to a list if needed
            cardManager.drawDifferentCards(10, GameController.instance.assetName2, false);
            {
                for (int i = 0; i < cardManager.containerOfCards2.Count; i++)
                {
                    Texture2D loadedAsset = myLoadedAssetBundle2.LoadAsset(cardManager.containerOfCards2[i].getCardName()) as Texture2D;
                    GameController.instance.textures2.Add(loadedAsset);
                }
            }
        }
        else
        {
            //Put the textures from the first bundle to a list
            cardManager.drawDifferentCards(GameController.instance.getGameLevel(), GameController.instance.assetName1, true);
            for (int i = 0; i < cardManager.containerOfCards1.Count; i++)
            {
                Texture2D loadedAsset = myLoadedAssetBundle1.LoadAsset(cardManager.containerOfCards1[i].getCardName()) as Texture2D;
                GameController.instance.textures1.Add(loadedAsset);
            }
        }

        switch (API.instance.data.chosenGameMode)
        {
            case 1:
                GameController.instance.newArrival();
                break;
            case 2:
                GameController.instance.pairThem();
                break;
            case 3:
                GameController.instance.putThemInOrder();
                break;
            default:
                Debug.Log("No such case as given");
                break;
        }
    }
}