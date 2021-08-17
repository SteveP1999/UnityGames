using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    [SerializeField] private bool isOccupied = false;
    [SerializeField] private bool isAvailable = false;
    [SerializeField] private bool isStarter = false;
    [SerializeField] private int id;
    [SerializeField] private CardModel card = null;


    public void setIsStarter(bool val)
    {
        isStarter = val;
    }

    public bool getIsStarter()
    {
        return isStarter;
    }

    public CardModel getCard()
    {
        return card;
    }

    public void setCard(CardModel card)
    {
        this.card = card;
    }

    public void setIsAvailable(bool ava)
    {
        isAvailable = ava;
    }

    public bool getIsAvailable()
    {
        return isAvailable;
    }

    public void setId(int id)
    {
        this.id = id;
    }

    public int getId()
    {
        return id;
    }

    public bool getOccupied()
    {
        return isOccupied;
    }

    public void setOccupied(bool val)
    {
        isOccupied = val;
    }
}
