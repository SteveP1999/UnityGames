using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    #region Variables
    [SerializeField] private Toggle Toggle1;
    [SerializeField] private Toggle Toggle2;
    [SerializeField] private Toggle Toggle3;
    [SerializeField] private ProfileManager profileManager;
    public bool gameOn = false;     //Visszaállítani ha kiléptünk a játékből a menübe !!!!!!!!!!!!!!!!!
    #endregion

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
        else if(SceneManager.GetActiveScene().buildIndex == 1 && GameController.instance.firstRun == true && GameController.instance.textures1.Count > 0 )
        {
            API api = new API();
            switch (api.data.chosenGameMode)
            {
                case 1:
                    GameController.instance.newArrival();
                    break;
                case 2:
                    GameController.instance.pairThem();
                    break;
                case 3:
                    GameController.instance.putThemInOrder();
                    break;
                default:
                    Debug.Log("No such case as given");
                    break;
            }
            GameController.instance.firstRun = false;
        }
    }
}
