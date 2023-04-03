using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IUnity3dAd{
	void ShowAd();
	void ShowRewardedAd();
	bool IsAdReady();
	bool IsRewardedAdReady();
	void AddEventListener( Action RewardComplete, Action RewardFailed, Action RewardSkip);
	void RemoveEventListener();
}
