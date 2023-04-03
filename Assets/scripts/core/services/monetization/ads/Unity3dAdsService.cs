using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class Unity3dAdsService : IUnity3dAd {
	private const string TAG = "[Unity3dAdsService]";

	private Action RewardComplete;
	private Action RewardFailed;
	private Action RewardSkipped;


	public void ShowAd(){
		if (Advertisement.IsReady()){
			Advertisement.Show();
		}
	}

	public void ShowRewardedAd(){
		if (Advertisement.IsReady("rewardedVideo")){
			var options = new ShowOptions { resultCallback = HandleShowResult };
			Advertisement.Show("rewardedVideo", options);
		}
	}

	public bool IsAdReady(){
		return Advertisement.IsReady();
	}

	public bool IsRewardedAdReady(){
		return Advertisement.IsReady("rewardedVideo");
	}

	public void AddEventListener( Action RewardComplete, Action RewardFailed, Action RewardSkipped){
		this.RewardComplete = RewardComplete;
		this.RewardFailed = RewardFailed;
		this.RewardSkipped = RewardSkipped;
	}

	public void RemoveEventListener(){
		this.RewardComplete = null;
		this.RewardFailed = null;
		this.RewardSkipped = null;
	}

	private void HandleShowResult(ShowResult result)
	{
		switch (result)
		{
		case ShowResult.Finished:
			Debug.Log("The ad was successfully shown.");
			//
			// YOUR CODE TO REWARD THE GAMER
			// Give coins etc.
			if(null!=RewardComplete){
				RewardComplete();
			}
			break;
		case ShowResult.Skipped:
			Debug.Log("The ad was skipped before reaching the end.");
			if(null!=RewardFailed){
				RewardFailed();
			}
			break;
		case ShowResult.Failed:
			Debug.LogError("The ad failed to be shown.");
			if(null!=RewardSkipped){
				RewardSkipped();
			}
			break;
		}
	}
}
