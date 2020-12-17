using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class audioManager : MonoBehaviour
{
    public static audioManager instance = null;

    private AudioSource[] audios;
    public sound[] sounds;
    sound soundPlay;
    private bool clickS;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
         
        DontDestroyOnLoad(gameObject);

        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;

          }

     // soundPlay = Array.Find(sounds, sound => sound.Name == "loop");
    }

 

    private void Start()
    {
        /* 
        if (gameManager.instance.soundOn)
        {
            Play("loop");
        } 
        */
        
        Debug.Log(audios.Length);
    }

    
    public void soundOff()
    {
        audios = FindObjectsOfType<AudioSource>();
        Debug.Log(audios.Length);

        gameManager.instance.soundOn = !gameManager.instance.soundOn;
        if (gameManager.instance.soundOn)
        {
    
            foreach (AudioSource s in audios)
            {
                
              s.mute = false;

            }

        }
        else
        {
            foreach (AudioSource s in audios)
            {

                s.mute = true;

            }
        }
    }

    private void Update()
    {
        if (clickS)
        {
            Play("Click");
        }
    }

    public void toggleClick()
    {
        clickS = !clickS;
    }


    public void Play(string name)
    {
        sound s = Array.Find(sounds, sound => sound.Name == name);
 
        s.source.Play();
    }
}
