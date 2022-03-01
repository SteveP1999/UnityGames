using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameController gc;
    [SerializeField] private Toggle Toggle1;
    [SerializeField] private Toggle Toggle2;
    [SerializeField] private Toggle Toggle3;
    [SerializeField] private ProfileManager profileManager;
    private bool gameOn = false;
    [SerializeField] private Text mainText;


    public void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1 && (Toggle1.isOn || Toggle2.isOn || Toggle3.isOn))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            if(Toggle1.isOn)
            {
                profileManager.profiles[0].setActive(true);
                profileManager.setActivePlayerIndex(0);
            }
            else if (Toggle2.isOn)
            {
                profileManager.profiles[1].setActive(true);
                profileManager.setActivePlayerIndex(1);
            }
            else
            {
                profileManager.profiles[2].setActive(true);
                profileManager.setActivePlayerIndex(2);
            }
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1 && gameOn == false)
        {
            gameOn = true;
            switch (GameData.instance.getGameID())
            {
                case 1:
                    mainText.text = "Ki az új felszálló";
                    gc.newArrival();
                    break;
                case 2:
                    mainText.text = "Állítsd párba a lapokat";
                    gc.pairThem();
                    break;
                case 3:
                    mainText.text = "Rendezd párba és sorrendbe a lapokat";
                    gc.putThemInOrder();
                    break;
                default:
                    Debug.Log("No such case as given");
                    break;
            }
        }
    }
}
