using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public Toggle toggle1;
    public Toggle toggle2;
    public InputField inputField;
    
    public void reset()
    {
        toggle1.isOn = false;
        toggle2.isOn = false;
        inputField.text = "";
    }
}
