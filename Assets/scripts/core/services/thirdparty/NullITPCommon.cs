using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NullITPCommon : ITPCommon {
	private const string TAG = "[NullITPCommon]";

	private bool isLogIn;
	
	public void Init(){
		Debug.Log( " init NullITPCommon " );
	}

	public void SignIn(Action <bool> OnSignIn){		
		Debug.Log( " NullITPCommon SignIn " );
	}

	public void SignOut(){
		// sign out
		Debug.Log( "NullITPCommon SignOut " );
	}

	public bool IsLogIn(){
		return isLogIn;
	}

	public void RevealAchievement(string achievementID,Action<bool> onRevealAchievement){
		Debug.Log( "NullITPCommon RevealAchievement " );
	}

	public void UnlockAchievement(string achievementID,Action<bool> onUnlockAchievement){
		Debug.Log( "NullITPCommon UnlockAchievement " );
	}

	public void IncrementAchievement(string achievementID, int val,Action<bool> onIncrementAchievement){
		Debug.Log( "NullITPCommon IncrementAchievement " );
	}

	public void ShowAchievement(){
		Debug.Log( "NullITPCommon ShowAchievement " );
	}

	public void ShowAchievement(Action<bool> onLoadAchievement){
		
	}

	public void PostScore(string leaderboardID, int val,Action<bool> onPostScore){
		Debug.Log( "NullITPCommon PostScore " );
	}

	// metatag ex. FirstDaily
	public void PostScore(string leaderboardID,string metaTag, int val,Action<bool> onPostScore){
		Debug.Log( "NullITPCommon PostScore metaTag " + metaTag );
	}

	public void ShowLeaderBoard(){
		Debug.Log( "NullITPCommon ShowLeaderBoard " );
	}

	public void ShowLeaderBoard(string leaderboardID){
		Debug.Log( "NullITPCommon ShowLeaderBoard leaderboardID " + leaderboardID );
	}
}

