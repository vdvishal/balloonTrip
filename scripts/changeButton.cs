using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeButton : MonoBehaviour
{
    public Image soundSprite;

    public Sprite soundOnSprite;
    public Sprite soundOffSprite;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.soundOn)
        {
            soundSprite.sprite = soundOnSprite;
        }else
        {
            soundSprite.sprite = soundOffSprite;
        }
    }
}
