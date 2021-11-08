using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This class contains the sounds for the game
public class AudioManager : MonoBehaviour
{
    //List of the sounds
    public Sound[] sounds;


    //Finds the audio source for all sounds in the list
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    
    //Plays a sound, name required
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }


    //Plays the backgroundmusic when the games starts
    void Start()
    {
        Play("BackGroundMusic");
    }
}
