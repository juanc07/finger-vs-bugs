using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface ITPCommon{	
	
	void Init();
	void SignIn(Action<bool> onSignIn);
	void SignOut();
	bool IsLogIn();

	void RevealAchievement(string achievementID,Action<bool> onRevealAchievement);
	void UnlockAchievement(string achievementID,Action<bool> onUnlockAchievement);
	void IncrementAchievement(string achievementID, int val,Action<bool> onIncrementAchievement);
	void ShowAchievement();
	void ShowAchievement(Action<bool> onLoadAchievement);

	void PostScore(string leaderboardID, int val,Action<bool> onPostScore);
	void PostScore(string leaderboardID,string metaTag, int val,Action<bool> onPostScore);
	void ShowLeaderBoard();
	void ShowLeaderBoard(string leaderboardID);

	// load scores
}
