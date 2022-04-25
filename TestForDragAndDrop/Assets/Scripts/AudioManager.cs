﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This class contains the sounds for the game
public class AudioManager : MonoBehaviour
{
    //List of the sounds
    public Sound[] sounds;
    public static AudioManager audioManager;


    //Finds the audio source for all sounds in the list
    void Awake()
    {
        if (audioManager == null)
        {
            DontDestroyOnLoad(gameObject);
            audioManager = this;
        }
        else if (audioManager != this)
        {
            Destroy(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    //Plays a sound by its name
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }


    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Stop();
    }

    //Starts the background music when the games starts
    void Start()
    {
        Play("BackGround");
    }
}
