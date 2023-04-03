using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class GoogleAdsService : IGoogleAds {
	private const string TAG = "[GoogleAds]";
	private MonoBehaviour monoInstance;

	private BannerView bannerView;
	private bool isBannerAdLoaded;

	private InterstitialAd interstitial;

	private RewardBasedVideoAd rewardBasedVideo;

	//banner events
	private Action onHandleOnAdLoaded;
	private Action onHandleOnAdFailedToLoad;
	private Action onHandleOnAdOpened;
	private Action onHandleOnAdClosed;
	private Action onHandleOnAdLeavingApplication;

	//intertitial events
	private Action onAdLoadedIntertitial;
	private Action onAdFailedToLoadIntertitial;
	private Action onAdOpenedIntertitial;
	private Action onAdClosedIntertitial;
	private Action onAdLeavingApplicationIntertitial;

	//reward based video ads
	private Action onHandleRewardBasedVideoLoaded;
	private Action onHandleRewardBasedVideoFailedToLoad;
	private Action onHandleRewardBasedVideoOpened;
	private Action onHandleRewardBasedVideoStarted;
	private Action <object,Reward>onHandleRewardBasedVideoRewarded;
	private Action onHandleRewardBasedVideoClosed;
	private Action onHandleRewardBasedVideoLeftApplication;

	private bool rewardBasedEventHandlersSet = false;

	//reward based video ads

	public void Init(MonoBehaviour monoInstance){
		this.monoInstance = monoInstance;
	}

	public void RequestBanner(string adUnitId,AdmobBannerType currentAdSize,AdmobAdsPosition currentAdPosition,
		bool isTest,string deviceId
	){
		isBannerAdLoaded = false;

		AdSize bannerSize = AdSize.SmartBanner;
		AdPosition adPosition = AdPosition.Top;

		if(currentAdSize== AdmobBannerType.Banner ){
			bannerSize = AdSize.Banner;
		}else if(currentAdSize== AdmobBannerType.IABBanner ){
			bannerSize = AdSize.IABBanner;
		}else if(currentAdSize== AdmobBannerType.Leaderboard ){
			bannerSize = AdSize.Leaderboard;
		}else if(currentAdSize== AdmobBannerType.MediumRectangle ){
			bannerSize = AdSize.MediumRectangle;
		}else if(currentAdSize== AdmobBannerType.SmartBanner ){
			bannerSize = AdSize.SmartBanner;
		}else{
			bannerSize = AdSize.SmartBanner;
		}

		if(currentAdPosition == AdmobAdsPosition.Bottom){
			adPosition = AdPosition.Bottom;
		}else if(currentAdPosition == AdmobAdsPosition.BottomLeft){
			adPosition = AdPosition.BottomLeft;
		}else if(currentAdPosition == AdmobAdsPosition.BottomRight){
			adPosition = AdPosition.BottomRight;
		}else if(currentAdPosition == AdmobAdsPosition.Center){
			adPosition = AdPosition.Center;
		}else if(currentAdPosition == AdmobAdsPosition.Top){
			adPosition = AdPosition.Top;
		}else if(currentAdPosition == AdmobAdsPosition.TopLeft){
			adPosition = AdPosition.TopLeft;
		}else if(currentAdPosition == AdmobAdsPosition.TopRight){
			adPosition = AdPosition.TopRight;
		}else {
			adPosition = AdPosition.Bottom;
		}

		// Create a 320x50 banner at the top of the screen.
		bannerView = new BannerView(adUnitId, bannerSize, adPosition);

		// Called when an ad request has successfully loaded.
		bannerView.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is clicked.
		bannerView.OnAdOpening += HandleOnAdOpened;
		// Called when the user returned from the app after an ad click.
		bannerView.OnAdClosed += HandleOnAdClosed;
		// Called when the ad click caused the user to leave the application.
		bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;


		AdRequest request;

		if(isTest){
			request = new AdRequest.Builder()				
				.AddTestDevice(deviceId)  // My test device.
				.Build();			
		}else{
			// Create an empty ad request.
			request = new AdRequest.Builder()
				//.SetGender(Gender.Male)
				//.SetBirthday(new DateTime(1985, 1, 1))
				//.AddExtra("excl_cat", "cars,sports") // Category exclusions for DFP.
				//.TagForChildDirectedTreatment(true) // for child filter
				.Build();
		}

		// Load the banner with the request.
		bannerView.LoadAd(request);	
	}

	public void AddBannerAdEventListener(
		Action HandleOnAdLoaded,
		Action HandleOnAdFailedToLoad,
		Action HandleOnAdOpened,
		Action HandleOnAdClosed,
		Action HandleOnAdLeavingApplication
	){
		this.onHandleOnAdLoaded = HandleOnAdLoaded;
		this.onHandleOnAdFailedToLoad = HandleOnAdFailedToLoad;
		this.onHandleOnAdOpened = HandleOnAdOpened;
		this.onHandleOnAdClosed = HandleOnAdClosed;
		this.onHandleOnAdLeavingApplication = HandleOnAdLeavingApplication;
	}

	public bool IsBannerAdsLoaded(){
		return isBannerAdLoaded;
	}

	public void ShowBanner(){		
		bannerView.Show();
	}

	public void HideBanner(){
		bannerView.Hide();
	}

	public void DestroyBanner(){
		bannerView.Destroy();
	}

	public void RequestInterstitial(string adUnitId,bool isTest,string deviceId){
		// Initialize an InterstitialAd.
		interstitial = new InterstitialAd(adUnitId);


		// Called when an ad request has successfully loaded.
		interstitial.OnAdLoaded += HandleOnAdLoadedIntertitial;
		// Called when an ad request failed to load.
		interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoadIntertitial;
		// Called when an ad is clicked.
		interstitial.OnAdOpening += HandleOnAdOpenedIntertitial;
		// Called when the user returned from the app after an ad click.
		interstitial.OnAdClosed += HandleOnAdClosedIntertitial;
		// Called when the ad click caused the user to leave the application.
		interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplicationIntertitial;

		// Create an empty ad request.
		AdRequest request;

		if(isTest){			
			request = new AdRequest.Builder()				
				.AddTestDevice(deviceId)  // My test device.
				.Build();
		}else{
			request = new AdRequest.Builder().Build();
		}

		// Load the interstitial with the request.
		interstitial.LoadAd(request);
	}

	public void AddIntertitialAdEventListener(
		Action AdLoadedIntertitial,
		Action AdFailedToLoadIntertitial,
		Action AdOpenedIntertitial,
		Action AdClosedIntertitial,
		Action AdLeavingApplicationIntertitial
	){
		this.onAdLoadedIntertitial = AdLoadedIntertitial;
		this.onAdFailedToLoadIntertitial = AdFailedToLoadIntertitial;
		this.onAdOpenedIntertitial = AdOpenedIntertitial;
		this.onAdClosedIntertitial = AdClosedIntertitial;
		this.onAdLeavingApplicationIntertitial = AdLeavingApplicationIntertitial;
	}

	public void ShowIntertitial(){
		if (interstitial.IsLoaded()) {
			interstitial.Show();
		}
	}

	public void DestroyIntertitial(){
		interstitial.Destroy();
	}

	public void RequestRewardBasedVideo(string adUnitId,bool isTest,string deviceId){
		rewardBasedVideo = RewardBasedVideoAd.Instance;

		if (!rewardBasedEventHandlersSet)
		{
			// Ad event fired when the rewarded video ad
			// has been received.
			rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
			// has failed to load.
			rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
			// is opened.
			rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
			// has started playing.
			rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
			// has rewarded the user.
			rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
			// is closed.
			rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
			// is leaving the application.
			rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

			rewardBasedEventHandlersSet = true;
		}


		// Create an empty ad request.
		AdRequest request;

		if(isTest){			
			request = new AdRequest.Builder()				
				.AddTestDevice(deviceId)  // My test device.
				.Build();
		}else{
			request = new AdRequest.Builder().Build();
		}

		// Load the reward based video request.
		rewardBasedVideo.LoadAd(request, adUnitId);
	}

	public void AddRewardBasedVideoEventListener(
		Action onHandleRewardBasedVideoLoaded,
		Action onHandleRewardBasedVideoFailedToLoad,
		Action onHandleRewardBasedVideoOpened,
		Action onHandleRewardBasedVideoStarted,
		Action <object,Reward>onHandleRewardBasedVideoRewarded,
		Action onHandleRewardBasedVideoClosed,
		Action onHandleRewardBasedVideoLeftApplication
	){
		this.onHandleRewardBasedVideoLoaded = onHandleRewardBasedVideoLoaded;
		this.onHandleRewardBasedVideoFailedToLoad = onHandleRewardBasedVideoFailedToLoad;
		this.onHandleRewardBasedVideoOpened = onHandleRewardBasedVideoOpened;
		this.onHandleRewardBasedVideoStarted = onHandleRewardBasedVideoStarted;
		this.onHandleRewardBasedVideoRewarded = onHandleRewardBasedVideoRewarded;
		this.onHandleRewardBasedVideoClosed = onHandleRewardBasedVideoClosed;
		this.onHandleRewardBasedVideoLeftApplication = onHandleRewardBasedVideoLeftApplication;
	}

	public void ShowRewardBasedVideoAds(){
		if(rewardBasedVideo!=null){
			if (rewardBasedVideo.IsLoaded()){
				rewardBasedVideo.Show();
			}	
		}
	}

	public bool checkIsRewardBasedVideoAdLoaded(){
		if (rewardBasedVideo.IsLoaded()){			
			return true;
		}else{
			return false;
		}
	}

	public void GetDeviceId(Action<string> onGetDeviceId){
		#if UNITY_IOS
		Application.RequestAdvertisingIdentifierAsync (
			(string advertisingId, bool trackingEnabled, string error) =>
			{ 
				Debug.Log ("advertisingId " + advertisingId + " " + trackingEnabled + " " + error); 
				if(trackingEnabled){
					if(null!=onGetDeviceId){
						onGetDeviceId(Md5Sum(advertisingId));
					}	
				}else{
					if(null!=onGetDeviceId){
						onGetDeviceId(error);
					}
				}
			}
		);
		#endif
	}

	private string Md5Sum(string strToEncrypt) {
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		string hashString = ""; 
		for (int i = 0; i < hashBytes.Length; i++) {
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}

	// banner ads event handlers
	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		Debug.Log("OnAdLoaded event received.");
		// Handle the ad loaded event.

		if(null!=onHandleOnAdLoaded){
			onHandleOnAdLoaded();
		}

		isBannerAdLoaded = true;
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		Debug.Log(TAG + " OnHandleOnAdFailedToLoad Failed to load: " + args.Message);
		// Handle the ad failed to load event.

		if(null!=onHandleOnAdFailedToLoad){
			onHandleOnAdFailedToLoad();
		}
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		Debug.Log(TAG + " OnHandleOnAdOpened event received.");

		if(null!=onHandleOnAdOpened){
			onHandleOnAdOpened();
		}
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		Debug.Log(TAG + " OnHandleOnAdClosed event received.");

		if(null!=onHandleOnAdClosed){
			onHandleOnAdClosed();
		}
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		Debug.Log(TAG + " OnHandleOnAdLeavingApplication event received.");

		if(null!=onHandleOnAdLeavingApplication){
			onHandleOnAdLeavingApplication();
		}
	}
	// banner ads event handlers

	// Interstitial ads event handlers
	public void HandleOnAdLoadedIntertitial(object sender, EventArgs args)
	{
		Debug.Log(TAG + " HandleOnAdLoadedIntertitial event received.");
		// Handle the ad loaded event.

		if(null!=onAdLoadedIntertitial){
			onAdLoadedIntertitial();
		}
	}

	public void HandleOnAdFailedToLoadIntertitial(object sender, AdFailedToLoadEventArgs args)
	{
		Debug.Log(TAG + " HandleOnAdFailedToLoadIntertitial Failed to load: " + args.Message);
		// Handle the ad failed to load event.

		if(null!=onAdFailedToLoadIntertitial){
			onAdFailedToLoadIntertitial();
		}
	}

	public void HandleOnAdOpenedIntertitial(object sender, EventArgs args)
	{
		Debug.Log(TAG + " HandleOnAdOpenedIntertitial event received.");

		if(null!=onAdOpenedIntertitial){
			onAdOpenedIntertitial();
		}
	}

	public void HandleOnAdClosedIntertitial(object sender, EventArgs args)
	{
		Debug.Log(TAG + " HandleOnAdClosedIntertitial event received.");

		if(null!=onAdClosedIntertitial){
			onAdClosedIntertitial();
		}
	}

	public void HandleOnAdLeavingApplicationIntertitial(object sender, EventArgs args)
	{
		Debug.Log(TAG + " HandleOnAdLeavingApplicationIntertitial event received.");

		if(null!=onAdLeavingApplicationIntertitial){
			onAdLeavingApplicationIntertitial();
		}
	}
	// Interstitial ads event handlers

	// reward based video ads event handlers
	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args){
		Debug.Log(TAG + " HandleRewardBasedVideoLoaded event received.");
		if( null != onHandleRewardBasedVideoLoaded){
			onHandleRewardBasedVideoLoaded();
		}
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args){
		Debug.Log(TAG + " HandleRewardBasedVideoFailedToLoad event received.");
		if( null != onHandleRewardBasedVideoFailedToLoad){
			onHandleRewardBasedVideoFailedToLoad();
		}
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args){
		Debug.Log(TAG + " HandleRewardBasedVideoOpened event received.");
		if( null != onHandleRewardBasedVideoOpened){
			onHandleRewardBasedVideoOpened();
		}
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args){
		Debug.Log(TAG + " HandleRewardBasedVideoStarted event received.");
		if( null != onHandleRewardBasedVideoStarted){
			onHandleRewardBasedVideoStarted();
		}
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args){		
		string type = args.Type;
		double amount = args.Amount;

		Debug.Log(TAG + " HandleRewardBasedVideoRewarded event received. User rewarded with:" 
			+ amount.ToString() + " " + type);

		if( null != onHandleRewardBasedVideoRewarded){
			onHandleRewardBasedVideoRewarded(sender,args);
		}
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args){
		Debug.Log(TAG + " HandleRewardBasedVideoClosed event received.");
		if( null != onHandleRewardBasedVideoClosed){
			onHandleRewardBasedVideoClosed();
		}
	}

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args){
		Debug.Log(TAG + " HandleRewardBasedVideoLeftApplication event received.");
		if( null != onHandleRewardBasedVideoLeftApplication){
			onHandleRewardBasedVideoLeftApplication();
		}
	}

	// reward based video ads event handlers
}
