using UnityEngine;
using System.Collections;
using UnityEditor;

#pragma warning disable 0219

[CustomEditor(typeof(SoundConfig))]
public class SoundConfigEditor : Editor {

	static private bool showSFX;
	static private bool showBGM;

	public override void OnInspectorGUI() {
		
		showSFX = EditorGUILayout.Foldout(showSFX, "Sound Effects");
		
		if(showSFX) {
			EditorGUI.indentLevel++;

			SoundConfig.SfxDictionary sfxDictionary = ((SoundConfig)target).sfxDictionary;

			string[] sfxNames = System.Enum.GetNames(typeof(SFX));
			SFX[] sfxTypes = (SFX[])System.Enum.GetValues(typeof(SFX));
			int sfxCount = sfxNames.Length;
			for(int sfxIndex = 0; sfxIndex < sfxCount; sfxIndex++) {
				EditorGUILayout.BeginHorizontal();
				
				EditorGUILayout.PrefixLabel(sfxNames[sfxIndex]);
				SFX key = sfxTypes[sfxIndex];
				sfxDictionary.Set(key, (AudioClip)EditorGUILayout.ObjectField(sfxDictionary.Get(key), typeof(AudioClip), false));
				
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.BeginHorizontal();
			
			Color original = GUI.color;
			
			GUI.color = Color.red;
			if(GUILayout.Button("Reset Listing")) {
				sfxDictionary.Clear();
			}
			
			GUI.color = Color.green;
			if (GUILayout.Button("Save Changes")) {
				EditorUtility.SetDirty(target as SoundConfig);
				AssetDatabase.SaveAssets();
			}
			
			GUI.color = original;
			
			EditorGUILayout.EndHorizontal();
			EditorGUI.indentLevel--;
		}


		showBGM = EditorGUILayout.Foldout(showBGM, "Background Music");

		if(showBGM) {
			EditorGUI.indentLevel++;
			
			SoundConfig.BGMDictionary bgmDictionary = ((SoundConfig)target).bgmDictionary;
			
			string[] bgmNames = System.Enum.GetNames(typeof(BGM));
			BGM[] bgmTypes = (BGM[])System.Enum.GetValues(typeof(BGM));
			int bgmCount = bgmNames.Length;
			for(int bgmIndex = 0; bgmIndex < bgmCount; bgmIndex++) {
				EditorGUILayout.BeginHorizontal();
				
				EditorGUILayout.PrefixLabel(bgmNames[bgmIndex]);
				BGM key = bgmTypes[bgmIndex];
				bgmDictionary.Set(key, (AudioClip)EditorGUILayout.ObjectField(bgmDictionary.Get(key), typeof(AudioClip), false));
				
				EditorGUILayout.EndHorizontal();
			}
			
			EditorGUILayout.BeginHorizontal();
			
			Color original = GUI.color;
			
			GUI.color = Color.red;
			if(GUILayout.Button("Reset Listing")) {
				bgmDictionary.Clear();
			}
			
			GUI.color = Color.green;
			if (GUILayout.Button("Save Changes")) {
				EditorUtility.SetDirty(target as SoundConfig);
				AssetDatabase.SaveAssets();
			}
			
			EditorGUILayout.EndHorizontal();
			
			EditorGUI.indentLevel--;
		}
	}
}
