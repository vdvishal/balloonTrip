using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
public class unlock : MonoBehaviour
{
    public int myNum;
    public bool unlocked;
    public bool selected;

    public GameObject buyButton;
    public GameObject selectButton;

    public Button buyButtonUI;
    public Button selectButtonUI;


    public TMP_Text price;

    public TMP_Text selectedText;

    public TMP_FontAsset font;

    private gameManager game;

 



    // Start is called before the first frame update
    void Start()
    {
        price.font = font;
        selectedText.font = font;
        game = FindObjectOfType<gameManager>();
        price.color = new Color32(224,254,0,255);
        price.fontSize = 36;

        selectedText.color = new Color32(224, 254, 0, 255);
        price.SetText("100");
        selectedText.SetText("Select");

        buyButtonUI.onClick.AddListener(() => unlockSkin(transform.GetSiblingIndex()));
        selectButtonUI.onClick.AddListener(() => select(transform.GetSiblingIndex()));

        myNum = transform.GetSiblingIndex();

        if (gameManager.instance.cosmeticsArr[myNum] == 1)
        {
            unlocked = true;
        }
        else
        {
            unlocked = false;
        }

        if (unlocked)
        {
            buyButton.SetActive(false);
            selectButton.SetActive(true);
        }
        else
        {
            buyButton.SetActive(true);
            selectButton.SetActive(false);
        }
 
    }
 
    // Update is called once per frame
    void Update()
    {
        if (gameManager.instance.cosmeticsArr[myNum] == 1)
        {
            unlocked = true;
        }

        if (gameManager.instance.cosmeticsSelectedNum == myNum)
        {
            selected = true;
        }else
        {
            selected = false;
            selectedText.SetText("Select");
            selectedText.fontSize = 36;

            selectedText.color = new Color32(224, 254, 0, 255);

        }

        if (unlocked)
        {
            buyButton.SetActive(false);
            selectButton.SetActive(true);
        }

        if (selected)
        { 
            selectButton.SetActive(true);
            selectedText.SetText("Selected");
            selectedText.fontSize = 36;

            selectedText.color = new Color32(0,255,0,255);
        }
 
    }
 
    public void unlockSkin(int num)
    {
        if(gameManager.instance.balloonsCoins > 100)
        {
            game.unlock(num);
            CloudOnce.CloudVariables.unlocked = CloudOnce.CloudVariables.unlocked + "," + num.ToString();
            CloudOnce.CloudVariables.BalloonCoins -= 100;
            CloudOnce.Cloud.Storage.Save();
        }
    }


    public void select(int num)
    {  
        game.select(num);
        gameManager.instance.select(num);
    }
}
