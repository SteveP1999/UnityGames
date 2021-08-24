using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //This script contains all the stuff that a player has:

    private int money = 0;
    private int level = 0;
    private int actions = 2;
    private Factory factory;

    public void action()
    {
    }

    public void refreshAction()
    {
        actions = 2;
    }

    public void setActive(bool val)
    {
        this.gameObject.SetActive(val);
    }
}
