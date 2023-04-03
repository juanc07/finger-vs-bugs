using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AndroidService : IMobile,ICleanable {
	private const string TAG = "[AndroidService]";
	
	private MonoBehaviour monoInstance;

	private SharePlugin sharePlugin;
	private UtilsPlugin utilsPlugin;

	private LocalNotificationPlugin localNotificationPlugin;
	private TimePlugin timePlugin;
	//store request code of all local notification
	//tip save this on playerpref so that you can still access it when your player quit and the open your application
	private List<int> notificationRequestCodeCollection = new List<int>();

	//this must be unique for every alarm that you will create, 
	//that's why we added request code to numbers of request code you have 
	//created
	//you will need this to remove or cancel them
	private int REQUEST_CODE = 808;

	private Action<bool> LocalNotificationLoadComplete;


	public void Init(MonoBehaviour monoInstance){		
		this.monoInstance = monoInstance;

		LocalNotificationPlugin.OnLocalNotificationLoadComplete+=OnLocalNotificationLoadComplete;
		LocalNotificationPlugin.OnLocalNotificationLoadFail+=OnLocalNotificationLoadFail;

		localNotificationPlugin = LocalNotificationPlugin.GetInstance();
		localNotificationPlugin.SetDebug(0);
		localNotificationPlugin.Init();

		timePlugin = TimePlugin.GetInstance();
		timePlugin.SetDebug(0);

		utilsPlugin = UtilsPlugin.GetInstance();
		utilsPlugin.SetDebug(0);

		sharePlugin = SharePlugin.GetInstance();
		sharePlugin.SetDebug(0);

		localNotificationPlugin.LoadNotification();
	}

	public void Clean(){
		LocalNotificationPlugin.OnLocalNotificationLoadComplete-=OnLocalNotificationLoadComplete;
		LocalNotificationPlugin.OnLocalNotificationLoadFail-=OnLocalNotificationLoadFail;
	}

	public void Share( string screenShotName, string folderName ){
		//string screenShotName = "FingerVSBugsShare.jpg";
		string folderPath = utilsPlugin.CreateFolder(folderName,0);
		string path ="";

		int hour = timePlugin.GetIntHour();
		// plus 1 minute meaning it will trigger after 1 minute
		int minute = timePlugin.GetIntMinute();

		int sec = timePlugin.GetIntSec();

		//1 = pm && 0 = am
		int amOrPM = timePlugin.GetIntAmOrPm();
		string stateOfDay;

		if(amOrPM == 1){
			stateOfDay = "PM";
		}else{
			stateOfDay = "AM";
		}

		//string currScreenShotName = screenShotName + "_" +String.Format("H:{0} M:{1} S:{2} - {3}",hour,minute,sec,stateOfDay);

		if(!folderPath.Equals("",StringComparison.Ordinal)){
			path = folderPath + "/" + screenShotName;

			this.monoInstance.StartCoroutine(AUP.Utils.TakeScreenshot(path,screenShotName));
			sharePlugin.ShareImage("subject","subjectContent",path);
		}	
	}

	/// <summary>
	/// Schedules the local notification.
	/// </summary>
	/// <param name="AfterHour">After hour. 1 equals after 1 hour</param>
	/// <param name="afterMinute">After minute. 1 equals after 1 minute</param>
	/// <param name="afterSecond">After second. 1 equals after 1 second</param>
	public void ScheduleLocalNotification(string title,string message,string tickerMessage, int AfterHour, int afterMinute, int afterSecond){
		//request code is the unique id of local notification
		int requestCode = REQUEST_CODE +  notificationRequestCodeCollection.Count;

		// 0 to 11 , 0 = 12
		int hour = timePlugin.GetIntHour() + AfterHour;
		if(hour>=12){
			hour-=12;
		}
		// plus 1 minute meaning it will trigger after 1 minute
		int minute = timePlugin.GetIntMinute() + afterMinute;

		int sec = timePlugin.GetIntSec() + afterSecond;

		//1 = pm && 0 = am
		int amOrPM = timePlugin.GetIntAmOrPm();
		//string stateOfDay;

		/*if(amOrPM == 1){
			stateOfDay = "PM";
		}else{
			stateOfDay = "AM";
		}*/

		//string scheduleValue = String.Format("H:{0} M:{1} S:{2} - {3}",hour,minute,sec,stateOfDay);

		localNotificationPlugin.ScheduleSpecificNotification(title,requestCode,hour,minute,sec,amOrPM, message,tickerMessage,true,true);

		//save request code for future usage ex. canceling notification or removing it
		notificationRequestCodeCollection.Add(requestCode);
		//Debug.Log("added scheduled Specific notification with requestCode " + requestCode );
	}

	public void ScheduleLocalNotificationEveryDay(string title,string message,string tickerMessage,
		int hour, int min, int sec, int amOrPm
	){
		//request code is the unique id of local notification
		int requestCode = REQUEST_CODE +  notificationRequestCodeCollection.Count;

		// 0 to 11 , 0 = 12
		int currHour = timePlugin.GetIntHour();

		// extra check because sometimes
		int checkAmOrPM = timePlugin.GetIntAmOrPm();
		if(checkAmOrPM == 1 && currHour >= 2){
			//pm
			amOrPm = 0;
		}else{
			//am
			amOrPm = 1;
		}

		//localNotificationPlugin.ScE (title,requestCode,hour,minute,sec,amOrPM, message,tickerMessage,true,true);
		localNotificationPlugin.ScheduleEveryDay(title,requestCode,hour,min,sec,amOrPm,message,tickerMessage,true,true);

		//save request code for future usage ex. canceling notification or removing it
		notificationRequestCodeCollection.Add(requestCode);
		//Debug.Log("added scheduled Specific notification with requestCode " + requestCode );
	}

	public void ScheduleLocalNotificationShortTime(string title,string message,string tickerMessage,
		int day,int hour, int min, int sec
	){
		//request code is the unique id of local notification
		int requestCode = REQUEST_CODE +  notificationRequestCodeCollection.Count;
		//1 = pm && 0 = am
		//int amOrPM = timePlugin.GetIntAmOrPm();
		//string scheduleValue = String.Format("H:{0} M:{1} S:{2} - {3}",hour,minute,sec,stateOfDay);

		//localNotificationPlugin.ScE (title,requestCode,hour,minute,sec,amOrPM, message,tickerMessage,true,true);
		localNotificationPlugin.ScheduleShortTime(title,requestCode,day,hour,min,sec,message,tickerMessage,true,true);

		//save request code for future usage ex. canceling notification or removing it
		notificationRequestCodeCollection.Add(requestCode);
		//Debug.Log("added scheduled Specific notification with requestCode " + requestCode );
	}

	public void CancelLocalNotification(int requestCode){
		int len = notificationRequestCodeCollection.Count;
		for(int index=0;index<len;index++){
			int currentRequestCode = notificationRequestCodeCollection[index];
			if(currentRequestCode == requestCode){				
				localNotificationPlugin.CancelScheduledNotification(requestCode);
				notificationRequestCodeCollection.RemoveAt(index);
				break;
			}
		}
	}

	public void CancelAllLocalNotification(){
		localNotificationPlugin.ClearAllScheduledNotification();
		notificationRequestCodeCollection.Clear();
	}

	public void SetLocalNotificationListener(Action<bool> LocalNotificationLoadComplete){
		this.LocalNotificationLoadComplete = LocalNotificationLoadComplete;
	}

	private void OnLocalNotificationLoadComplete(string notifications){
		if(!notifications.Equals("",StringComparison.Ordinal)){
			//remove brackets
			notifications =  notifications.Replace( "[","" ).Replace("]","");
			Debug.Log(TAG + "remove bracket OnLocalNotificationLoadComplete notifcations: " +  notifications);

			//get split the request codes
			string[] loadedRequestCode = notifications.Split(',');

			//convert them to int and place them on notification request collections
			foreach(string reqCode in loadedRequestCode){
				notificationRequestCodeCollection.Add(  int.Parse(reqCode));
			}
		}else{
			Debug.Log(TAG + "empty no save request code...");
		}

		CancelAllLocalNotification();

		if(null!=LocalNotificationLoadComplete){
			LocalNotificationLoadComplete(true);
		}
	}

	private void OnLocalNotificationLoadFail(){
		if(null!=LocalNotificationLoadComplete){
			LocalNotificationLoadComplete(false);
		}
		Debug.Log(TAG + " OnLocalNotificationLoadFail");
	}
}
