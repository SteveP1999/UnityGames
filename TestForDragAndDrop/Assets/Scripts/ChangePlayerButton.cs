using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangePlayerButton : MonoBehaviour
{
    [SerializeField] private StartButton start;

    private void OnMouseDown()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        start.gameOn = false;
    }
}
