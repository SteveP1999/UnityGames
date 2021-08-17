using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    private float currentTime = 0f;
    private float startingTime = 10f;

    [SerializeField]
    public Text timerText;

    void Start()
    {
        currentTime = startingTime;
    }

    void Update()
    {
        currentTime -= 1 * Time.deltaTime;   // Ne minden framben csinálja, hanem másodpercenként
        timerText.text = "Hátralevő idő: 00:0" + currentTime.ToString("0");
        if(currentTime <= 0)
        {
            currentTime = 0;
        }
    }
}
