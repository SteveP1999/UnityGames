using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
    public GameController gc;
    public Renderer rend;
    [SerializeField] private int cardId;
    private GameObject pair;
    [SerializeField] private Border border;

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

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }

    public void setCardId(int id)
    {
        cardId = id;
    }

    public int getCardId()
    {
        return cardId;
    }

    public void OnMouseDown()
    {
        if (gc.getCanBeSelected())
        {
            if (cardId == gc.getIdOfNewArrival())
            {
                gc.guessedRight(true);
            }
            else
            {
                gc.guessedRight(false);
            }
        }
    }
}
