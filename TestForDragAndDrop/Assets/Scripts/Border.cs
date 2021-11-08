using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//This class contains all the data for a border in the game (object where u can drag the card)
public class Border : MonoBehaviour
{
    //bool for indicate if the border is occupied
    [SerializeField] private bool isOccupied = false;

    //bool for turn on and off a border
    [SerializeField] private bool isAvailable = false;
    
    //bool for indicate if the border will contain card when the game starts
    [SerializeField] private bool isStarter = false;

    //Reference to the border's card
    [SerializeField] private CardModel card = null;

    //The card's id
    [SerializeField] private int id;


    //Getters and setters for the variables:

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
