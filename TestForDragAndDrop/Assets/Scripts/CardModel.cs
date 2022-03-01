using System.Collections;
using UnityEngine;
using UnityEngine.UI;


//This class contains the card objects data
public class CardModel : MonoBehaviour
{
    #region Variables
    public Renderer rend;

    //The id of the card in its set
    [SerializeField] private int cardId;

    //Unique id of the card
    [SerializeField] private int uniqueCardId;

    private GameObject pair;

    [SerializeField] private Border border;

    [SerializeField] private StartButton startButton;

    [SerializeField] private Text winOrLost;
    #endregion

    #region Getters / Setters

    public void setUniqueCardId(int number)
    {
        uniqueCardId = number;
    }

    public int getUniqueCardId()
    {
        return uniqueCardId;
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
    #endregion

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    public void OnMouseDown()
    {

        if (GameData.instance.getGameID() == 1 && GameController.instance.getCanBeSelected())
        {
            GameObject[] parent = GameObject.FindGameObjectsWithTag("Parent");

            for (int i = 0; i < parent.Length; i++)
            {
                if (parent[i].GetComponentInChildren<CardModel>().getCardId() == cardId)
                {
                    GameController.instance.positionForSmoothStep(parent[i], 0, 0, -2f, true, 2f);
                }
            }
            if (cardId == GameController.instance.getIdOfNewArrival())
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
        yield return new WaitForSeconds(1.5f);
        ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem>();
        if (won)
        {
            foreach (ParticleSystem PE in particleSystems)
            {
                PE.Play();
            }
            winOrLost.text = "Gratulálok, ügyes vagy nyertél!";
            winOrLost.gameObject.SetActive(true);

            yield return new WaitForSeconds(3.0f);
            foreach (ParticleSystem PE in particleSystems)
            {
                PE.Stop();
            }
            winOrLost.gameObject.SetActive(false);
            yield return new WaitForSeconds(2.0f);
            GameController.instance.guessedRight(true);
        }
        else
        {
            winOrLost.text = "Sajnos ez most nem sikerült, próbáld újra";
            winOrLost.gameObject.SetActive(true);

            GameObject chosenCard = GameController.instance.findParentObjectByID(cardId);
            GameController.instance.positionForSmoothStep(chosenCard, 0, 0, -15f, true, 0.5f);

            yield return new WaitForSeconds(0.5f);

            GameObject newArrival = GameController.instance.findParentObjectByID(GameController.instance.getIdOfNewArrival());
            GameController.instance.positionForSmoothStep(newArrival, 0, 0, -2f, true, 2f);

            yield return new WaitForSeconds(3.0f);

            winOrLost.gameObject.SetActive(false);
            GameController.instance.guessedRight(false);
        }
    }
}
