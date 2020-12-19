using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class iapManager : MonoBehaviour, IStoreListener
{
   
    private string removeAds = "remove_ads";
    private string removeAdsBundle = "remove_ads_bundle";
    private string allBalloonBundle = "all_balloon_bundle";
    private string balloon_100 = "balloon_1000";
 
 



    public static iapManager instance;

    private static IStoreController m_StoreController;
    private static IExtensionProvider m_StoreExtensionProvider;

    //Step 1 create your products



    //************************** Adjust these methods **************************************
    public void InitializePurchasing()
    {
        if (IsInitialized()) { return; }
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        //Step 2 choose if your product is a consumable or non consumable
        builder.AddProduct(removeAds, ProductType.NonConsumable);
        builder.AddProduct(removeAdsBundle, ProductType.NonConsumable);
        builder.AddProduct(allBalloonBundle, ProductType.NonConsumable);
        builder.AddProduct(balloon_100, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }


    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }


    //Step 3 Create methods
    public void purchase(int type)
    {
        switch (type)
        {
            case 1:
                BuyProductID(removeAds);
                break;
            case 2:
                BuyProductID(removeAdsBundle);
                break;
            case 3:
                BuyProductID(allBalloonBundle);
                break;
            case 4:
                BuyProductID(balloon_100);
                break;
            default:
                break;
        }
    }



    //Step 4 modify purchasing
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        if (String.Equals(args.purchasedProduct.definition.id, "remove_ads", StringComparison.Ordinal))
        {
            gameManager.instance.adsDeactivated = true;
            CloudOnce.CloudVariables.IAP += 1;
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "all_balloon_bundle", StringComparison.Ordinal))
        {
             
            CloudOnce.CloudVariables.IAP += 2;

            String unlock = "-1";
            for (int i = 0; i < gameManager.instance.cosmeticsArr.Length; i++)
            {
                unlock += "," + i.ToString();
            }

            CloudOnce.CloudVariables.unlocked = unlock;
            gameManager.instance.unlockedString = unlock;

            gameManager.instance.updateUnlocks();


        }
        else if (String.Equals(args.purchasedProduct.definition.id, "remove_ads_bundle", StringComparison.Ordinal))
        {

            CloudOnce.CloudVariables.IAP += 3;
            gameManager.instance.adsDeactivated = true;

            String unlock = "-1";
            for (int i = 0; i < gameManager.instance.cosmeticsArr.Length; i++)
            {
                unlock += "," + i.ToString();
            }

            CloudOnce.CloudVariables.unlocked = unlock;

            gameManager.instance.unlockedString = unlock;

            gameManager.instance.updateUnlocks();
        }
        else if (String.Equals(args.purchasedProduct.definition.id, "balloon_1000", StringComparison.Ordinal))
        {
            gameManager.instance.balloonsCoins += 1000;
            CloudOnce.CloudVariables.BalloonCoins += 1000;
            CloudOnce.Cloud.Storage.Save();
        }
        else
        {
            Debug.Log("Purchase Failed");
        }
        CloudOnce.Cloud.Storage.Save();
        return PurchaseProcessingResult.Complete;
    }










    //**************************** Dont worry about these methods ***********************************
    private void Awake()
    {
        TestSingleton();
    }

    void Start()
    {
        if (m_StoreController == null) { InitializePurchasing(); }
    }

    private void TestSingleton()
    {
        if (instance != null) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            Product product = m_StoreController.products.WithID(productId);
            if (product != null && product.availableToPurchase)
            {
                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            Debug.Log("BuyProductID FAIL. Not initialized.");
        }
    }

    public void RestorePurchases()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer ||
            Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions>();
            apple.RestoreTransactions((result) => {
                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
            });
        }
        else
        {
            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        Debug.Log("OnInitialized: PASS");
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
    }


    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }



}
