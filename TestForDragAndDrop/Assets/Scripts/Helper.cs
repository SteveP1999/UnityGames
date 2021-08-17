using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Helper : MonoBehaviour
{
    public Toggle toggle;

    void Update()
    {
        if(toggle.isOn == true)
        {
            toggle.interactable = false;
        }
        else
        {
            toggle.interactable = true;
        }
    }
}
