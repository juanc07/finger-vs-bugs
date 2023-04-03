using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NullAnalyticsService : IAnalytics {

	private const string TAG ="[NullAnalyticsService]: ";

	public void Init(){		
		Debug.Log( TAG + "  init..." );
	}

	public void SetAnalyticsHelper(AnalyticsHelper helper){
		Debug.Log( TAG + "warning using null SetAnalyticsHelper..." );
	}

	public AnalyticsHelper GetAnalyticsHelper(){
		Debug.Log( TAG + "warning using null GetAnalyticsHelper..." );
		return null;
	}


	public void LogEvent( string eventName ){
		Debug.Log( TAG + "warning using null LogEvent..." );
	}

	public void LogEvent( string eventName,string parameterName, string value ){
		Debug.Log( TAG + "warning using null LogEvent..." );
	}

	public void LogEvent( string eventName,string parameterName, int value ){
		Debug.Log( TAG + "warning using null LogEvent..." );
	}

	public void LogEvent( string eventName,string parameterName, double value ){
		Debug.Log( TAG + "warning using null LogEvent..." );
	}

	public void LogEvent( string eventName,string parameterName, long value ){
		Debug.Log( TAG + "warning using null LogEvent..." );
	}

	public void LogEvent( string eventName,Dictionary< string, object > parameters ){
		Debug.Log( TAG + "warning using null LogEvent..." );

	}

	public void SetUserProperty(string propertyName, string value){
		Debug.Log( TAG + "warning using null SetUserProperty..." );
	}

	public void SetUserId(string value){
		Debug.Log( TAG + "warning using null SetUserId..." );
	}
}
