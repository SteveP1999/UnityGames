using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    [SerializeField] private InputField inputField1;
    [SerializeField] private InputField inputField2;
    [SerializeField] private InputField inputField3;

    [SerializeField] private Toggle toggle1Kicsi;
    [SerializeField] private Toggle toggle1Nagy;
    [SerializeField] private Toggle toggle2Kicsi;
    [SerializeField] private Toggle toggle2Nagy;
    [SerializeField] private Toggle toggle3Kicsi;
    [SerializeField] private Toggle toggle3Nagy;

    [SerializeField] private Toggle toggle1Active;
    [SerializeField] private Toggle toggle2Active;
    [SerializeField] private Toggle toggle3Active;

    [SerializeField] private ProfileManager profileManager;

    public void setUpMenu()
    {
        inputField1.text = profileManager.profiles[0].getName();
        inputField2.text = profileManager.profiles[1].getName();
        inputField3.text = profileManager.profiles[2].getName();

        if (profileManager.profiles[0].getAge() == "old")
        {
            toggle1Kicsi.isOn = false;
            toggle1Nagy.isOn = true;
        }
        else if(profileManager.profiles[0].getAge() == "young")
        {
            toggle1Kicsi.isOn = true;
            toggle1Nagy.isOn = false;
        }
        else
        {
            toggle1Kicsi.isOn = false;
            toggle1Nagy.isOn = false;
        }

        if (profileManager.profiles[1].getAge() == "old")
            toggle2Kicsi.isOn = false;
            toggle2Nagy.isOn = true;

        if (profileManager.profiles[2].getAge() == "old")
            toggle3Kicsi.isOn = false;
            toggle3Nagy.isOn = true;

        toggle1Active.isOn = profileManager.profiles[0].getActive();
        toggle2Active.isOn = profileManager.profiles[1].getActive();
        toggle3Active.isOn = profileManager.profiles[2].getActive();
    }
}
