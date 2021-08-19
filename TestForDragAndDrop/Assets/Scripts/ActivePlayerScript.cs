using UnityEngine;
using UnityEngine.UI;

public class ActivePlayerScript : MonoBehaviour
{
    [SerializeField] private Toggle toggle1;
    [SerializeField] private Toggle toggle2;
    [SerializeField] private Toggle activePlayer;
    [SerializeField] private InputField inputField;

    void Update()
    {
        if((toggle1.isOn || toggle2.isOn) && inputField.text != "")
        {
            activePlayer.interactable = true;
        }
        else
        {
            activePlayer.interactable = false;
            activePlayer.isOn = false;
        }
    }
}
