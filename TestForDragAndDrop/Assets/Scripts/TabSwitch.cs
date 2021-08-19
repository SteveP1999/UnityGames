using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSwitch : MonoBehaviour
{
    [SerializeField] private InputField UsernameInput1;
    [SerializeField] private InputField UsernameInput2;
    [SerializeField] private InputField UsernameInput3;

    private int selectedRowId;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (selectedRowId == 2)
            {
                selectedRowId = 0;
                selectInputField();
            }
            else
            {
                selectedRowId++;
                selectInputField();
            }
        }

        void selectInputField()
        {
            switch (selectedRowId)
            {
                case 0:
                    UsernameInput1.Select();
                    break;
                case 1:
                    UsernameInput2.Select();
                    break;
                case 2:
                    UsernameInput3.Select();
                    break;
            }
        }
    }

    public void UsernameInput1Selected() => selectedRowId = 0;
    public void UsernameInput2Selected() => selectedRowId = 1;
    public void UsernameInput3Selected() => selectedRowId = 2;

}

