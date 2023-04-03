using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsHelper:ICleanable
{
    private const string TAG = "[ AdsHelper ]: ";

    private IGoogleAds adsService;
    private IUnity3dAd unity3dAdService;
    public AnalyticsHelper analyticsHelper;
    private bool isAlreadyOfferRewardedAd;

    private GameDataManager gameDataManager;
    private UIEventManager uiEventManager;
    private GameEventManager gameEventManager;
    private GameState gameState;

    private MonoBehaviour monoInstance;


    public void Init(MonoBehaviour monoInstance, AnalyticsHelper analyticsHelper)
    {
        
        this.analyticsHelper = analyticsHelper;
        this.monoInstance = monoInstance;

        //google ads service
        adsService = new GoogleAdsService();
        adsService.Init(this.monoInstance);
        // provide ads service
        ServiceLocator.ProvideGoogleAdService(adsService);
        // google ads service

        // unity3d ad service
        unity3dAdService = new Unity3dAdsService();
        ServiceLocator.ProvideUnity3dAdService(unity3dAdService);
        // unity3d ad service

        AddListenerForIAds();

        #if UNITY_ANDROID
        // if android direct request
        RequestBannerAds();
        RequestInterStitialAds();
        //RequestRewardBasedVideoAds();
        #elif UNITY_IOS
			if(!GameConfig.IS_ADMOB_TEST_ADS){
				RequestBannerAds();
				RequestInterStitialAds();
				//RequestRewardBasedVideoAds();
			}else{
				// if ios need to get the ios device id 1st for test
				adsService.GetDeviceId(OnGetDeviceId);
			}
        #endif

        AddEventListener();
    }

    private void AddEventListener()
    {
        gameDataManager = GameDataManager.GetInstance();
        gameEventManager = GameEventManager.GetInstance();
        gameDataManager.OnGameStateChange += OnGameStateChange;

        uiEventManager = UIEventManager.GetInstance();
        uiEventManager.OnShowIntertitialAd += OnShowIntertitialAd;
    }

    private void RemoveEventListener()
    {        
        gameDataManager.OnGameStateChange -= OnGameStateChange;
        gameEventManager = null;

        uiEventManager.OnShowIntertitialAd -= OnShowIntertitialAd;
        uiEventManager = null;
    }

    public void Clean()
    {
        RemoveEventListener();
        unity3dAdService.RemoveEventListener();
    }

    public void RequestBannerAds()
    {		
        // if android direct request
        adsService.RequestBanner(GameConfig.ADMOB_BANNER_AD_UNIT_ID, AdmobBannerType.SmartBanner, AdmobAdsPosition.Top, GameConfig.IS_ADMOB_TEST_ADS, GameConfig.ADMOB_TEST_AD_UNIT_ID);
    }

    public void ShowBannerAds()
    {
        if (adsService != null)
        {			
            if (adsService.IsBannerAdsLoaded())
            {
                adsService.ShowBanner();
            }
        }
    }

    public void HideBannerAds()
    {
        if (adsService != null)
        {
            adsService.HideBanner();
        }
    }

    public void RequestInterStitialAds()
    {
        // request interstitial 
        adsService.RequestInterstitial(GameConfig.ADMOB_INTERTITIAL_AD_UNIT_ID, GameConfig.IS_ADMOB_TEST_ADS, GameConfig.ADMOB_TEST_AD_UNIT_ID);
    }

    public void ShowInterStitialAds()
    {
        if (adsService != null)
        {
            adsService.ShowIntertitial();
        }
    }

    public void HideInterStitialAds()
    {
		
    }

    /*public void RequestRewardBasedVideoAds(){
		// request rewardedBasedVideoAds 
		adsService.RequestRewardBasedVideo( GameConfig.ADMOB_REWARDED_INTERTITIAL_AD_UNIT_ID,GameConfig.IS_ADMOB_TEST_ADS,GameConfig.ADMOB_TEST_AD_UNIT_ID );
	}*/

    public void ShowRewardBasedVideoAds()
    {
        if (adsService != null)
        {
            adsService.ShowRewardBasedVideoAds();
        }
    }

    public bool IsRewardedBasedVideoLoaded()
    {
        if (adsService != null)
        {
            return adsService.checkIsRewardBasedVideoAdLoaded();
        }
        else
        {
            return false;
        }
    }

    public bool GetIsAlreadyOfferRewardedAd()
    {
        return isAlreadyOfferRewardedAd;
    }

    public void SetIsAlreadyOfferRewardedAd(bool val)
    {
        isAlreadyOfferRewardedAd = val;
    }

    private void AddListenerForIAds()
    {
        unity3dAdService.AddEventListener(
            OnRewardComplete,
            OnRewardSkipped,
            OnRewardFailed
        );

        adsService.AddBannerAdEventListener(
            OnBannerAdLoadComplete,
            OnBannerAdLoadFail,
            OnBannerAdOpen,
            OnBannerAdClose,
            OnBannerAdLeaveApplication);

        adsService.AddIntertitialAdEventListener(
            OnAdLoadedIntertitial,
            OnAdFailedToLoadIntertitial,
            OnAdOpenedIntertitial,
            OnAdClosedIntertitial,
            OnAdLeavingApplicationIntertitial
        );

        adsService.AddRewardBasedVideoEventListener(
            onHandleRewardBasedVideoLoaded,
            onHandleRewardBasedVideoFailedToLoad,
            onHandleRewardBasedVideoOpened,
            onHandleRewardBasedVideoStarted,
            onHandleRewardBasedVideoRewarded,
            onHandleRewardBasedVideoClosed,
            onHandleRewardBasedVideoLeftApplication
        );
    }

    public bool IsUnity3dRewardedAdReady()
    {
        if (unity3dAdService != null)
        {
            return unity3dAdService.IsRewardedAdReady();
        }
        else
        {
            return false;
        }
    }

    public void CheckIfNeedToShowIntertitial()
    {
        // check if will show interstitial ads
        if (gameDataManager.GetGameMode() == GameMode.HARD)
        {
            if (gameDataManager.GetGameCount() % GameConfig.GAME_COUNT_CLASSIC_INTERSTITIAL == 0)
            {
                ShowHideInterstitialAds(true);
            }
        }
        else if (gameDataManager.GetGameMode() == GameMode.EASY)
        {
            if (gameDataManager.GetGameCount() % GameConfig.GAME_COUNT_KIDS_INTERSTITIAL == 0)
            {
                ShowHideInterstitialAds(true);
            }
        }
    }

    public void ShowHideInterstitialAds(bool val)
    {
        
        if (val)
        {
            Debug.Log(TAG + " ShowHideInterstitialAds GAME COUNT: " + gameDataManager.GetGameCount());
            ShowInterStitialAds();
        }
        else
        {
            HideInterStitialAds();
        }

    }

    public void ShowHideRewardBasedVideoAdsAds(bool val)
    {
        if (val)
        {
            Debug.Log("ShowHideRewardBasedVideoAdsAds now!!!");
            Debug.Log(TAG + " ShowHideRewardBasedVideoAdsAds");
            ShowUnity3dRewardedAd();
            //RewardExtraLives(true);
        }
    }

    public void ShowUnity3dRewardedAd()
    {		
        unity3dAdService.ShowRewardedAd();
    }

    public void ShowHideBannerAds(bool val)
    {
        if (this.gameState == GameState.PLAY)
        {
            if (val)
            {
                ShowBannerAds();
            }
            else
            {
                HideBannerAds();
            }
        }
    }

    // Banner ads events handlers
    private void OnBannerAdLoadComplete()
    {        
        analyticsHelper.LogLoadBannerAds();

        Debug.Log(TAG + " OnBannerAdLoadComplete ");
        ShowHideBannerAds(true);
    }

    private void OnBannerAdLoadFail()
    {        
        analyticsHelper.LogLoadFailBannerAds();

        Debug.Log(TAG + " OnBannerAdLoadFail ");
    }

    private void OnBannerAdOpen()
    {        
        analyticsHelper.LogOpenBannerAds();
        Debug.Log(TAG + " OnBannerAdOpen ");
    }

    private void OnBannerAdClose()
    {        
        analyticsHelper.LogCloseBannerAds();
        Debug.Log(TAG + " OnBannerAdClose ");
    }

    private void OnBannerAdLeaveApplication()
    {        
        analyticsHelper.LogLeavingApplicationBannerAds();
        Debug.Log(TAG + " OnBannerAdLeaveApplication ");
    }
    // Banner ads events handlers

    // Interstitial events handlers
    public void OnAdLoadedIntertitial()
    {        
        analyticsHelper.LogLoadInterStitialAds();
        Debug.Log(TAG + " OnAdLoadedIntertitial ");
    }

    public void OnAdFailedToLoadIntertitial()
    {        
        analyticsHelper.LogLoadFailInterStitialAds();
        Debug.Log(TAG + " OnAdFailedToLoadIntertitial ");
    }

    public void OnAdOpenedIntertitial()
    {		
        analyticsHelper.LogOpenInterStitialAds();
        Debug.Log(TAG + " OnAdOpenedIntertitial ");
    }

    public void OnAdClosedIntertitial()
    {        
        analyticsHelper.LogCloseInterStitialAds();
        // request interstitial again after closing
        RequestInterStitialAds();
        Debug.Log(TAG + " OnAdClosedIntertitial ");
    }

    public void OnAdLeavingApplicationIntertitial()
    {        
        analyticsHelper.LogLeavingApplicationInterStitialAds();
        Debug.Log(TAG + " OnAdLeavingApplicationIntertitial ");
    }
    // Interstitial events handlers

    public void OnGetDeviceId(string deviceId)
    {
        if (GameConfig.IS_ADMOB_TEST_ADS)
        {
            #if UNITY_IOS
			// if ads test need this if not can remove
			adsService.RequestBanner(GameConfig.ADMOB_BANNER_AD_UNIT_ID, AdmobBannerType.SmartBanner, AdmobAdsPosition.Top,GameConfig.IS_ADMOB_TEST_ADS,deviceId);
			adsService.RequestInterstitial( GameConfig.ADMOB_INTERTITIAL_AD_UNIT_ID,GameConfig.IS_ADMOB_TEST_ADS,deviceId);
			//adsService.ShowRewardBasedVideoAds( GameConfig.ADMOB_REWARDED_INTERTITIAL_AD_UNIT_ID,GameConfig.IS_ADMOB_TEST_ADS,deviceId);
            #endif
        }
    }

    // RewardBasedVideoAds events handlers
    public void onHandleRewardBasedVideoLoaded()
    {
        Debug.Log(TAG + " onHandleRewardBasedVideoLoaded ");
    }

    public void onHandleRewardBasedVideoFailedToLoad()
    {
        Debug.Log(TAG + " onHandleRewardBasedVideoFailedToLoad ");
    }

    public void onHandleRewardBasedVideoOpened()
    {
        Debug.Log(TAG + " onHandleRewardBasedVideoOpened ");
    }

    public void onHandleRewardBasedVideoStarted()
    {
        Debug.Log(TAG + " onHandleRewardBasedVideoStarted ");
    }

    public void onHandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Debug.Log(TAG + " onHandleRewardBasedVideoRewarded ");
        string type = args.Type;
        double amount = args.Amount;

        Debug.Log(TAG + " HandleRewardBasedVideoRewarded event received. User rewarded with:"
            + amount.ToString() + " " + type);
        /*if(null!=RewardBasedVideoRewarded){
			RewardBasedVideoRewarded(sender,args);
		}*/

        gameEventManager.DispatchRewardExtraLives(true);
    }


    public void onHandleRewardBasedVideoClosed()
    {
        Debug.Log(TAG + " onHandleRewardBasedVideoClosed ");
    }

    public void onHandleRewardBasedVideoLeftApplication()
    {
        Debug.Log(TAG + " onHandleRewardBasedVideoLeftApplication ");
    }

    // RewardBasedVideoAds events handlers

    // unity3d ads event handlers
    public void OnRewardComplete()
    {
        Debug.Log(TAG + " OnRewardComplete ");
        gameEventManager.DispatchRewardExtraLives(true);
    }

    public void OnRewardSkipped()
    {
        Debug.Log(TAG + " OnRewardSkipped ");
        gameEventManager.DispatchRewardExtraLives(false);
    }

    public void OnRewardFailed()
    {
        Debug.Log(TAG + " OnRewardFailed ");
        gameEventManager.DispatchRewardExtraLives(false);
    }
    // unity3d ads event handlers

    private void OnGameStateChange(GameState gameState)
    {
        this.gameState = gameState;
    }

    private void OnShowIntertitialAd()
    {
        ShowHideInterstitialAds(true);
    }
}
