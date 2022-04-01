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
    #endregion

    public void OnMouseDown()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1 && (Toggle1.isOn || Toggle2.isOn || Toggle3.isOn))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            if(Toggle1.isOn)
            {
                profileManager.subUsers[0].active = true;
                profileManager.setActivePlayerIndex(0);
            }
            else if (Toggle2.isOn)
            {
                profileManager.subUsers[1].active = true;
                profileManager.setActivePlayerIndex(1);
            }
            else
            {
                profileManager.subUsers[2].active = true;
                profileManager.setActivePlayerIndex(2);
            }
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1 && GameController.instance.firstRun == true && GameController.instance.textures1.Count > 0 )
        {
            GameController.instance.resetGame();
            GameController.instance.firstRun = false;
            gameObject.SetActive(false);
        }
    }
}
