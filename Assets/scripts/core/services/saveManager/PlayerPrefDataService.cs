using UnityEngine;
using System.Collections;

public class PlayerPrefDataService: ISave{
	public void SaveData(string saveDatakey, int val ){
		PlayerPrefs.SetInt(saveDatakey,val);
	}

	public void SaveData(string saveDatakey, float val ){
		PlayerPrefs.SetFloat(saveDatakey,val);
	}

	public void SaveData(string saveDatakey, string val ){
		PlayerPrefs.SetString(saveDatakey,val);
	}

	public int LoadIntSaveData(string saveDatakey){
		return PlayerPrefs.GetInt(saveDatakey,0);
	}

	public float LoadFloatSaveData(string saveDatakey){
		return PlayerPrefs.GetFloat(saveDatakey,0f);
	}

	public string LoadStringSaveData(string saveDatakey){
		return PlayerPrefs.GetString(saveDatakey,"");
	}

	public void DeleteSaveData(string saveDatakey){
		PlayerPrefs.DeleteKey(saveDatakey);
	}

	public void DeleteAll(){
		PlayerPrefs.DeleteAll();
	}
}
