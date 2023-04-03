using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SocialPlatforms;

public class GameCenterService : ITPCommon {
	private const string TAG = "[GameCenterService]";
	private bool isLogIn;

	public void Init(){
		Debug.Log( " init GameCenterService " );
	}

	public void SignIn(Action <bool> OnSignIn){
		Debug.Log( TAG + " SignIn " );
		Social.localUser.Authenticate ((bool success) => {
			// handle success or failure
			isLogIn = success;
			if(success){				
				Debug.Log( TAG + " sign in success " );
			}else{
				Debug.Log( TAG +  " sign in failed " );
			}

			if(null!=OnSignIn){
				OnSignIn(success);
			}
		});
	}

	public void SignOut(){
		// sign out
		Debug.Log(TAG  + "SignOut " );
	}

	public bool IsLogIn(){
		return isLogIn;
	}

	public void RevealAchievement(string achievementID,Action<bool> onRevealAchievement){
		// You can also call into the functions like this
		Social.ReportProgress (achievementID, 0.0f, success => {
			if (success){
				Debug.Log (TAG + "Successfully RevealAchievement");
			}else{
				Debug.Log (TAG +  "Failed to RevealAchievement");
			}

			if(null!=onRevealAchievement){
				onRevealAchievement(success);
			}
		});
	}

	public void UnlockAchievement(string achievementID,Action<bool> onUnlockAchievement){
		// You can also call into the functions like this
		Social.ReportProgress (achievementID, 100.0f, success => {
			if (success){
				Debug.Log (TAG + "Successfully UnlockAchievement");
			}else{
				Debug.Log (TAG +  "Failed to UnlockAchievement");
			}

			if(null!=onUnlockAchievement){
				onUnlockAchievement(success);
			}
		});
	}

	public void IncrementAchievement(string achievementID, int val,Action<bool> onIncrementAchievement){
		// You can also call into the functions like this
		Social.ReportProgress (achievementID, val, success => {
			if (success){
				Debug.Log (TAG + "Successfully IncrementAchievement");
			}else{
				Debug.Log (TAG +  "Failed to IncrementAchievement");
			}

			if(null!=onIncrementAchievement){
				onIncrementAchievement(success);
			}
		});
	}

	public void ShowAchievement(){
		/*Social.LoadAchievements( (IAchievement[] achievements  )=>{
			if (achievements.Length == 0){			
				Debug.Log ( TAG + "Error: no achievements found");
			}else{
				Debug.Log (TAG + "Got " + achievements.Length + " achievements");
			}
		} );*/

		Social.ShowAchievementsUI();
	}

	public void ShowAchievement(Action<bool> onLoadAchievement){
		Social.LoadAchievements( (IAchievement[] achievements  )=>{
			if (achievements.Length == 0){			
				Debug.Log ( TAG + "Error: no achievements found");
				if(null!=onLoadAchievement){
					onLoadAchievement(false);
				}
			}else{
				if(null!=onLoadAchievement){
					onLoadAchievement(true);
				}
				Debug.Log (TAG + "Got " + achievements.Length + " achievements");
			}
		} );
	}

	public void PostScore(string leaderboardID, int val,Action<bool> onPostScore){
		Social.ReportScore( val,leaderboardID,(bool success)=>{
			if(success){
				Debug.Log( TAG + " PostScore success " );
			}else{
				Debug.Log( TAG + " PostScore failed " );
			}

			if(null!=onPostScore){
				onPostScore(success);
			}
		} );
	}

	// metatag ex. FirstDaily
	public void PostScore(string leaderboardID,string metaTag, int val,Action<bool> onPostScore){
		Social.ReportScore( val,leaderboardID,(bool success)=>{
			if(success){
				Debug.Log( TAG + " PostScore success " );	
			}else{
				Debug.Log( TAG + " PostScore failed " );
			}

			if(null!=onPostScore){
				onPostScore(success);
			}
		} );
	}

	public void ShowLeaderBoard(){
		Social.ShowLeaderboardUI();
		Debug.Log( TAG + " ShowLeaderBoard ");
	}

	public void ShowLeaderBoard(string leaderboardID){
		Social.ShowLeaderboardUI();
		Debug.Log( TAG + " ShowLeaderBoard leaderboardID" + leaderboardID );
	}
}


