﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private Toggle toggle1;
    [SerializeField] private Toggle toggle2;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Toggle activePlayer;
    public void reset()
    {
        toggle1.isOn = false;
        toggle2.isOn = false;
        activePlayer.isOn = false;
        inputField.text = "";
    }
}
