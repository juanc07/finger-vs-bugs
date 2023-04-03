using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using System;

public class GooglePlayService : ITPCommon {
	private const string TAG = "[GooglePlayService]";

	private bool isLogIn;

	public void Init(){
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
			// enables saving game progress.
			//.EnableSavedGames()
			// registers a callback to handle game invitations received while the game is not running.
			//.WithInvitationDelegate(<callback method>)
			// registers a callback for turn based match notifications received while the
			// game is not running.
			//.WithMatchDelegate(<callback method>)
			// require access to a player's Google+ social graph (usually not needed)
			//.RequireGooglePlus()
			.Build();

		PlayGamesPlatform.InitializeInstance(config);
		// recommended for debugging:
		PlayGamesPlatform.DebugLogEnabled = true;
		// Activate the Google Play Games platform
		//PlayGamesPlatform.Activate();

		Debug.Log( " google play services initialized....... " );
	}

	public void SignIn(Action <bool> OnSignIn){
		if(!isLogIn){
			// authenticate user:
			PlayGamesPlatform.Instance.localUser.Authenticate((bool success) => {
				// handle success or failure
				isLogIn = success;
				if(success){				
					Debug.Log( " google play services sign in success " );
				}else{
					Debug.Log( " google play services sign in failed " );
				}

				if(null!=OnSignIn){
					OnSignIn(success);
				}
			});	
		}

	}

	public void SignOut(){
		if(isLogIn){
			PlayGamesPlatform.Instance.SignOut();
		}
	}

	public bool IsLogIn(){
		return isLogIn;
	}

	public void RevealAchievement(string achievementID,Action<bool> onRevealAchievement){
		// unlock achievement (achievement ID "Cfjewijawiu_QA")
		PlayGamesPlatform.Instance.ReportProgress(achievementID, 0.0f, (bool success) => {
			// handle success or failure
			if(success){
				Debug.Log( " google play services RevealAchievement in success " );
			}else{
				Debug.Log( " google play services RevealAchievement in failed " );
			}
			// inside will call whenever server is complete

			if(null!=onRevealAchievement){
				onRevealAchievement(success);
			}
		});
	}

	public void UnlockAchievement(string achievementID,Action<bool> onUnlockAchievement){		

		// unlock achievement (achievement ID "Cfjewijawiu_QA")
		PlayGamesPlatform.Instance.ReportProgress(achievementID, 100.0f, (bool success) => {
			// handle success or failure
			if(success){
				Debug.Log( " google play services UnlockAchievement in success " );
			}else{
				Debug.Log( " google play services UnlockAchievement in failed " );
			}

			if(null!=onUnlockAchievement){
				onUnlockAchievement(success);
			}
		});
	}

	public void IncrementAchievement(string achievementID, int val,Action<bool> onIncrementAchievement){		

		// increment achievement (achievement ID "Cfjewijawiu_QA") by 5 steps
		PlayGamesPlatform.Instance.IncrementAchievement(
			achievementID, val, (bool success) => {
				// handle success or failure
				if(success){
					Debug.Log( " google play services IncrementAchievement in success " );
				}else{
					Debug.Log( " google play services IncrementAchievement in failed " );
				}

				if(null!=onIncrementAchievement){
					onIncrementAchievement(success);
				}
		});
	}

	public void ShowAchievement(){		
		// show achievements UI
		Debug.Log( "google play services Show Achievement");
		PlayGamesPlatform.Instance.ShowAchievementsUI();
	}

	public void ShowAchievement(Action<bool> onLoadAchievement){
		
	}

	public void PostScore(string leaderboardID, int val,Action<bool> onPostScore){
		Debug.Log( "google play services try PostScore id " + leaderboardID + " score " + val);
		// post score 12345 to leaderboard ID "Cfji293fjsie_QA")
		PlayGamesPlatform.Instance.ReportScore(val, leaderboardID, (bool success) => {
			// handle success or failure
			if(success){
				Debug.Log( " google play services PostScore in success " );
			}else{
				Debug.Log( " google play services PostScore in failed " );
			}
			if(null!=onPostScore){
				onPostScore(success);
			}
		});
	}

	// metatag ex. FirstDaily
	public void PostScore(string leaderboardID,string metaTag, int val,Action<bool> onPostScore){		
		Debug.Log( "google play services try PostScore id " + leaderboardID + " metaTag " + metaTag + " score " + val);
		// post score 12345 to leaderboard ID "Cfji293fjsie_QA" and tag "FirstDaily")
		PlayGamesPlatform.Instance.ReportScore(val, leaderboardID, metaTag, (bool success) => {
			// handle success or failure
			if(success){
				Debug.Log( " google play services PostScore with id in success " );
			}else{
				Debug.Log( " google play services PostScore with in failed " );
			}

			if(null!=onPostScore){
				onPostScore(success);
			}
		});
	}

	public void ShowLeaderBoard(){

		if(!isLogIn){
			return ;	
		}

		// show leaderboard UI
		Debug.Log( "google play services ShowLeaderBoard");
		PlayGamesPlatform.Instance.ShowLeaderboardUI();
	}

	public void ShowLeaderBoard(string leaderboardID){
		if(!isLogIn){
			return ;	
		}

		// show leaderboard UI
		Debug.Log( "google play services ShowLeaderBoard id " + leaderboardID );
		PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderboardID);
	}
}
