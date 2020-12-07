using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class audioManager : MonoBehaviour
{
    public sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.Clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
        }
    }

    private void Start()
    {
        Play("loop");
    }

    public void Play(string name)
    {
        sound s = Array.Find(sounds, sound => sound.Name == name);
        s.source.Play();
    }
}
