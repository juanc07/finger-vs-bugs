using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public interface IGoogleAds{
	void Init(MonoBehaviour monoInstance);

	void RequestBanner(string adUnitId,AdmobBannerType bannerSize,AdmobAdsPosition adPosition,bool isTest,string deviceId);
	void AddBannerAdEventListener(
		Action HandleOnAdLoaded,
		Action HandleOnAdFailedToLoad,
		Action HandleOnAdOpened,
		Action HandleOnAdClosed,
		Action HandleOnAdLeavingApplication
	);
	bool IsBannerAdsLoaded();
	void ShowBanner();
	void HideBanner();
	void DestroyBanner();


	void RequestInterstitial(string adUnitId,bool isTest,string deviceId);
	void AddIntertitialAdEventListener(
		Action AdLoadedIntertitial,
		Action AdFailedToLoadIntertitial,
		Action AdOpenedIntertitial,
		Action AdClosedIntertitial,
		Action AdLeavingApplicationIntertitial
	);
	void ShowIntertitial();
	void DestroyIntertitial();

	void RequestRewardBasedVideo(string adUnitId,bool isTest,string deviceId);
	void AddRewardBasedVideoEventListener(
		Action HandleRewardBasedVideoLoaded,
		Action HandleRewardBasedVideoFailedToLoad,
		Action HandleRewardBasedVideoOpened,
		Action HandleRewardBasedVideoStarted,
		Action <object,Reward>HandleRewardBasedVideoRewarded,
		Action HandleRewardBasedVideoClosed,
		Action HandleRewardBasedVideoLeftApplication
	);
	void ShowRewardBasedVideoAds();
	bool checkIsRewardBasedVideoAdLoaded();

	void GetDeviceId(Action<string> onGetDeviceId);
}