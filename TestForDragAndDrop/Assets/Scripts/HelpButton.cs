using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    public void OnMouseDown()
    {
        ;
    }

    public void OnMouseEnter()
    {
        GameController.instance.informationText.text = "Ez egy kis információs ablak.";
        GameController.instance.informationText.gameObject.SetActive(true);
    }

    public void OnMouseExit()
    {
        GameController.instance.informationText.gameObject.SetActive(false);
    }
}

