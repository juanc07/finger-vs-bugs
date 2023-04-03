using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Analytics;

public class FirebaseAnalyticsService : IAnalytics {

	private const string TAG ="[FirebaseAnalyticsService]: ";
	private AnalyticsHelper helper;

	public void Init(){
		FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
		Debug.Log( TAG + "  init..." );
	}

	public void SetAnalyticsHelper(AnalyticsHelper helper){
		this.helper = helper;
	}

	public AnalyticsHelper GetAnalyticsHelper(){
		return this.helper;
	}

	public void LogEvent( string eventName ){
		FirebaseAnalytics.LogEvent(eventName);
	}

	public void LogEvent( string eventName,string parameterName, string value ){
		FirebaseAnalytics.LogEvent(eventName,parameterName,value);
	}

	public void LogEvent( string eventName,string parameterName, int value ){
		FirebaseAnalytics.LogEvent(eventName,parameterName,value);
	}

	public void LogEvent( string eventName,string parameterName, double value ){
		FirebaseAnalytics.LogEvent(eventName,parameterName,value);
	}

	public void LogEvent( string eventName,string parameterName, long value ){
		FirebaseAnalytics.LogEvent(eventName,parameterName,value);
	}

	public void LogEvent( string eventName,Dictionary< string, object > parameters ){
		List<Parameter> parameterCollection = new List<Parameter>();

		foreach(KeyValuePair<string, object> obj in parameters){
			if (obj.Value.Equals(typeof(string))){
				parameterCollection.Add(new Parameter(obj.Key.ToString(),obj.Value.ToString()));	
			}else if (obj.Value.Equals(typeof(int))){
				parameterCollection.Add(new Parameter(obj.Key.ToString(),(int)obj.Value));	
			}else if (obj.Value.Equals(typeof(double))){
				parameterCollection.Add(new Parameter(obj.Key.ToString(),(double)obj.Value));	
			}else if (obj.Value.Equals(typeof(long))){
				parameterCollection.Add(new Parameter(obj.Key.ToString(),(long)obj.Value));	
			}
		}

		FirebaseAnalytics.LogEvent(eventName,parameterCollection.ToArray());
	}

	public void SetUserProperty(string propertyName, string value){
		FirebaseAnalytics.SetUserProperty(
			propertyName,
			value
		);
	}

	public void SetUserId(string value){
		FirebaseAnalytics.SetUserId(value);
	}
}
