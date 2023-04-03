using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(DataController))]
public class DataControllerEditor : Editor {

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		DataController dataController = (DataController)target;
		if(GUILayout.Button("Reset Save Data"))
		{
			dataController.ResetSaveData();
		}
	}
}
