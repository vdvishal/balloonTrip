using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Adsmanager : MonoBehaviour
{
    public static Adsmanager instance = null;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;


    private void Awake()
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
    }

    public void Start()
    {
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        RequestInterstitial();
        RewardAdFor20balloons();
    }

    private void RequestInterstitial()
    {
        #if UNITY_ANDROID
            string adUnitId = "ca-app-pub-9832492835389318/2379083304";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
            string adUnitId = "unexpected_platform";
#endif





        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);

        this.interstitial.OnAdClosed += HandleOnAdClosed;
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
    }

    public void GameOver()
    {
        // remove it before goinf to production
        gameManager.instance.deathCount = 0;
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        gameManager.instance.deathCount = 0;
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
         RequestInterstitial();
        gameManager.instance.deathCount = 0;

    }


    public void RewardAdFor20balloons()
    {
#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-9832492835389318/7719626628";
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
            string adUnitId = "unexpected_platform";
#endif



        this.rewardedAd = new RewardedAd(adUnitId);


        this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;

        this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;


        this.rewardedAd.OnAdClosed += HandleRewardedAdClosed;


        AdRequest request = new AdRequest.Builder().Build();

        this.rewardedAd.LoadAd(request);

    }

    public void UserChoseToWatchAd20balloons()
    {
         if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        RewardAdFor20balloons();
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        UserChoseToWatchAd20balloons();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        RewardAdFor20balloons();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        gameManager.instance.balloonsCoins += 20;
        CloudOnce.CloudVariables.BalloonCoins += 20;
        CloudOnce.Cloud.Storage.Save();
        RewardAdFor20balloons();
    }

}
