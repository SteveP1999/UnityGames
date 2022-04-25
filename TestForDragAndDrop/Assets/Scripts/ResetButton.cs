using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    [SerializeField] private Toggle toggle1;
    [SerializeField] private Toggle toggle2;
    [SerializeField] private InputField inputField;
    [SerializeField] private Toggle activePlayer;
    [SerializeField] private ProfileManager profileManager;
    [SerializeField] private DbManager dbManager;

    public void reset(int id)
    {
        toggle1.isOn = false;
        toggle2.isOn = false;
        activePlayer.isOn = false;
        inputField.text = "";
        profileManager.subUsers[id].userName = "";
        profileManager.subUsers[id].age = "";
        //profileManager.subUsers[id].levelInNewArrival = 4;
        //profileManager.subUsers[id].levelInOrderGame = 4;
        profileManager.subUsers[id].active = false;
        //StartCoroutine(dbManager.saveUser(profileManager.profiles[id].getName(), profileManager.profiles[id].getAge(), profileManager.profiles[id].getLevel(), profileManager.profiles[id].getPlayer(), profileManager.profiles[id].getActive()));
    }
}
