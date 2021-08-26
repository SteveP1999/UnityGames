using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private Sprite playerImage1;
    [SerializeField] private Sprite playerImage2;
    [SerializeField] private Sprite playerImage3;
    [SerializeField] private Text mainText;
    [SerializeField] private Text nameText;

    void Start()
    {
        GameObject go = GameObject.Find("ProfileManager");

        switch (go.GetComponent<ProfileManager>().getActivePlayerIndex())
        {
            case 0:
                playerImage.sprite = playerImage1;
                nameText.text = go.GetComponent<ProfileManager>().profiles[0].getName();
                break;
            case 1:
                playerImage.sprite = playerImage2;
                nameText.text = go.GetComponent<ProfileManager>().profiles[1].getName();
                break;
            case 2:
                playerImage.sprite = playerImage3;
                nameText.text = go.GetComponent<ProfileManager>().profiles[2].getName();
                break;
            default:
                Debug.Log("No such case as given");
                break;
        }

        int caseSwitch = 2;
        switch (caseSwitch)
        {
            case 1:
                mainText.text = "Ki az új felszálló?";
                break;
            case 2:
                mainText.text = "Állítsd párba a lapokat!";
                break;
            case 3:
                mainText.text = "Rakd sorba a lapokat";
                break;
            default:
                Debug.Log("No such case as given");
                break;
        }
    }
}
