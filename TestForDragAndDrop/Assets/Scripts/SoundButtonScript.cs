using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButtonScript : MonoBehaviour
{
    private int soundOn = 0;   //0: megy a sound 1: megy az effekt hang 2: minden megy
    public Sprite[] sprites;
    public Image soundButton;

    public void OnMouseDown()
    {
        if (soundOn == 0)
        {
            AudioManager am = FindObjectOfType<AudioManager>();
            foreach (Sound s in am.sounds)
            {
                if (s.name == "BackGroundMusic")
                {
                    s.source.Stop();
                }
            }
            soundButton.sprite = sprites[2];
            soundOn = 1;
        }
        else if(soundOn == 1)
        {
            FindObjectOfType<AudioManager>().Play("BackGroundMusic");
            soundOn = 2;
            soundButton.sprite = sprites[0];
        }
        else
        {
            soundOn = 0;
            soundButton.sprite = sprites[1];
        }
    }
}
