using UnityEngine;
using System.Collections;

public class NullSaveDataService: ISave{
	public void SaveData(string saveDatakey, int val ){
		Debug.LogWarning("you are using null save data service");
	}

	public void SaveData(string saveDatakey, float val ){
		Debug.LogWarning("you are using null save data service");
	}

	public void SaveData(string saveDatakey, string val ){
		Debug.LogWarning("you are using null save data service");
	}

	public int LoadIntSaveData(string saveDatakey){
		Debug.LogWarning("you are using null save data service");
		return 0;
	}

	public float LoadFloatSaveData(string saveDatakey){
		Debug.LogWarning("you are using null save data service");
		return 0f;
	}

	public string LoadStringSaveData(string saveDatakey){
		Debug.LogWarning("you are using null save data service");
		return "";
	}

	public void DeleteSaveData(string saveDatakey){
		Debug.LogWarning("you are using null save data service");
	}

	public void DeleteAll(){
		Debug.LogWarning("you are using null save data service");
	}
}
