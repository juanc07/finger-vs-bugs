using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NullMobileService : IMobile,ICleanable {
	private const string TAG = "[NullMobileService]";

	private MonoBehaviour monoInstance;

	public void Init(MonoBehaviour monoInstance){
		this.monoInstance = monoInstance;
	}

	public void Clean(){
		Debug.Log( " Clean" + TAG);
	}

	public void Share( string screenShotName, string folderName ){
		Debug.Log( " Share" + TAG);
	}

	/// <summary>
	/// Schedules the local notification.
	/// </summary>
	/// <param name="AfterHour">After hour. 1 equals after 1 hour</param>
	/// <param name="afterMinute">After minute. 1 equals after 1 minute</param>
	/// <param name="afterSecond">After second. 1 equals after 1 second</param>
	public void ScheduleLocalNotification(string title,string message,string tickerMessage, int AfterHour, int afterMinute, int afterSecond){
		Debug.Log( " ScheduleLocalNotification" + TAG);
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
		Debug.Log( " CancelLocalNotification" + TAG);
	}

	public void CancelAllLocalNotification(){
		Debug.Log( " CancelAllLocalNotification" + TAG);
	}
}

