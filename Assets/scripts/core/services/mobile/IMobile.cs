using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IMobile {
	void Init(MonoBehaviour monoInstance);
	void Share(string screenShotName, string folderName);
	void ScheduleLocalNotification(string title,string message,string tickerMessage, int AfterHour, int afterMinute, int afterSecond);
	void ScheduleLocalNotificationEveryDay(string title,string message,string tickerMessage,
		int hour, int min, int sec, int amOrPm
	);
	void ScheduleLocalNotificationShortTime(string title,string message,string tickerMessage,
		int day,int hour, int min, int sec
	);
	void SetLocalNotificationListener(Action<bool> LocalNotificationLoadComplete);
	void CancelAllLocalNotification();
	void CancelLocalNotification(int requestCode);
}
