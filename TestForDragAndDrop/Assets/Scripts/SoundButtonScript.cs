using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButtonScript : MonoBehaviour
{
    private int soundOn = 0;   //0: megy a sound 1: megy az effekt hang 2: minden megy
    public Sprite[] sprites;
    public SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
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
            spriteRenderer.sprite = sprites[2];
            soundOn = 1;
        }
        else if(soundOn == 1)
        {
            FindObjectOfType<AudioManager>().Play("BackGroundMusic");
            soundOn = 2;
            spriteRenderer.sprite = sprites[0];
        }
        else
        {
            soundOn = 0;
            spriteRenderer.sprite = sprites[1];
        }
    }
}
