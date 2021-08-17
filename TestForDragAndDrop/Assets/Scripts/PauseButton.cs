using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : MonoBehaviour
{
    public Sprite[] sprites;
    private bool isPlaying = true;
    public SpriteRenderer spriteRenderer;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown()
    {
        if(isPlaying)
        {
            isPlaying = false;
            spriteRenderer.sprite = sprites[0];
            Time.timeScale = 0f;
        }
        else
        {
            isPlaying = true;
            spriteRenderer.sprite = sprites[1];
            Time.timeScale = 1f;
        }
    }
}
