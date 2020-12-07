using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Audio;

[System.Serializable]
public class sound
{
    public string Name;
    public AudioClip Clip;
    public float volume;
    public bool loop;
     public AudioSource source;
}
