using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;
using UnityEngine.SceneManagement;

public class purchaseManager : MonoBehaviour
{

    public TMP_Text balloonCoins;
    public Button rmAds;
    public Button rmAdsbundle;
    public Button balloon;


    public void purchase(int num)
    {
        switch (num)
        {
            case 1:
                iapManager.instance.purchase(1);
                break;
            case 2:
                iapManager.instance.purchase(2);
                break;
            case 3:
                iapManager.instance.purchase(3);
                break;
            case 4:
                iapManager.instance.purchase(4);
                break;
            default:
                break;
        }
    }

    void Start()
    {
 /*       if (Application.platform != RuntimePlatform.IPhonePlayer ||
            Application.platform != RuntimePlatform.OSXPlayer)
        {
            gameObject.SetActive(false);
        }
        */
        balloonCoins.SetText(gameManager.instance.balloonsCoins.ToString());

        if (gameManager.instance.adsDeactivated)
        {
            rmAds.interactable = false;
             
        }

        if(gameManager.instance.IAP == 3 || gameManager.instance.IAP == 1)
        {
            rmAds.interactable = false;
            rmAdsbundle.interactable = false;
        }

        if (gameManager.instance.IAP == 2)
        {
            balloon.interactable = false;
            rmAdsbundle.interactable = false;
        }

        if (gameManager.instance.IAP == 3 )
        {
            rmAds.interactable = false;
            balloon.interactable = false;
            rmAdsbundle.interactable = false;
        }


    }

    public void ClickRestorePurchaseButton()
    {
        iapManager.instance.RestorePurchases();
    }

     public void viewAd()
    {
        Adsmanager.instance.UserChoseToWatchAd20balloons();
    }

    public void back()
    {
        SceneManager.LoadScene(1);
    }

    private void Update()
    {
        balloonCoins.SetText(gameManager.instance.balloonsCoins.ToString());
    }
}
