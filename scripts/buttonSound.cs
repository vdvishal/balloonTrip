using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonSound : MonoBehaviour
{
    public AudioSource ClickSource;
    public AudioClip ClickSound;

    // Start is called before the first frame update
    void Start()
    {

     }
    
    public void Click()
    {
        ClickSource.PlayOneShot(ClickSound);
    }
}
