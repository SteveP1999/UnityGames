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
        inputField1.text = profileManager.subUsers[0].userName;
        inputField2.text = profileManager.subUsers[1].userName;
        inputField3.text = profileManager.subUsers[2].userName;

        if (profileManager.subUsers[0].age == "old")
        {
            toggle1Kicsi.isOn = false;
            toggle1Nagy.isOn = true;
        }
        else if(profileManager.subUsers[0].age == "young")
        {
            toggle1Kicsi.isOn = true;
            toggle1Nagy.isOn = false;
        }
        else
        {
            toggle1Kicsi.isOn = false;
            toggle1Nagy.isOn = false;
        }

        if (profileManager.subUsers[1].age == "old")
            toggle2Kicsi.isOn = false;
            toggle2Nagy.isOn = true;

        if (profileManager.subUsers[2].age == "old")
            toggle3Kicsi.isOn = false;
            toggle3Nagy.isOn = true;

        toggle1Active.isOn = profileManager.subUsers[0].active;
        toggle2Active.isOn = profileManager.subUsers[1].active;
        toggle3Active.isOn = profileManager.subUsers[2].active;
    }
}
