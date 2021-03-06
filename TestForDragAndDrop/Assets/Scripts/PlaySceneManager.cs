using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlaySceneManager : MonoBehaviour
{
    [SerializeField] private Image playerImage;
    [SerializeField] private Sprite playerImage1;
    [SerializeField] private Sprite playerImage2;
    [SerializeField] private Sprite playerImage3;
    [SerializeField] private Text mainText;
    [SerializeField] private TextMeshProUGUI nameText;

    void Start()
    {
        GameObject go = GameObject.Find("ProfileManager");
        switch (go.GetComponent<ProfileManager>().getActivePlayerIndex())
        {
            case 0:
                playerImage.sprite = playerImage1;
                nameText.text = go.GetComponent<ProfileManager>().subUsers[0].userName;
                break;
            case 1:
                playerImage.sprite = playerImage2;
                nameText.text = go.GetComponent<ProfileManager>().subUsers[1].userName;
                break;
            case 2:
                playerImage.sprite = playerImage3;
                nameText.text = go.GetComponent<ProfileManager>().subUsers[2].userName;
                break;
            default:
                Debug.Log("No such case as given");
                break;
        }
        switch (API.instance.data.chosenGameMode)
        {
            case 1:
                mainText.text = "Ki az új felszálló";
                break;
            case 2:
                mainText.text = "Rendezd párba és sorrendbe";
                break;
            case 3:
                mainText.text = "Rendezd sorba";
                break;
            default:
                Debug.Log("No such case as given");
                break;
        }
    }
}
