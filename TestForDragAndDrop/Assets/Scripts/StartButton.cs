using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public GameController gc;
    public void OnMouseDown()
    {
        int caseSwitch = 3;

        switch (caseSwitch)
        {
            case 1:
                gc.newArrival();
                break;
            case 2:
                gc.pairThem();
                break;
            case 3:
                gc.putThemInOrder();
                break;
            default:
                Debug.Log("No such case as given");
                break;
        }
    }
}
