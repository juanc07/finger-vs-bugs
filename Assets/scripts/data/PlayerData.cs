using UnityEngine;
using System.Collections;
using System;

[Serializable]
public class PlayerData{

	public int hp;
	public int maxHp;
	public int score;
	public int bestScore;
	public int kidBestScore;

	public int combo;
	public int bestCombo;
	public int kidsBestCombo;

	public int gameCount;
	public int totalGameCount;
	public int bugTotalKill;


	public PlayerData(){
		
	}
}
