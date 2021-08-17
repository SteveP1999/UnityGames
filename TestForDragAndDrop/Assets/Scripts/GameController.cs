using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

static class Constants
{
    public const float cardSize = 1.812f;
    public const float yMid = 0.0f;
    public const float yUp = 1.0f;
    public const float yDown = -2.0f;
    public const float padding = 0.2f;
    public const float speedOfFirstPositioning = 0.5f;
    public const float speedOfZeroing = 0.3f;
    public const float speedOfArrivalZeroing = 0.5f;
    public const float speedOfThreeShuffles = 0.1f;
    public const float yOrder = 1.2f;
    public const float yOrderBorder = -2.0f;
    public const float speedOfPairGame = 2.0f;
    public const float yPairUpper = 1.5f;
    public const float yPairLower = -1.5f;
    public const float yPairTemp = -4.5f;
    public const float waitTimeInPairGame = 1.0f;
}

public class GameController : MonoBehaviour 
{
    [SerializeField] private Button orderGame;
    [SerializeField] private Button pairGame;
    [SerializeField] private Button giveNextCards;
    [SerializeField] private Button evaluateCards;
    private int maxLevel = 14;
    private int minLevel = 4;
    private int idOfNewArrival = 0; //Itt tárolom az új felszálló indexét
    [SerializeField] private int gameLevel = 6; //Szint, ez határozza meg mennyi kártya van a játékban
    private float waitTimeForRevealReference = 2;
    private float waitTimeForReveal = 2; //Mennyi idő teljen el aközött, hogy kitettük a lapokat és elkezdjük felfordítani őket.
    private float waitBetweenCardsAndNewArrival = 2;  //Mennyi idő teljen el az összes lap felfordítása után ameddig az új felszállót betesszük
    private float waitBetweenCardsFlipping = 2; //Mennyi idő teljen el az egyik lap lefordítása és a másik lap felfordítása között
    private GameObject[] objs; //Ebben tárolom a kártyákat, ha új keletkezik frissítem
    private ArrayList listForMain = new ArrayList();
    private bool canBeSelected = false; //Engedélyező, hogy megtippelhessük az új felszállót
    private int rightGuesses = 0; //Counter a jó tippekhez
    private int wrongGuesses = 0; //Counter a rossz tippekhez
    public List<Texture2D> textures; //A textúrákat itt tároljuk el, amiket betöltünk
    public LoadAsset loadAsset; //Hivatkozás a LoadAsset osztályra
    new private GameObject camera;
    private readonly float[] cameraZPos = { -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -8.0f, -10.0f, -10.0f, -10.0f };
    private readonly float[] cameraZPosOrder = { -10.0f, -10.0f, -10.0f, -10.0f, -10.0f, -10.0f, -10.0f, -10.0f, -10.0f, -10.0f, -10.0f, -12.0f, -12.0f, -12.0f };


    void Awake()
    {
        camera = GameObject.Find("CardCamera");
    }

    public void putThemInOrder()
    {
        ArrayList orderedIds = new ArrayList();

        orderGame.gameObject.SetActive(true);

        camera.transform.localPosition = new Vector3(0, 0, cameraZPosOrder[gameLevel - 1]);

        GameObject[] parent = GameObject.FindGameObjectsWithTag("instantiateParent");
        GameObject[] border = GameObject.FindGameObjectsWithTag("instantiateBorder");

        //Felrakom a kártyákat
        for (int i = 0; i < gameLevel; i++)
        {
            var card = Instantiate(parent[0], new Vector3(0, 0, 0), Quaternion.identity);
            var bord = Instantiate(border[0], new Vector3(0, 10, 0), Quaternion.identity);
            var starterBord = Instantiate(border[0], new Vector3(0, 10, 0), Quaternion.identity);

            card.tag = "Parent";
            card.GetComponentInChildren<CardModel>().tag = "CardModel";
            card.GetComponentInChildren<CardModel>().setBorder(starterBord.GetComponent<Border>());

            card.transform.Rotate(0, 180, 0);

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
            ((GameObject)listForMain[i]).GetComponent<CardModel>().rend.materials[2].mainTexture = textures[i];
            ((GameObject)listForMain[i]).GetComponent<CardModel>().setCardId((int)loadAsset.getIds()[i]);
        }

        parent = GameObject.FindGameObjectsWithTag("Parent");
        
        border = GameObject.FindGameObjectsWithTag("Border");

        GameObject[] starterBorder = GameObject.FindGameObjectsWithTag("starterBorder");

        shuffleIndexes(loadAsset.getIds());

        startPositioning(parent, 0.2f, 0.2f, 0.1f);

        posCardsForOrder(parent, Constants.yOrder);
        posCardsForOrder(starterBorder, Constants.yOrder);

        for(int i = 0; i < starterBorder.Length; i++)
        {
            starterBorder[i].tag = "Border";
        }

        posCardsForOrder(border, Constants.yOrderBorder);

        //Felfordítjuk a kártyákat egyesével
        StartCoroutine(revealCards(parent));
    }

    private void posCardsForOrder(GameObject[] parent, float y)
    {
        double x = calcx(parent.Length, 1.0f);
        for(int i = 0; i < parent.Length; i++)
        {
            parent[i].transform.position = new Vector3((float)x, y, 0);
            x += Constants.padding + Constants.cardSize;
        }
    }

    IEnumerator revealCards(GameObject[] parent)
    {
        for (int j = 0; j < listForMain.Count; j++)
        {
            for (int i = 0; i < loadAsset.getIds().Count; i++)
            {
                if (parent[i].GetComponentInChildren<CardModel>().getCardId() == (int)loadAsset.getIds()[j])
                {
                    GameObject go = parent[i];
                    ParentScript ps = go.GetComponent<ParentScript>();

                    if (ps != null)
                    {
                        ps.revealAndHide();
                        yield return new WaitForSeconds(2);
                    }
                }
            }
        }

        for (int i = 0; i < listForMain.Count; i++)
        {
            parent[i].GetComponentInChildren<DragAndDrop>().setDrag(true);
        }
    }

    public void evaluateOrder(ArrayList ids)
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
                if (borders[j].GetComponent<Border>().getId() == (int)ids[j])
                {
                    counter++;
                }
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

        //resetOrderGame();
    }

    private void resetOrderGame()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Parent");
        GameObject[] borderTemp = GameObject.FindGameObjectsWithTag("Border");
        for (int i = 0; i < temp.Length; i++)
        {
            DestroyImmediate(temp[i]);
            DestroyImmediate(borderTemp[i]);
        }
    }


    public void pairThem()
    {
        camera.transform.localPosition = new Vector3(0, 0, -14);

        pairGame.gameObject.SetActive(true);

        GameObject[] parent = GameObject.FindGameObjectsWithTag("instantiateParent");

        float x = -5;
        float y = 10;
        float z = 0;

        //Felrakom a kártyákat
        for (int i = 0; i < 2 * gameLevel; i++)
        {
            var card = Instantiate(parent[0], new Vector3(x, y, z), Quaternion.identity);
            card.tag = "Parent";
            card.GetComponentInChildren<CardModel>().tag = "CardModel";
            card.transform.Rotate(0, 180, 0);

            if(i == gameLevel - 1)
            {
                x = 5;
            }
        }

        loadInObjects();

        for (int i = 0; i < listForMain.Count/2; i++)
        {
            ((GameObject)listForMain[i]).GetComponent<CardModel>().rend.materials[2].mainTexture = textures[i];
            ((GameObject)listForMain[i]).GetComponent<CardModel>().setCardId((int)loadAsset.getIds()[i]);
        }

        for (int j = 0; j < gameLevel; j++)
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

    IEnumerator showPair()
    {
        GameObject[] parent = GameObject.FindGameObjectsWithTag("Parent");

        for (int i = 0; i < gameLevel; i++)
        {
            parent[i].GetComponentInChildren<CardModel>().setPair(parent[2 * gameLevel - i - 1]);

            positionForSmoothStep(parent[i], -2, 0, 0, true, Constants.speedOfPairGame);

            positionForSmoothStep(parent[2 * gameLevel - i - 1], 2, 0, 0, true, Constants.speedOfPairGame);

            yield return new WaitForSeconds(Constants.waitTimeInPairGame);

            positionForSmoothStep(parent[i], -5, -10, 0, true, Constants.speedOfPairGame);

            positionForSmoothStep(parent[2 * gameLevel - i - 1], 5, -10, 0, true, Constants.speedOfPairGame);
        }

        yield return new WaitForSeconds(2);

        double x = calcx(gameLevel, 1);

        GameObject[] starterBorder = GameObject.FindGameObjectsWithTag("starterBorder");
        GameObject[] pair1Border = GameObject.FindGameObjectsWithTag("pair1Border");
        GameObject[] pair2Border = GameObject.FindGameObjectsWithTag("pair2Border");

        for (int j = 0; j < gameLevel; j++)
        {
            starterBorder[j].GetComponent<Border>().transform.position = new Vector3((float)x, Constants.yPairTemp, 0);
            starterBorder[j].GetComponent<Border>().tag = "Border";
            starterBorder[j].GetComponent<Border>().setOccupied(true);
            starterBorder[j].GetComponent<Border>().setCard(parent[j].GetComponentInChildren<CardModel>());
            starterBorder[j].GetComponent<Border>().setId(parent[j].GetComponentInChildren<CardModel>().getCardId());

            pair1Border[j].GetComponent<Border>().transform.position = new Vector3((float)x, Constants.yPairLower, 0);

            pair2Border[j].GetComponent<Border>().transform.position = new Vector3((float)x, Constants.yPairUpper, 0);
            pair2Border[j].GetComponent<Border>().tag = "Border";

            parent[j].transform.position = new Vector3((float)x, Constants.yPairTemp, 0);
            parent[j].GetComponentInChildren<CardModel>().setBorder(starterBorder[j].GetComponent<Border>());
            parent[j].GetComponentInChildren<DragAndDrop>().setDrag(true);

            x += Constants.padding + Constants.cardSize;

            giveNextCards.gameObject.SetActive(true);
        }
    }

    public void giveNextCard()
    {
        GameObject[] borders = GameObject.FindGameObjectsWithTag("Border");
        int counter = 0;
        for(int i = 0; i < borders.Length; i++)
        {
            if(borders[i].transform.position.y == Constants.yPairUpper && borders[i].GetComponent<Border>().getOccupied() == true)
            {
                counter++;
            }
        }
        if(counter == gameLevel)
        {
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
                starterBorder[j - gameLevel].GetComponent<Border>().tag = "Border";
                starterBorder[j - gameLevel].GetComponent<Border>().setOccupied(true);
                starterBorder[j - gameLevel].GetComponent<Border>().setCard(parent[j].GetComponentInChildren<CardModel>());
                starterBorder[j - gameLevel].GetComponent<Border>().setId(parent[j].GetComponentInChildren<CardModel>().getCardId());

                parent[j].transform.position = new Vector3((float)x, Constants.yPairTemp, 0);
                parent[j].GetComponentInChildren<CardModel>().setBorder(starterBorder[j - gameLevel].GetComponent<Border>());
                parent[j].GetComponentInChildren<DragAndDrop>().setDrag(true);

                pair1Border[j - gameLevel].GetComponent<Border>().tag = "Border";
                pair1Border[j - gameLevel].GetComponent<Border>().setIsAvailable(true);

                x += Constants.padding + Constants.cardSize;

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


        //resetPairGame();
    }

    public void resetPairGame()
    {
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Parent");
        GameObject[] borderTemp = GameObject.FindGameObjectsWithTag("Border");
        for (int i = 0; i < temp.Length; i++)
        {
            DestroyImmediate(temp[i]);
            DestroyImmediate(borderTemp[i]);
        }
    }

    public void newArrival()
    {
        camera.transform.localPosition = new  Vector3(0, 0, cameraZPos[gameLevel - 1]);

        float delay = gameLevel * waitBetweenCardsFlipping + waitTimeForReveal + waitBetweenCardsAndNewArrival - 2;

        GameObject[] parent = GameObject.FindGameObjectsWithTag("instantiateParent");

        //Felrakom a kártyákat
        for (int i = 0; i < gameLevel; i++)
        {
            var card = Instantiate(parent[0], new Vector3(0, 0, 0), Quaternion.identity);
            card.tag = "Parent";
            card.GetComponentInChildren<CardModel>().tag = "CardModel";
        }

        loadInObjects();

        for(int i = 0; i < listForMain.Count; i++)
        {
            ((GameObject)listForMain[i]).GetComponent<CardModel>().rend.materials[2].mainTexture = textures[i];
            ((GameObject)listForMain[i]).GetComponent<CardModel>().setCardId((int)loadAsset.getIds()[i]);
        }

        //Megkeressük az összes kártyát és megfelelően pozícionáljuk őket
        parent = GameObject.FindGameObjectsWithTag("Parent");

        shuffleIndexes(loadAsset.getIds());

        startPositioning(parent, 0.2f, 0.2f, 0.1f);

        positionCards2(parent, 1);

        //Felfordítjuk a kártyákat egyesével
        for (int j = 0; j < listForMain.Count; j++)
        {
            for (int i = 0; i < loadAsset.getIds().Count; i++)
            {
                if (parent[i].GetComponentInChildren<CardModel>().getCardId() == (int)loadAsset.getIds()[j])
                {
                    GameObject go = parent[i];
                    ParentScript ps = go.GetComponent<ParentScript>();
                    StartCoroutine(waitABitAndReveal(waitTimeForReveal, ps));
                    waitTimeForReveal += waitBetweenCardsFlipping;
                }
            }
        }

        StartCoroutine(waitAndPositionAllCardsInZero(delay + 5));
        
        //Kirakjuk az új felszállót és becsúsztatjuk a 0,0-ba
        StartCoroutine(WaitaBitAndAddNewArrival(delay + 6));

        //Ide jön a shuffle:
        StartCoroutine(shuffle(parent, delay + 7));
        
        //Megkeverjük, újra kitesszük
        StartCoroutine(waitABitAndDoAll(delay + 9));

        //Felfordítjuk egyesével a kártyákat és várunk a tippre
        StartCoroutine(tippingPosition(delay + 10));    
    } 

    private void loadInObjects()
    {
        GameObject[] temp1 = GameObject.FindGameObjectsWithTag("CardModel");
        for(int i = 0; i < temp1.Length; i++)
        {
            listForMain.Add(temp1[i]);
        }
    }

    //A játék legvégére az előkészítés, ennek lefutása után tudunk tippelni, hogy melyik az új felszálló
    IEnumerator tippingPosition(float n)
    {
        yield return new WaitForSeconds(n);
        objs = GameObject.FindGameObjectsWithTag("Parent");
        GameObject[] cards = GameObject.FindGameObjectsWithTag("CardModel");
        for (int i = 0; i < objs.Length; i++)
        {
            for (int j = 0; j < loadAsset.getIds().Count; j++)
            {
                if (cards[j].GetComponent<CardModel>().getCardId() == (int)loadAsset.getIds()[i])
                {
                    GameObject go = objs[i];
                    go.GetComponent<ParentScript>().reveal();
                    //yield return new WaitForSeconds(0.5f);
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
        loadAsset.addNewArrival();
        var card = Instantiate(parent[0], new Vector3(-13, 5, 0), Quaternion.identity);
        card.tag = "Parent";
        card.GetComponentInChildren<CardModel>().tag = "CardModel";
        card.GetComponentInChildren<CardModel>().rend.materials[2].mainTexture = textures[textures.Count - 1];
        card.GetComponentInChildren<CardModel>().setCardId(idOfNewArrival);

        positionForSmoothStep(card, 0, 0, gameLevel * -0.2f, true, Constants.speedOfArrivalZeroing);
    }

    public void positionForSmoothStep(GameObject parent, float xEnd, float yEnd, float zEnd, bool canStart, float duration)
    {
        parent.GetComponent<ParentScript>().startTime = Time.time;

        parent.GetComponent<ParentScript>().xStart = parent.transform.position.x;
        parent.GetComponent<ParentScript>().yStart = parent.transform.position.y;
        parent.GetComponent<ParentScript>().zStart = parent.transform.position.z;

        parent.GetComponent<ParentScript>().xEnd = xEnd;
        parent.GetComponent<ParentScript>().yEnd = yEnd;
        parent.GetComponent<ParentScript>().zEnd = zEnd;

        parent.GetComponent<ParentScript>().duration = duration;
        parent.GetComponent<ParentScript>().canStart = canStart;
        parent.GetComponent<ParentScript>().t = 0;
    }


    IEnumerator waitAndPositionAllCardsInZero(float n)
    {
        yield return new WaitForSeconds(n);
        float z = 0;
        for(int i = 0; i < gameLevel; i++)
        {            
            GameObject[] parent = GameObject.FindGameObjectsWithTag("Parent");

            positionForSmoothStep(parent[i], 0, 0, z, true, Constants.speedOfZeroing);

            z -= 0.2f;
        }
    }

    IEnumerator shuffle(GameObject[] parent, float n)
    {
        yield return new WaitForSeconds(n);
        float x = 2 * Constants.cardSize;
        for(int i = 0; i < 3; i++)
        {
            positionForSmoothStep(parent[i], x, parent[i].transform.position.y, (gameLevel + 1 + i) * -0.2f, true, Constants.speedOfThreeShuffles);

            yield return new WaitForSeconds(Constants.speedOfThreeShuffles);

            positionForSmoothStep(parent[i], 0, 0, parent[i].transform.position.z, true, Constants.speedOfThreeShuffles);

            yield return new WaitForSeconds(0.5f);
        }
    }


    //Az új felszálló után megkever mindent és kiteszi őket elrendezve
    IEnumerator waitABitAndDoAll(float n)
    {
        yield return new WaitForSeconds(n);

        // 1. lépés Megkeressük az összes kártyát
        GameObject[] parent = GameObject.FindGameObjectsWithTag("Parent");

        // 2. lépés Átrendezzük a kártyák sorrendjét
        shuffleIndexes(loadAsset.getIds());

        //3. Pozícionáljuk a kártyákat
        positionCards2(parent, 0);
    }

    //Felfordítja a lapokat egyesével kettő másodpercre majd vissza
    IEnumerator waitABitAndReveal(float n, ParentScript ps)
    {
        if(ps != null)
        {
            yield return new WaitForSeconds(n);
            ps.revealAndHide();
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

    //Az elején elrendezi a kártyákat, 1. variáns
    private void startPositioning(GameObject[] parent, float xOffset, float yOffset, float zOffset)
    {
        float x = 0;
        float y = 0;
        float z = 0;
        for(int i = 0; i < parent.Length; i++)
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

    private void positionCards2(GameObject[] objs, float n)      //Pálya szélessége: -5.34 + 1.812/2 = 6,246 * 2 = 12,492
    {
        //yield return new WaitForSeconds(n);
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
        int waitBetween = 2;
        double val = objs.Length;
        for (int i = 0; i < objs.Length; i++)
        {
            for (int j = 0; j < loadAsset.getIds().Count; j++)
            {
                if (objs[j].GetComponentInChildren<CardModel>().getCardId() == (int)loadAsset.getIds()[i])
                {
                    Vector3 b = new Vector3((float)xUpper, (float)yUpper, 0);

                    positionForSmoothStep(objs[j], b.x, b.y, b.z, true, Constants.speedOfFirstPositioning);

                    waitBetween += 2;

                    xUpper += Constants.padding + Constants.cardSize;
                    if (objs.Length > 5 && Math.Ceiling(val / 2) - 1 == i)
                    {
                        xUpper = xLower;
                        yUpper = yLower;
                    }
                }
            }
        }
    }

    // Ez volt: return -(cardNumber / 2) * (Constants.cardSize * scale) - Constants.padding / 2;
    private double calcx(double cardNumber, float scale)
    {
        return -(((cardNumber / 2) - 0.5f) * ((Constants.cardSize * scale) + Constants.padding));
    }

    public void resetGame()
    {
        //Törli az összes kártyát kivéve egyet
        GameObject[] temp = GameObject.FindGameObjectsWithTag("Parent");
        for (int i = 0; i < temp.Length; i++)
        {
            DestroyImmediate(temp[i]);
        }

        //Visszaállítjuk a várakozásokat a helyes értékekre
        waitTimeForReveal = waitTimeForRevealReference;
        canBeSelected = false;

        //Kiürítjuk a listánkat
        listForMain.Clear();

        //Kiüríti az id listát
        loadAsset.getIds().Clear();

        //Üríti a textúrák listáját
        textures.Clear();

        //Újra kisorsolunk textúrákat
        loadAsset.generateNewIds(gameLevel, loadAsset.getIds());
        loadAsset.loadTextures();
    }


    public void guessedRight(bool right)
    {
        if(right)
        {
            rightGuesses += 1;
            wrongGuesses = 0;
            Debug.Log("Ügyes vagy eltaláltad");
            if (rightGuesses == 2)
            {
                rightGuesses = 0;                //Emeljük a tétet
                if (gameLevel < maxLevel)
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
                newGameStarted(3);                //Tartjuk a tétet
            }
        }
        else
        {
            wrongGuesses += 1;
            rightGuesses = 0;
            Debug.Log("Sajnos ez most nem sikerült");
            if (wrongGuesses == 2)
            {
                wrongGuesses = 0;              //Csökkentjük a tétet
                if (gameLevel > minLevel)
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
                newGameStarted(3);             //Tartjuk a tétet
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
        resetGame();

        //Elindítja az új játékot
        newArrival();
    }


    //Innentől Getter-Setterek:

    public void addToTexture(Texture2D texture)
    {
        textures.Add(texture);
    }

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

}