using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class NullGoogleAdsService : IGoogleAds {
	private const string TAG = "[NullGoogleAdsService]";
	private MonoBehaviour monoInstance;

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

	public void Init(MonoBehaviour monoInstance){
		this.monoInstance = monoInstance;
	}

	public void RequestBanner(string adUnitId,AdmobBannerType bannerSize,AdmobAdsPosition adPosition,bool isTest,string deviceId){
		
	}

	public void AddBannerAdEventListener(
		Action HandleOnAdLoaded,
		Action HandleOnAdFailedToLoad,
		Action HandleOnAdOpened,
		Action HandleOnAdClosed,
		Action HandleOnAdLeavingApplication
	){
		
	}

	public bool IsBannerAdsLoaded(){
		return false;
	}

	public void ShowBanner(){		
		
	}

	public void HideBanner(){
		
	}

	public void DestroyBanner(){
		
	}

	public void RequestInterstitial(string adUnitId,bool isTest,string deviceId){
		
	}

	public void AddIntertitialAdEventListener(
		Action HandleOnAdLoaded,
		Action HandleOnAdFailedToLoad,
		Action HandleOnAdOpened,
		Action HandleOnAdClosed,
		Action HandleOnAdLeavingApplication
	){
		
	}

	public void ShowIntertitial(){
		
	}

	public void DestroyIntertitial(){
		
	}

	public void RequestRewardBasedVideo(string adUnitId,bool isTest,string deviceId){
		
	}

	public void AddRewardBasedVideoEventListener(
		Action HandleRewardBasedVideoLoaded,
		Action HandleRewardBasedVideoFailedToLoad,
		Action HandleRewardBasedVideoOpened,
		Action HandleRewardBasedVideoStarted,
		Action <object,Reward>HandleRewardBasedVideoRewarded,
		Action HandleRewardBasedVideoClosed,
		Action HandleRewardBasedVideoLeftApplication
	){


	}

	public void ShowRewardBasedVideoAds(){		
	}

	public bool checkIsRewardBasedVideoAdLoaded(){
		return false;
	}

	public void GetDeviceId(Action<string> onGetDeviceId){
		
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		
	}

	public void HandleOnAdLeavingApplication(object sender, EventArgs args)
	{
		
	}

	// intertitial events
	public void HandleOnAdLoadedIntertitial(object sender, EventArgs args)
	{
		
	}

	public void HandleOnAdFailedToLoadIntertitial(object sender, AdFailedToLoadEventArgs args)
	{
		
	}

	public void HandleOnAdOpenedIntertitial(object sender, EventArgs args)
	{
		
	}

	public void HandleOnAdClosedIntertitial(object sender, EventArgs args)
	{
		
	}

	public void HandleOnAdLeavingApplicationIntertitial(object sender, EventArgs args)
	{
		
	}
}
