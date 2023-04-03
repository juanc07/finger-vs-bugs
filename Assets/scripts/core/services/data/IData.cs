using UnityEngine;
using System.Collections;

public interface IData{
	int GetScore();
	void SetScore(int val);
	void UpdateScore(int val);

	int GetBestScore();
	void SetBestScore(int val);

	int GetKidsBestScore();
	void SetKidsBestScore(int val);

	bool GetIsNewBestScore();
	void SetIsNewBestScore(bool val);

	int GetHP();
	void SetHP(int val);
	void UpdateHP(int val);

	int GetCombo();
	void SetCombo(int val);
	void UpdateCombo(int val);

	int GetMaxHP();
	void SetMaxHP(int val);

	void SetGameMode(GameMode gameMode);
	GameMode GetGameMode();

	bool GetVibration();
	void SetVibration(bool val);

	int GetGameCount();
	void SetGameCount(int val);
	void UpdateGameCount(int val);

	int GetTotalGameCount();
	void SetTotalGameCount(int val);
	void UpdateTotalGameCount(int val);

	int GetHasRate();
	void SetHasRate(int val);
	int GetDontShowRate();
	void SetDontShowRate(int val);
	int GetJustShowRateUs();
	void SetJustShowRateUs(int val);

	int GetBestCombo();
	void SetBestCombo(int val);

	int GetKidsBestCombo();
	void SetKidsBestCombo(int val);


	int GetBugKill();
	void SetBugKill(int val);
	void UpdateBugKill(int val);

	int GetTotalBugKill();
	void SetTotalBugKill(int val);
	void UpdateTotalBugKill(int val);

	int GetAntQueenKill();
	void SetAntQueenKill(int val);
	void UpdateAntQueenKill(int val);

	int GetAntWarriorKill();
	void SetAntWarriorKill(int val);
	void UpdateAntWarriorKill(int val);

	int GetAntWorkerKill();
	void SetAntWorkerKill(int val);
	void UpdateAntWorkerKill(int val);

	int GetSpiderKill();
	void SetSpiderKill(int val);
	void UpdateSpiderKill(int val);

	int GetSmallSpiderKill();
	void SetSmallSpiderKill(int val);
	void UpdateSmallSpiderKill(int val);

	int GetCockroachKill();
	void SetCockroachKill(int val);
	void UpdateCockroachKill(int val);

	int GetDuration();
	void SetDuration(int val);
}
