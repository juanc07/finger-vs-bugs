using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using Tutela;

public class TutelaService : IDataShare {
	private const string TAG = "[TutelaService]";
	
	public void Init(string apiKey, Action<bool> onComplete){

		/*TutelaSDK.Initialized += (sender, e) => 
		{			
			Debug.Log(TAG + "Tutela SDK successfully initialized.");
			if(null!=onComplete){
				onComplete(true);
			}
		};

		TutelaSDK.FailedToInitialize += (sender, e) => 
		{
			Debug.Log(TAG + "Tutela SDK not successfully initialized. Error: " + e.ErrorMessage);
			if(null!=onComplete){
				onComplete(false);
			}
		};

		TutelaSDK.InitializeWithAPIKey(apiKey);*/
	}
}
