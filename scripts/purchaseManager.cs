using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class purchaseManager : MonoBehaviour
{

    public TMP_Text balloonCoins;

 
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
        if (Application.platform != RuntimePlatform.IPhonePlayer ||
            Application.platform != RuntimePlatform.OSXPlayer)
        {
            gameObject.SetActive(false);
        }

        balloonCoins.SetText(gameManager.instance.balloonsCoins.ToString());
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
}
