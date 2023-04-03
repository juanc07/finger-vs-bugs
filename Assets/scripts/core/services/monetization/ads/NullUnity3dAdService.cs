using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NullUnity3dAdService : IUnity3dAd {
	private const string TAG = "[NullUnity3dAdService]";

	public void ShowAd(){
		Debug.Log(TAG + " ShowAd.");
	}

	public void ShowRewardedAd(){
		Debug.Log(TAG + " ShowRewardedAd.");
	}

	public bool IsAdReady(){
		Debug.Log(TAG + " IsAdReady.");
		return false;
	}

	public bool IsRewardedAdReady(){
		Debug.Log(TAG + " IsRewardedAdReady.");
		return false;
	}

	public void AddEventListener( Action RewardComplete, Action RewardFailed, Action RewardSkipped){

	}

	public void RemoveEventListener( ){
	}
}
