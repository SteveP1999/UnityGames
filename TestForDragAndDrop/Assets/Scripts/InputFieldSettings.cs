using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputFieldSettings : MonoBehaviour
{
    [SerializeField] private InputField nameInput;
    void Start()
    {
        nameInput.characterLimit = 15;
    }
}
