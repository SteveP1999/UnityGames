﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

static class Constants
{
    #region Constant variables
    //Timing:
    public const float speedOfFirstPositioning = 0.5f;
    public const float speedOfZeroing = 0.3f;
    public const float speedOfArrivalZeroing = 0.5f;
    public const float speedOfThreeShuffles = 0.1f;
    public const float waitTimeInPairGame = 3.5f;
    public const float speedOfPairGame = 2.0f;
    public const float shuffleDelay = 1.0f;
    public const float cardChangeSpeed = 0.2f;
    public const float speedOfChoosenCard = 1.0f;

    //Positioning:
    public const float cardSize = 1.812f;
    public const float padding = 0.2f;

    public const float yMid = -0.5f;
    public const float yUp = 1.0f;
    public const float yDown = -2.0f;

    public const float yOrder = -2.0f;
    public const float yOrderBorder = 1.0f;

    public const float yPairUpper = 1.0f;
    public const float yPairLower = -5.0f;
    public const float yPairMid = -1.9f;
    public const float yPairInit = -0.4f;
    public const float yPairTemp = -10.0f;

    //Game settings:
    public const int minLevelOfNewArrival = 4;
    public const int maxLevelOfNewArrival = 14;
    public const int minLevelOfOrderGame = 4;
    public const int maxLevelOfOrderGame = 9;
    #endregion
}

public class GameController : MonoBehaviour
{
    //Info for myself:
    // New Arrival : 1
    // Pair Game : 2
    // Order Game : 3

    #region Variables
    [SerializeField] private Button orderGame;
    [SerializeField] private Button pairGame;
    [SerializeField] private Button giveNextCards;
    [SerializeField] private Button evaluateCards;

    //Game:
    private int idOfNewArrival = 0; //Contains the id of the new arrival
    [SerializeField] private int gameLevel = 6; //Game level
    private GameObject[] objs; //Container for the cards
    [SerializeField] private ArrayList listForMain = new ArrayList();
    [SerializeField] private bool canBeSelected = false; //Engedélyező, hogy megtippelhessük az új felszállót
    private int rightGuesses = 0; //Counter a jó tippekhez
    private int wrongGuesses = 0; //Counter a rossz tippekhez
    private int counterForPairGame = 0;
    private GameObject cardCollectionManager;
    private GameObject cardSetCollectionManager;
    private CardManager cardManager;
    private CardSetManager cardSetManager;
    public ArrayList normalIds = new ArrayList();
    ArrayList orderedIds = new ArrayList();
    public ArrayList ids1 = new ArrayList();
    public ArrayList ids2 = new ArrayList();
    public string assetName1;
    public string assetName2;
    public DrawNewAssetButton newAssetDrawer;
    [SerializeField] private Text mainText;
    public bool firstRun = true;


    //Timing:
    private float waitTimeForRevealReference = 2;
    private float waitTimeForReveal = 2; //Mennyi idő teljen el aközött, hogy kitettük a lapokat és elkezdjük felfordítani őket.
    private float waitBetweenCardsAndNewArrival = 2;  //Mennyi idő teljen el az összes lap felfordítása után ameddig az új felszállót betesszük
    private float waitBetweenCardsFlipping = 2; //Mennyi idő teljen el az egyik lap lefordítása és a másik lap felfordítása között

    //Assets:
    public List<Texture2D> textures1; //A textúrákat itt tároljuk el, amiket betöltünk
    public List<Texture2D> textures2; //A textúrákat itt tároljuk el, amiket betöltünk
    public LoadAsset loadAsset; //Hivatkozás a LoadAsset osztályra


    //Camera settings:
    new private GameObject camera;
    private readonly float[] cameraZPosNewArrival = { -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.5f, -9.0f, -9.0f, -10.5f };
    private readonly float[] cameraZPosOrder = { -7.0f, -7.0f, -7.0f, -8.0f, -7.0f, -7.0f, -10.0f, -12.0f, -12.0f, -10.0f, -10.0f, -12.0f, -12.0f, -12.0f };
    private readonly float[] cameraZPosPair = { -7.0f, -7.0f, -7.0f, -8.5f, -8.5f, -8.5f, -9.6f, -11.2f, -13.0f, -13.0f, -10.0f, -12.0f, -12.0f, -12.0f };
    private float cameraZPosPair1 = -5.3f;
    #endregion

    #region instance
    public static GameController instance;
    #endregion


    void Awake()
    {
        instance = this;
        cardSetCollectionManager = GameObject.FindGameObjectWithTag("cardSetCollectionManager");
        cardCollectionManager = GameObject.FindGameObjectWithTag("cardCollectionManager");
        cardManager = cardCollectionManager.GetComponent<CardManager>();
        cardSetManager = cardSetCollectionManager.GetComponent<CardSetManager>();
        camera = GameObject.Find("CardCamera");
        assetName1 = cardSetManager.drawAsset();

        if (GameData.instance.getGameID() == 2)
        {
            int k = 0;
            while(k != 1)
            {
                assetName2 = cardSetManager.drawAsset();
                if(assetName2 != assetName1)
                {
                    k++;
                }
            }
            loadAsset.loadAssetBundles(assetName1, assetName2, true);
        }
        else
        {
            loadAsset.loadAssetBundle(assetName1, true);
        }
    }

    #region OrderGameCode
    public void putThemInOrder()
    {
        for (int i = 0; i < cardManager.containerOfCards1.Count; i++)
        {
            ids1.Add(cardManager.containerOfCards1[i].getCardId());
        }

        for (int i = 0; i < cardManager.containerOfCards1.Count; i++)
        {
            orderedIds.Add(cardManager.containerOfCards1[i].getUniqueId());
        }

        orderGame.gameObject.SetActive(true);

        camera.transform.localPosition = new Vector3(0, 0, cameraZPosOrder[gameLevel - 1]);

        GameObject[] parent = GameObject.FindGameObjectsWithTag("instantiateParent");
        GameObject[] border = GameObject.FindGameObjectsWithTag("instantiateBorder");

        for (int i = 0; i < gameLevel; i++)
        {
            var card = Instantiate(parent[0], new Vector3(0, 0, 0), Quaternion.identity);
            var bord = Instantiate(border[0], new Vector3(0, 10, 0), Quaternion.identity);
            var starterBord = Instantiate(border[0], new Vector3(0, 10, 0), Quaternion.identity);

            card.tag = "Parent";
            card.GetComponentInChildren<CardModel>().tag = "CardModel";
            card.GetComponentInChildren<CardModel>().setBorder(starterBord.GetComponent<Border>());

            bord.tag = "Border";
            bord.GetComponent<Border>().setIsAvailable(true);


            starterBord.tag = "starterBorder";
            starterBord.GetComponent<Border>().setIsStarter(true);
            starterBord.GetComponent<Border>().setIsAvailable(true);
            starterBord.GetComponent<Border>().setOccupied(true);
            starterBord.GetComponent<Border>().setCard(card.GetComponentInChildren<CardModel>());
        }

        loadInObjects();

        for (int i = 0; i < listForMain.Count; i++)
        {
            ((GameObject)listForMain[i]).GetComponent<CardModel>().rend.materials[2].mainTexture = textures1[i];
            ((GameObject)listForMain[i]).GetComponent<CardModel>().setUniqueCardId(cardManager.containerOfCards1[i].getUniqueId());
            ((GameObject)listForMain[i]).GetComponent<CardModel>().setCardId(cardManager.containerOfCards1[i].getCardId()); 
        }

        orderedIds.Sort();

        parent = GameObject.FindGameObjectsWithTag("Parent");

        border = GameObject.FindGameObjectsWithTag("Border");

        GameObject[] starterBorder = GameObject.FindGameObjectsWithTag("starterBorder");

        shuffleIndexes(ids1);

        startPositioning(parent, 0.2f, 0.2f, 0.1f);

        posCardsForOrder(parent, Constants.yOrder);

        posCardsForOrder(starterBorder, Constants.yOrder);

        for (int i = 0; i < starterBorder.Length; i++)
        {
            starterBorder[i].tag = "Border";
        }

        posCardsForOrder(border, Constants.yOrderBorder);

        //Felfordítjuk a kártyákat egyesével
        StartCoroutine(revealCards(parent));
    }
    #endregion
    
    //----------------------------------------------------------------------------------------------------------------------------------
    
    #region PairGame
    public void pairThem()
    {
        camera.transform.localPosition = new Vector3(0, 0, cameraZPosPair1);

        pairGame.gameObject.SetActive(true);

        GameObject[] parent = GameObject.FindGameObjectsWithTag("instantiateParent");

        float x = -5;
        float y = 10;
        float z = 0;

        for (int i = 0; i < 20; i++)
        {
            var card = Instantiate(parent[0], new Vector3(x, y, z), Quaternion.identity);
            card.tag = "Parent";
            card.GetComponentInChildren<CardModel>().tag = "CardModel";
            card.transform.Rotate(0, 180, 0);

            if (i == gameLevel - 1)
            {
                x = 5;
            }
        }

        loadInObjects();

        for (int i = 0; i < listForMain.Count / 2; i++)
        {
            ((GameObject)listForMain[i]).GetComponent<CardModel>().rend.materials[2].mainTexture = textures1[i];
            ((GameObject)listForMain[i]).GetComponent<CardModel>().setCardId(cardManager.containerOfCards1[i].getCardId());
            ((GameObject)listForMain[i]).GetComponent<CardModel>().setUniqueCardId(cardManager.containerOfCards1[i].getUniqueId());
        }

        for(int j = listForMain.Count/2; j < listForMain.Count; j++)
        {
            ((GameObject)listForMain[j]).GetComponent<CardModel>().rend.materials[2].mainTexture = textures2[listForMain.Count-j-1];
            ((GameObject)listForMain[j]).GetComponent<CardModel>().setCardId(cardManager.containerOfCards2[listForMain.Count - j - 1].getCardId());
            ((GameObject)listForMain[j]).GetComponent<CardModel>().setUniqueCardId(cardManager.containerOfCards2[listForMain.Count - j - 1].getUniqueId());
        }

        for (int j = 0; j < 10; j++)
        {
            GameObject[] border = GameObject.FindGameObjectsWithTag("instantiateBorder");

            var bordStarter = Instantiate(border[0], new Vector3(0, 10, 0), Quaternion.identity);
            var bordPair1 = Instantiate(border[0], new Vector3(0, 10, 0), Quaternion.identity);
            var bordPair2 = Instantiate(border[0], new Vector3(0, 10, 0), Quaternion.identity);

            bordStarter.tag = "starterBorder";
            bordStarter.GetComponent<Border>().setIsAvailable(true);
            bordStarter.GetComponent<Border>().setIsStarter(true);

            bordPair1.tag = "pair1Border";
            bordPair1.GetComponent<Border>().setIsAvailable(false);

            bordPair2.tag = "pair2Border";
            bordPair2.GetComponent<Border>().setIsAvailable(true);
        }

        StartCoroutine(showPair());
    }
    #endregion
    
    //---------------------------------------------------------------------------------------------------------------------------------
    
    #region NewArrivalGame
    public void newArrival()
    {
        for (int i = 0; i < cardManager.containerOfCards1.Count; i++)
        {
            ids1.Add(cardManager.containerOfCards1[i].getUniqueId());    //Ez getCardId volt
        }

        camera.transform.localPosition = new Vector3(0, 0, cameraZPosNewArrival[gameLevel - 1]);

        float delay = gameLevel * waitBetweenCardsFlipping + waitTimeForReveal + waitBetweenCardsAndNewArrival - 2;

        GameObject parentInstantiate = GameObject.FindGameObjectWithTag("instantiateParent");

        //Felrakom a kártyákat
        for (int i = 0; i < gameLevel; i++)
        {
            var card = Instantiate(parentInstantiate, new Vector3(0, 0, 0), Quaternion.identity);
            card.tag = "Parent";
            card.GetComponentInChildren<CardModel>().tag = "CardModel";
        }

        loadInObjects();


        for (int i = 0; i < listForMain.Count; i++)
        {
            ((GameObject)listForMain[i]).GetComponent<CardModel>().rend.materials[2].mainTexture = textures1[i];
            ((GameObject)listForMain[i]).GetComponent<CardModel>().setCardId(cardManager.containerOfCards1[i].getCardId());
            ((GameObject)listForMain[i]).GetComponent<CardModel>().setUniqueCardId(cardManager.containerOfCards1[i].getUniqueId());
        }

        //Megkeressük az összes kártyát és megfelelően pozícionáljuk őket
        GameObject[] parent = GameObject.FindGameObjectsWithTag("Parent");

        shuffleIndexes(ids1);

        startPositioning(parent, 0.2f, 0.2f, 0.1f);

        positionCards2(parent);

        //Felfordítjuk a kártyákat egyesével
        for (int j = 0; j < listForMain.Count; j++)
        {
            for (int i = 0; i < ids1.Count; i++)
            {
                if (parent[i].GetComponentInChildren<CardModel>().getUniqueCardId() == (int)ids1[j])   //getCardID volt
                {
                    GameObject go = parent[i];
                    ParentScript ps = go.GetComponent<ParentScript>();
                    StartCoroutine(waitABitAndReveal(waitTimeForReveal, ps));
                    waitTimeForReveal += waitBetweenCardsFlipping;
                }
            }
        }

        StartCoroutine(waitAndPositionAllCardsInZero(delay + Constants.shuffleDelay));

        //Kirakjuk az új felszállót és becsúsztatjuk a 0,0-ba
        StartCoroutine(WaitaBitAndAddNewArrival(delay + Constants.shuffleDelay)); //Itt volt egy + 1.0f

        //Ide jön a shuffle:
        StartCoroutine(shuffle(parent, delay + Constants.shuffleDelay + 2.0f));

        //Megkeverjük, újra kitesszük
        StartCoroutine(waitABitAndDoAll(delay + Constants.shuffleDelay + 3.5f));

        //Felfordítjuk egyesével a kártyákat és várunk a tippre
        StartCoroutine(tippingPosition(delay + Constants.shuffleDelay + 5.0f));
    }
    #endregion

    //----------------------------------------------------------------------------------------------------------------------------------
    
    #region OrderGameFunctions
    //Functions for game no 1. (Order game):
    private void posCardsForOrder(GameObject[] parent, float y)
    {
        double x = calcx(parent.Length, 1.0f);
        for (int i = 0; i < parent.Length; i++)
        {
            parent[i].transform.position = new Vector3((float)x, y, 0);
            x += Constants.padding + Constants.cardSize;
        }
    }

    IEnumerator revealCards(GameObject[] parent)
    {
        for (int i = 0; i < ids1.Count; i++)
        {
            GameObject go = parent[i];
            ParentScript ps = go.GetComponent<ParentScript>();

            if (ps != null)
            {
                ps.revealAndHide();
                yield return new WaitForSeconds(2);
            }
        }

        for (int i = 0; i < listForMain.Count; i++)
        {
            parent[i].GetComponentInChildren<DragAndDrop>().setDrag(true);
        }
    }

    public void evaluateOrder()
    {
        GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");
        int i = 0;
        int counter = 0;
        while (i < borders.Length + 1)
        {
            if (borders[i].GetComponent<Border>().getOccupied() == true)
            {
                i++;
            }
            else
            {
                break;
            }
        }
        if (i == borders.Length / 2)
        {
            for (int j = 0; j < borders.Length / 2; j++)
            {
                if (borders[j].GetComponent<Border>().getId() <= (int)orderedIds[j])
                {
                    counter++;
                }
            }

            GameObject[] cards = GameObject.FindGameObjectsWithTag("Parent");
            for (int j = 0; j < cards.Length; j++)
            {
                cards[j].GetComponent<ParentScript>().reveal();
            }

            if (counter == gameLevel)
            {
                Debug.Log("Nyertél");
                guessedRight2(true);
            }
            else
            {
                Debug.Log("Elvesztetted");
                guessedRight2(false);
            }

            //StartCoroutine(resetOrderGame());
        }
        else
        {
            Debug.Log("There are missing cards");
        }
    }

    IEnumerator resetOrderGame()
    {
        yield return new WaitForSeconds(4);
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Parent");
        GameObject[] borderTemp = GameObject.FindGameObjectsWithTag("Border");
        for (int i = 0; i < temp.Length; i++)
        {
            DestroyImmediate(temp[i]);
        }
        for(int j = 0; j < borderTemp.Length;j++)
        {
            DestroyImmediate(borderTemp[j]);
        }

        newBundle();

        listForMain.Clear();
        orderedIds.Clear();
        ids1.Clear();
        textures1.Clear();
        cardManager.containerOfCards1.Clear();
        cardManager.containerOfCards2.Clear();

        if(newAssetDrawer.getDrawNewAssetValue() == false)
        {
            loadAsset.loadAllCards(false);
        }
        newAssetDrawer.setDrawNewAssetValue(false);
    }
    #endregion

    //----------------------------------------------------------------------------------------------------------------------------------

    #region PairGameFunctions
    //Functions for game no. 2 (Pair game):
    IEnumerator showPair()
    {
        GameObject[] parent = GameObject.FindGameObjectsWithTag("Parent");

        for (int i = 0; i < gameLevel; i++)
        {
            parent[i].GetComponentInChildren<CardModel>().setPair(parent[2 * gameLevel - i - 1]);

            positionForSmoothStep(parent[i], -1.5f, Constants.yPairInit, 0, true, Constants.speedOfPairGame);

            positionForSmoothStep(parent[2 * gameLevel - i - 1], 1.5f, Constants.yPairInit, 0, true, Constants.speedOfPairGame);

            yield return new WaitForSeconds(Constants.waitTimeInPairGame);

            positionForSmoothStep(parent[i], -5, -10, 0, true, Constants.speedOfPairGame);

            positionForSmoothStep(parent[2 * gameLevel - i - 1], 5, -10, 0, true, Constants.speedOfPairGame);
        }

        yield return new WaitForSeconds(3);

        for(int i = 0; i < gameLevel; i++)
        {
            parent[i].transform.position = new Vector3(-5, 10, 0);
            parent[2 * gameLevel - i - 1].transform.position = new Vector3(5, 10, 0);
        }

        counterForPairGame += 1;
        if(counterForPairGame < 1)
        {
            StartCoroutine(showPair());
        }

        if(counterForPairGame == 1)
        {
            camera.transform.localPosition = new Vector3(0, 0, cameraZPosPair[gameLevel - 1]);

            double x = calcx(gameLevel, 1);

            GameObject[] starterBorder = GameObject.FindGameObjectsWithTag("starterBorder");
            GameObject[] pair1Border = GameObject.FindGameObjectsWithTag("pair1Border");
            GameObject[] pair2Border = GameObject.FindGameObjectsWithTag("pair2Border");

            for (int j = 0; j < gameLevel; j++)
            {
                starterBorder[j].GetComponent<Border>().transform.position = new Vector3((float)x, Constants.yPairMid, 0);
                starterBorder[j].GetComponent<Border>().tag = "Border";
                starterBorder[j].GetComponent<Border>().setOccupied(true);
                starterBorder[j].GetComponent<Border>().setCard(parent[j].GetComponentInChildren<CardModel>());
                starterBorder[j].GetComponent<Border>().setId(parent[j].GetComponentInChildren<CardModel>().getCardId());

                pair1Border[j].GetComponent<Border>().transform.position = new Vector3((float)x, Constants.yPairTemp, 0);

                pair2Border[j].GetComponent<Border>().transform.position = new Vector3((float)x, Constants.yPairUpper, 0);
                pair2Border[j].GetComponent<Border>().tag = "Border";

                parent[j].transform.position = new Vector3((float)x, Constants.yPairMid, 0);
                parent[j].GetComponentInChildren<CardModel>().setBorder(starterBorder[j].GetComponent<Border>());
                parent[j].GetComponentInChildren<DragAndDrop>().setDrag(true);

                x += Constants.padding + Constants.cardSize;

                giveNextCards.gameObject.SetActive(true);
            }
        }
    }

    public void giveNextCard()
    {

        GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");
        int counter = 0;
        for (int i = 0; i < borders.Length; i++)
        {
            if (borders[i].transform.position.y == Constants.yPairUpper && borders[i].GetComponent<Border>().getOccupied() == true)
            {
                counter++;
            }
        }

        if (counter == gameLevel)
        {
            camera.transform.localPosition = new Vector3(0, 0, -15);
            giveNextCards.gameObject.SetActive(false);
            evaluateCards.gameObject.SetActive(true);

            GameObject[] border = GameObject.FindGameObjectsWithTag("Border");
            GameObject[] parent = GameObject.FindGameObjectsWithTag("Parent");

            double x = calcx(gameLevel, 1);

            for (int i = 0; i < border.Length; i++)
            {
                if (border[i].GetComponent<Border>().getOccupied())
                {
                    border[i].GetComponent<Border>().tag = "pair2Border";
                    border[i].GetComponent<Border>().setIsAvailable(false);
                }
                else
                {
                    border[i].GetComponent<Border>().tag = "starterBorder";
                }
            }

            GameObject[] starterBorder = GameObject.FindGameObjectsWithTag("starterBorder");
            GameObject[] pair1Border = GameObject.FindGameObjectsWithTag("pair1Border");

            for (int j = gameLevel; j < 2 * gameLevel; j++)
            {
                starterBorder[j - gameLevel].transform.position = new Vector3((float)x, Constants.yPairLower, 0);
                starterBorder[j - gameLevel].GetComponent<Border>().tag = "Border";
                starterBorder[j - gameLevel].GetComponent<Border>().setOccupied(true);
                starterBorder[j - gameLevel].GetComponent<Border>().setCard(parent[j].GetComponentInChildren<CardModel>());
                starterBorder[j - gameLevel].GetComponent<Border>().setId(parent[j].GetComponentInChildren<CardModel>().getCardId());

                parent[j].transform.position = new Vector3((float)x, Constants.yPairLower, 0);
                parent[j].GetComponentInChildren<CardModel>().setBorder(starterBorder[j - gameLevel].GetComponent<Border>());
                parent[j].GetComponentInChildren<DragAndDrop>().setDrag(true);

                pair1Border[j - gameLevel].transform.position = new Vector3((float)x, Constants.yPairMid, 0);
                pair1Border[j - gameLevel].GetComponent<Border>().tag = "Border";
                pair1Border[j - gameLevel].GetComponent<Border>().setIsAvailable(true);

                x += Constants.padding + Constants.cardSize;
            }

            GameObject[] pair2Border = GameObject.FindGameObjectsWithTag("pair2Border");

            for (int j = 0; j < pair2Border.Length; j++)
            {
                pair2Border[j].GetComponent<Border>().getCard().GetComponentInParent<DragAndDrop>().setDrag(false);
            }
        }
    }

    public void evaluatePair()
    {
        GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");
        int i = 0;
        int counter = 0;
        while (i < borders.Length)
        {
            if (borders[i].GetComponent<Border>().getOccupied() == true)
            {
                i++;
            }
            else
            {
                break;
            }
        }
        if (i == borders.Length)
        {
            Debug.Log("oké megvagyunk");
            for (int j = 0; j < borders.Length; j++)
            {
                //if (borders[j].GetComponent<Border>().getId() == borders
                //{
                //    counter++;
                //}
            }
        }
        else
        {
            Debug.Log("i: " + i);
            Debug.Log("borders.length: " + borders.Length);
        }

        if (counter == gameLevel)
        {
            Debug.Log("Nyertél");
        }
        else
        {
            Debug.Log("Elvesztetted");
        }

        resetPairGame();
    }

    public void resetPairGame()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Parent");
        GameObject[] borderTemp = GameObject.FindGameObjectsWithTag("Border");
        GameObject[] borderTemp2 = GameObject.FindGameObjectsWithTag("pair2Border");
        for (int i = 0; i < temp.Length; i++)
        {
            DestroyImmediate(temp[i]);
            DestroyImmediate(borderTemp[i]);
        }

        for(int j = 0; j < borderTemp2.Length; j++)
        {
            DestroyImmediate(borderTemp2[j]);
        }

        listForMain.Clear();
        orderedIds.Clear();
        ids1.Clear();
        ids2.Clear();
        textures1.Clear();
        textures2.Clear();
        cardManager.containerOfCards1.Clear();
        cardManager.containerOfCards2.Clear();

        counterForPairGame = 0;
        evaluateCards.gameObject.SetActive(false);

        newBundle();

        newAssetDrawer.setDrawNewAssetValue(false);
        loadAsset.loadAllCards(false);

        pairThem();
    }
    #endregion

    //----------------------------------------------------------------------------------------------------------------------------------

    #region NewArrivalGameFunctions
    //Functions for game no. 3 (New Arrival):

    //A játék legvégére az előkészítés, ennek lefutása után tudunk tippelni, hogy melyik az új felszálló
    IEnumerator tippingPosition(float n)
    {
        yield return new WaitForSeconds(n);
        shuffleIndexes(ids1);
        objs = GameObject.FindGameObjectsWithTag("Parent");
        GameObject[] cards = GameObject.FindGameObjectsWithTag("CardModel");
        for (int i = 0; i < objs.Length; i++)
        {
            for (int j = 0; j < ids1.Count; j++)
            {
                if (cards[j].GetComponent<CardModel>().getUniqueCardId() == (int)ids1[i])   //getCardId volt
                {
                    GameObject go = objs[j];
                    go.GetComponent<ParentScript>().reveal();
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }
        //Engedélyezzük a tippet
        canBeSelected = true;
    }


    //Kisorsolja az új felszállót, eltárolja az indexét majd felteszi a pályára 
    IEnumerator WaitaBitAndAddNewArrival(float n)
    {
        //Beteszünk egy új kártyát miután felfordítottuk a többit, ő lesz az új felszálló
        yield return new WaitForSeconds(n);
        GameObject[] parent = GameObject.FindGameObjectsWithTag("instantiateParent");
        loadAsset.loadNewArrival();
        ids1.Add(idOfNewArrival);
        var card = Instantiate(parent[0], new Vector3(-10, 5, 0), Quaternion.identity); //-13 volt a -10
        card.tag = "Parent";
        card.GetComponentInChildren<CardModel>().tag = "CardModel";
        card.GetComponentInChildren<CardModel>().rend.materials[2].mainTexture = textures1[textures1.Count - 1];
        card.GetComponentInChildren<CardModel>().setUniqueCardId(idOfNewArrival);   //setCardId volt
        card.GetComponentInChildren<CardModel>().setCardId(cardManager.containerOfCards1[cardManager.containerOfCards1.Count - 1].getCardId());

        GameObject[] cards = GameObject.FindGameObjectsWithTag("CardModel");
        camera.transform.localPosition = new Vector3(0, 0, cameraZPosNewArrival[cards.Length]);

        positionForSmoothStep(card, 0, 0, gameLevel * -0.2f, true, Constants.speedOfArrivalZeroing);
    }

    IEnumerator waitAndPositionAllCardsInZero(float n)
    {
        yield return new WaitForSeconds(n);
        float z = 0;
        for (int i = 0; i < gameLevel; i++)
        {
            GameObject[] parent = GameObject.FindGameObjectsWithTag("Parent");

            positionForSmoothStep(parent[i], 0, 0, z, true, Constants.speedOfZeroing);

            z -= 0.2f;
        }
    }

    //Az új felszálló után megkever mindent és kiteszi őket elrendezve
    IEnumerator waitABitAndDoAll(float n)
    {
        yield return new WaitForSeconds(n);

        // 1. lépés Megkeressük az összes kártyát
        GameObject[] parent = GameObject.FindGameObjectsWithTag("Parent");

        // 2. lépés Átrendezzük a kártyák sorrendjét
        shuffleIndexes(ids1);

        //3. Pozícionáljuk a kártyákat
        positionCards2(parent);
    }

    //Felfordítja a lapokat egyesével kettő másodpercre majd vissza
    IEnumerator waitABitAndReveal(float n, ParentScript ps)
    {
        if (ps != null)
        {
            yield return new WaitForSeconds(n);
            ps.revealAndHide();
        }
    }

    //Az elején elrendezi a kártyákat
    private void startPositioning(GameObject[] parent, float xOffset, float yOffset, float zOffset)
    {
        float x = 0;
        float y = 0;
        float z = 0;
        for (int i = 0; i < parent.Length; i++)
        {
            double val = parent.Length;
            parent[i].transform.position = new Vector3(x, y, z);
            x -= xOffset;
            y -= yOffset;
            z -= zOffset;

            if (i == Math.Ceiling(val))
            {
                x = 0;
                y = 0;
                z = 0;
                xOffset *= -1;
                yOffset *= -1;
                zOffset *= -1;
            }
        }
    }

    private void positionCards2(GameObject[] objs)      //Pálya szélessége: -5.34 + 1.812/2 = 6,246 * 2 = 12,492
    {
        double xUpper = 0;
        double xLower = 0;
        double yUpper = 0;
        double yLower = 0;

        yUpper = Constants.yUp;
        yLower = Constants.yDown;

        if (objs.Length < 6)
        {
            xUpper = calcx(objs.Length, 1);
            yUpper = Constants.yMid;
            pos2(objs, xUpper, yUpper, 0, 0);
        }
        else
        {
            if (objs.Length % 2 == 0)
            {
                xUpper = calcx(objs.Length / 2, 1);
                xLower = xUpper;
                pos2(objs, xUpper, yUpper, xLower, yLower);
            }
            else
            {
                double val = objs.Length;
                xUpper = calcx(Math.Ceiling(val / 2), 1);
                xLower = calcx(Math.Floor(val / 2), 1);
                pos2(objs, xUpper, yUpper, xLower, yLower);
            }
        }
    }

    private void pos2(GameObject[] objs, double xUpper, double yUpper, double xLower, double yLower)
    {
        double cardCount = objs.Length;
        for (int i = 0; i < cardCount; i++)
        {
            for (int j = 0; j < ids1.Count; j++)
            {
                if (objs[j].GetComponentInChildren<CardModel>().getUniqueCardId() == (int)ids1[i])   //getCardID volt
                {
                    Vector3 b = new Vector3((float)xUpper, (float)yUpper, 0);
                    positionForSmoothStep(objs[j], b.x, b.y, b.z, true, Constants.speedOfFirstPositioning);

                    xUpper += Constants.padding + Constants.cardSize;
                    if (objs.Length > 5 && Math.Ceiling(cardCount / 2) - 1 == i)
                    {
                        xUpper = xLower;
                        yUpper = yLower;
                    }
                }
            }
        }
    }

    public void guessedRight(bool right)
    {
        newBundle();

        if (right)
        {
            rightGuesses += 1;
            wrongGuesses = 0;
            Debug.Log("Ügyes vagy eltaláltad");
            if (rightGuesses == 2)
            {
                rightGuesses = 0;                //Emeljük a tétet
                if (gameLevel < Constants.maxLevelOfNewArrival)
                {
                    newGameStarted(1);
                }
                else
                {
                    newGameStarted(3);
                }
            }
            else
            {
                newGameStarted(3);    //Tartjuk a tétet
            }
        }
        else
        {
            wrongGuesses += 1;
            rightGuesses = 0;
            Debug.Log("Sajnos ez most nem sikerült");
            if (wrongGuesses == 2)
            {
                wrongGuesses = 0;    //Csökkentjük a tétet
                if (gameLevel > Constants.minLevelOfNewArrival)
                {
                    newGameStarted(2);
                }
                else
                {
                    newGameStarted(3);
                }
            }
            else
            {
                newGameStarted(3);  //Tartjuk a tétet
            }
        }
    }


    //Új játékot indít
    public void newGameStarted(int value)
    {
        //Beállítja a játék szintjét
        switch (value)
        {
            case 1:
                gameLevel += 1;
                break;
            case 2:
                gameLevel -= 1;
                break;
            case 3:
                break;
            default:
                Debug.Log("There is no such case as given case");
                break;
        }

        //Visszaállítja a játékot a kezdő állapotra
        resetNewArrival();

        //Elindítja az új játékot
        //newArrival();
    }

    public void resetNewArrival()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Parent");
        for (int i = 0; i < temp.Length; i++)
        {
            DestroyImmediate(temp[i]);
        }

        //Visszaállítjuk a várakozásokat a helyes értékekre
        waitTimeForReveal = waitTimeForRevealReference;
        canBeSelected = false;


        cardManager.containerOfCards1.Clear();
        cardManager.containerOfCards2.Clear();

        //Kiürítjuk a listánkat
        listForMain.Clear();

        //Kiüríti az id listát
        ids1.Clear();

        //Üríti a textúrák listáját
        textures1.Clear();

        if(newAssetDrawer.getDrawNewAssetValue() == false)
        {
            //Újra kisorsolunk textúrákat
            loadAsset.loadAllCards(false);
        }
        else
        {
            //DrawNewAsset value értékét visszaállítjuk
            newAssetDrawer.setDrawNewAssetValue(false);
        }
    }
    #endregion

    //----------------------------------------------------------------------------------------------------------------------------------

    #region BasicFunctions
    //Basic funtions that all games use:
    private void loadInObjects()
    {
        GameObject[] temp1 = GameObject.FindGameObjectsWithTag("CardModel");
        for (int i = 0; i < temp1.Length; i++)
        {
            listForMain.Add(temp1[i]);
        }
    }

    public void positionForSmoothStep(GameObject parent, float xEnd, float yEnd, float zEnd, bool canStart, float duration)
    {
        ParentScript parentScript = parent.GetComponent<ParentScript>();

        parentScript.startTime = Time.time;

        parentScript.xStart = parent.transform.position.x;
        parentScript.yStart = parent.transform.position.y;
        parentScript.zStart = parent.transform.position.z;

        parentScript.xEnd = xEnd;
        parentScript.yEnd = yEnd;
        parentScript.zEnd = zEnd;

        parentScript.duration = duration;
        parentScript.canStart = canStart;
        parentScript.t = 0;
    }


    IEnumerator shuffle(GameObject[] parent, float n)
    {
        yield return new WaitForSeconds(n);
        float x = 2 * Constants.cardSize;
        for (int i = 0; i < 4; i++)
        {
            positionForSmoothStep(parent[i], x, parent[i].transform.position.y, (gameLevel + 1 + i) * -0.2f, true, Constants.speedOfThreeShuffles);

            yield return new WaitForSeconds(Constants.speedOfThreeShuffles);

            positionForSmoothStep(parent[i], 0, 0, parent[i].transform.position.z, true, Constants.speedOfThreeShuffles);

            yield return new WaitForSeconds(Constants.speedOfThreeShuffles + 0.1f);
        }
    }


    //Reload bundles if we have clicked on the newAssetBundle button
    public void newBundle()
    {
        if(newAssetDrawer.getDrawNewAssetValue())
        {
            newAssetDrawer.DrawNewAsset();
        }
        else
        {
            Debug.Log("Most nem cseréltünk kártya setet");
        }
    }

    //Egy lista tartalmát megkeveri
    public void shuffleIndexes(ArrayList list)
    {
        RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
        int n = list.Count;
        while (n > 1)
        {
            byte[] box = new byte[1];
            do provider.GetBytes(box);
            while (!(box[0] < n * (Byte.MaxValue / n)));
            int k = (box[0] % n);
            n--;
            int value = (int)list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private double calcx(double cardNumber, float scale)
    {
        return -(((cardNumber / 2) - 0.5f) * ((Constants.cardSize * scale) + Constants.padding));
    }

    public GameObject findParentObjectByID(int id)
    {
        GameObject[] parents = GameObject.FindGameObjectsWithTag("Parent");
        foreach(GameObject GO in parents)
        {
            if(GO.GetComponentInChildren<CardModel>().getUniqueCardId() == id)  //getCardId volt
            {
                return GO;
            }
        }
        return null;
    }

    public void resetGame()
    {
        GameObject[] borderTemp = GameObject.FindGameObjectsWithTag("Border");
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Parent");
        GameObject[] borderTemp2 = GameObject.FindGameObjectsWithTag("pair2Border");

        switch (GameData.instance.getGameID())
        {
            //New Arrival
            case 1:
                waitTimeForReveal = waitTimeForRevealReference;
                canBeSelected = false;
                break;

            //PairGame
            case 2:
                for (int j = 0; j < borderTemp.Length; j++)
                {
                    DestroyImmediate(borderTemp[j]);
                }

                for (int j = 0; j < borderTemp2.Length; j++)
                {
                    DestroyImmediate(borderTemp2[j]);
                }

                cardManager.containerOfCards2.Clear();
                ids2.Clear();
                textures2.Clear();
                counterForPairGame = 0;
                evaluateCards.gameObject.SetActive(false);
                break;

            //OrderGame
            case 3:
                orderedIds.Clear();

                for (int j = 0; j < borderTemp.Length; j++)
                {
                    DestroyImmediate(borderTemp[j]);
                }
                break;
            default:
                Debug.Log("No such case as given");
                break;
        }

        for (int i = 0; i < temp.Length; i++)
        {
            DestroyImmediate(temp[i]);
        }

        cardManager.containerOfCards1.Clear();
        listForMain.Clear();
        ids1.Clear();
        textures1.Clear();

        newBundle();

        if (newAssetDrawer.getDrawNewAssetValue() == false)
        {
            loadAsset.loadAllCards(false);
        }
        else
        {
            newAssetDrawer.setDrawNewAssetValue(false);
        }
    }

    public void guessedRight2(bool right)
    {
        if (right)
        {
            rightGuesses += 1;
            wrongGuesses = 0;
            Debug.Log("Ügyes vagy eltaláltad");
            if (rightGuesses == 2)
            {
                rightGuesses = 0;                //Emeljük a tétet
                if(GameData.instance.getGameID() == 1) //NewArrival
                {
                    if(gameLevel < Constants.maxLevelOfNewArrival)
                    {
                        gameLevel++;
                    }
                }
                else
                {
                    if(gameLevel < Constants.maxLevelOfOrderGame)
                    {
                        gameLevel++;
                    }
                }
            }
        }
        else
        {
            wrongGuesses += 1;
            rightGuesses = 0;
            Debug.Log("Sajnos ez most nem sikerült");
            if (wrongGuesses == 2)
            {
                wrongGuesses = 0;    //Csökkentjük a tétet
                if(GameData.instance.getGameID() == 1) //NewArrival
                {
                    if(gameLevel > Constants.minLevelOfNewArrival)
                    {
                        gameLevel--;
                    }
                }
                else
                {
                    if(gameLevel > Constants.minLevelOfOrderGame)
                    {
                        gameLevel--;
                    }
                }
            }
        }
        resetGame();
    }

    #endregion

    //----------------------------------------------------------------------------------------------------------------------------------

    #region Getters / Setters
    //Getters and setters for variables:
    public bool getCanBeSelected()
    {
        return canBeSelected;
    }
    public void setIdOfNewArrival(int id)
    {
        idOfNewArrival = id;
    }
    public int getGameLevel()
    {
        return gameLevel;
    }
    public int getIdOfNewArrival()
    {
        return idOfNewArrival;
    }

    #endregion
}