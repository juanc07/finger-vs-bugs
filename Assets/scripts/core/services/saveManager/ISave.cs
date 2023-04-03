using UnityEngine;
using System.Collections;

public interface ISave{
	void SaveData(string saveDatakey, int val );
	void SaveData(string saveDatakey, float val );
	void SaveData(string saveDatakey, string val );
	int LoadIntSaveData(string saveDatakey);
	float LoadFloatSaveData(string saveDatakey);
	string LoadStringSaveData(string saveDatakey);
	void DeleteSaveData(string saveDatakey);
	void DeleteAll();
}
