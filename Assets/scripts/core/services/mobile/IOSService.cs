using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DeadMosquito.IosGoodies;

public class IOSService : IMobile,ICleanable {
	private const string TAG = "[IOSService]";

	private MonoBehaviour monoInstance;

	public void Init(MonoBehaviour monoInstance){
		this.monoInstance = monoInstance;


	}

	public void Clean(){
		
	}

	public void Share( string screenShotName, string folderName ){
		this.monoInstance.StartCoroutine(
			AUP.Utils.TakeScreenshotNoSave( (Texture2D image)=>{
				Debug.Log( TAG + "check screen shot image: " + image );
				if(image!=null){
					#if UNITY_IOS

					IGShare.Share( () =>{
						Debug.Log(TAG + "DONE sharing");
						},
						"Can you beat my score on Finger vs bugs #fingervsbugs",
						image
					);					

					#endif
				}
			} )
		);
	}

	public void ShareFacebook( string screenShotName, string folderName ){
		this.monoInstance.StartCoroutine(
			AUP.Utils.TakeScreenshotNoSave( (Texture2D image)=>{
				Debug.Log( TAG + "check screen shot image: " + image );
				if(image!=null){
					#if UNITY_IOS
					if (IGShare.IsFacebookSharingAvailable()){
						IGShare.PostToFacebook(
							() => Debug.Log(TAG + "Posted to Facebook Successfully"), 
							() => Debug.Log(TAG + "Posting to Facebook Cancelled"), "Can you beat my score on Finger vs bugs", image);
					}else{
						Debug.Log(TAG + "Native posting to Facebook is not available on this device");
					}
					#endif
				}
			} )
		);
	}


	/// <summary>
	/// Schedules the local notification.
	/// </summary>
	/// <param name="AfterHour">After hour. 1 equals after 1 hour</param>
	/// <param name="afterMinute">After minute. 1 equals after 1 minute</param>
	/// <param name="afterSecond">After second. 1 equals after 1 second</param>
	public void ScheduleLocalNotification(string title,string message,string tickerMessage, int AfterHour, int afterMinute, int afterSecond){
		
	}

	public void ScheduleLocalNotificationEveryDay(string title,string message,string tickerMessage,
		int hour, int min, int sec, int amOrPm
	){
		
	}

	public void ScheduleLocalNotificationShortTime(string title,string message,string tickerMessage,
		int day,int hour, int min, int sec
	){
		
	}

	public void SetLocalNotificationListener(Action<bool> LocalNotificationLoadComplete){
		
	}

	public void CancelLocalNotification(int requestCode){
		
	}

	public void CancelAllLocalNotification(){
		
	}
}

