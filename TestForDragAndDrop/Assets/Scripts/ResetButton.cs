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
        profileManager.profiles[id].setName("");
        profileManager.profiles[id].setAge("");
        profileManager.profiles[id].setLevel(4);
        profileManager.profiles[id].setActive(false);
        StartCoroutine(dbManager.saveUser(profileManager.profiles[id].getName(), profileManager.profiles[id].getAge(), profileManager.profiles[id].getLevel(), profileManager.profiles[id].getPlayer(), profileManager.profiles[id].getActive()));
    }
}
