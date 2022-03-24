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
        API api = new API();
        switch (api.data.chosenGameMode)
        {
            case 1:
                mainText.text = "Ki az új felszálló";
                Debug.Log("megvan");
                break;
            case 2:
                mainText.text = "Rendezd párba és sorrendbe";
                break;
            case 3:
                mainText.text = "Állítsd párba a lapokat";
                break;
            default:
                Debug.Log("No such case as given");
                break;
        }
    }
}
