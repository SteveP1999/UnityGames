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

    [SerializeField] private CardModel pair;

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

    public void setPair(CardModel pair)
    {
        this.pair = pair;
    }

    public CardModel getPair()
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
        if (API.instance.data.chosenGameMode == 1 && GameController.instance.getCanBeSelected())
        {
            GameController.instance.lockCards();
            if (uniqueCardId == GameController.instance.getIdOfNewArrival())  //cardId volt
            {
                StartCoroutine(wonOrLostMessage(true));
            }
            else
            {
                Color c = new Color(1.0f, 0.5f, 0.9f, 1.0f);
                rend.materials[1].color = c;
                StartCoroutine(wonOrLostMessage(false));
            }
        }
    }

    IEnumerator wonOrLostMessage(bool won)
    {
        //yield return new WaitForSeconds(1.5f);
        ParticleSystem[] particleSystems = FindObjectsOfType<ParticleSystem>();
        if (won)
        {
            GameController.instance.guessedRight(true);
            foreach (ParticleSystem PE in particleSystems)
            {
                PE.Play();
                GameController.instance.positionForSmoothStep(PE.transform.parent.gameObject, 8.5f, 0f, 0f, true, 1.0f);
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
        }
        else
        {
            winOrLost.text = "Sajnos ez most nem sikerült, próbáld újra";
            winOrLost.gameObject.SetActive(true);
            GameController.instance.guessedRight(false);

            yield return new WaitForSeconds(1.0f);

            GameObject newArrival = GameController.instance.findParentObjectByID(GameController.instance.getIdOfNewArrival());
            GameController.instance.positionForSmoothStep(newArrival, newArrival.transform.position.x, newArrival.transform.position.y, -1.0f, true, 1f);

            yield return new WaitForSeconds(3.0f);

            winOrLost.gameObject.SetActive(false);
        }
    }
}
