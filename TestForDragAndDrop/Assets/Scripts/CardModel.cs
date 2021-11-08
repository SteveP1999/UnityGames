using System.Collections;
using UnityEngine;
using UnityEngine.UI;


//This class contains the card objects data
public class CardModel : MonoBehaviour
{
    public GameController gc;
    public Renderer rend;
    [SerializeField] private int cardId;
    [SerializeField] private int normalcardId;
    private GameObject pair;
    [SerializeField] private Border border;
    [SerializeField] private StartButton startButton;
    [SerializeField] private Text winOrLost;


    public void setNormalCardId(int number)
    {
        normalcardId = number;
    }

    public int getNormalCardId()
    {
        return normalcardId;
    }

    public void setBorder(Border val)
    {
        border = val;
    }

    public Border getBorder()
    {
        return border;
    }

    public void setPair(GameObject pair)
    {
        this.pair = pair;
    }

    public GameObject getPair()
    {
        return pair;
    }

    public int getCardId()
    {
        return cardId;
    }   

    public void setCardId(int id)
    {
        cardId = id;
    }

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    public void OnMouseDown()
    {

        if (startButton.caseSwitch == 1 && gc.getCanBeSelected())
        {
            GameObject[] parent = GameObject.FindGameObjectsWithTag("Parent");

            for (int i = 0; i < parent.Length; i++)
            {
                if (parent[i].GetComponentInChildren<CardModel>().getCardId() == cardId)
                {
                    gc.positionForSmoothStep(parent[i], 0, 0, -2f, true, 2f);
                }
            }
            if (cardId == gc.getIdOfNewArrival())
            {
                StartCoroutine(wonOrLostMessage(true));
            }
            else
            {
                StartCoroutine(wonOrLostMessage(false));
            }
        }
    }

    IEnumerator wonOrLostMessage(bool won)
    {
        yield return new WaitForSeconds(2.0f);

        if(won)
        {
            winOrLost.text = "Gratulálok, ügyes vagy nyertél!";
            winOrLost.gameObject.SetActive(true);
            gc.guessedRight(true);
        }
        else
        {
            winOrLost.text = "Sajnos ez most nem sikerült, próbáld újra";
            winOrLost.gameObject.SetActive(true);
            gc.guessedRight(false);
        }
    }
}
