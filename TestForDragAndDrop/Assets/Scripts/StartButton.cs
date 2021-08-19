using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    [SerializeField] private GameController gc;
    [SerializeField] private Toggle Toggle1;
    [SerializeField] private Toggle Toggle2;
    [SerializeField] private Toggle Toggle3;
    [SerializeField] private Text mainText;
    [SerializeField] private Text instruction;

    public void OnMouseDown()
    {
        int caseSwitch = 2;

        if (SceneManager.GetActiveScene().buildIndex != 1 && (Toggle1.isOn || Toggle2.isOn || Toggle3.isOn))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if(SceneManager.GetActiveScene().buildIndex == 1)
        {
            switch (caseSwitch)
            {
                case 1:
                    mainText.text = "Ki az új felszálló?";
                    gc.newArrival();
                    break;
                case 2:
                    mainText.text = "Állítsd párba a lapokat!";
                    gc.pairThem();
                    break;
                case 3:
                    mainText.text = "Rakd sorba a lapokat";
                    gc.putThemInOrder();
                    break;
                default:
                    Debug.Log("No such case as given");
                    break;
            }
        }
    }
}
