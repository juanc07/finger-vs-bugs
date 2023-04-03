using UnityEditor;
using UnityEngine;
using System.IO;
using System.Text;
using System;
using UnityEditor.Callbacks;

public class SoundManagerEditor:EditorWindow{
	
	private  Array sfx;
	private  string[] sfxNames;
	private  int sfxCount;
	
	private  Array bgm;
	private  string[] bgmNames;
	private  int bgmCount;

	private  bool startDelayCreateConfig = false;

	//progress bar
	public float secs = 1f;
	public float startVal = 0f;
	public float progress = 0f;
	
	[MenuItem("Tools/Sound Manager /Setup...", false, 1)]
	public static void MenuItemSetup() {
		EditorWindow.GetWindow(typeof(SoundManagerEditor));
	}
	
	private void CreateFolder(string path, string folderName){
		string resourcesPath= "Assets/Resources";
		if (!System.IO.Directory.Exists(resourcesPath)){
			AssetDatabase.CreateFolder("Assets", "Resources");
		}
		
		if (!System.IO.Directory.Exists(path + "/" + folderName)){
			AssetDatabase.CreateFolder(path, folderName);
		}else{
			//EditorUtility.DisplayDialog("Failed: ", folderName + " folder already exist!","ok");
		}
	}
	
	private void OnGUI(){
		// Title
		GUILayout.BeginArea(new Rect(20, 20, position.width - 40, position.height));
		GUILayout.Label("Sound Manager Setup", EditorStyles.boldLabel);
		GUILayout.Label("NOTES:");
		GUILayout.Label("1. Please rename your audio files \navoid numbers at the begining and avoid spaces \nto prevent parsing error!");
		GUILayout.Label("2. Disable 3D Sound settings on your audio files");
		GUILayout.Space(10);

		//secs = EditorGUILayout.FloatField("Time to wait:", secs);

		// Setup button
		if (GUILayout.Button("Setup")){

			if (secs < 1) {
				Debug.LogError("Seconds should be bigger than 1");
				return;
			}

			startVal = (float)EditorApplication.timeSinceStartup;


			CreateFolder("Assets/Resources","SFX");
			CreateFolder("Assets/Resources","BGM");

			CreateAudioList("Assets/Resources/SFX","SFX","SFX");
			CreateAudioList("Assets/Resources/BGM","BGM","BGM");
			startDelayCreateConfig = true;
			AssetDatabase.Refresh();

		}
		
		GUILayout.EndArea();

		//Debug.Log("Current detected event: " + Event.current);

		if(startDelayCreateConfig){
			if (progress < secs)
				EditorUtility.DisplayProgressBar("Please Wait", "setting up...", progress / secs);
			//else
			//EditorUtility.ClearProgressBar();
		}else{
			EditorUtility.ClearProgressBar();
		}

		progress = (float)(EditorApplication.timeSinceStartup - startVal);
	}

	void OnInspectorUpdate() {
		Repaint();
	}
	
	private void CreateAudioList( string audioFolderPath, string audioListname, string audioFolderName){

		string[] directoryList = Directory.GetDirectories("Assets","gfSoundManager",SearchOption.AllDirectories);
		string finalPath="";

		foreach (string dir in directoryList){
			if(!dir.Equals("",StringComparison.Ordinal)){
				finalPath = dir;
				Debug.Log ("dir " + dir);
				break;
			}
		}

		if (finalPath.Equals("",StringComparison.Ordinal)){
			EditorUtility.DisplayDialog("Failed: ", "can't find sound manager folder","ok");
			return;
		}

		string copyPath = finalPath + "/" + audioListname + ".cs";
		string fileName = audioListname;
		object[] loadedAudio = Resources.LoadAll(audioFolderName);
		int len = loadedAudio.Length;
		int count =0;

		if (!System.IO.Directory.Exists(audioFolderPath)){
			EditorUtility.DisplayDialog("Failed: ", "can't create " + audioListname  + " List , Please generate " + audioFolderName + " folder","ok");
			return;
		}

		if(len == 0){
			EditorUtility.DisplayDialog("Failed: ", "can't create " + audioListname  + " , Audio files is missing on " + audioFolderName + " folder","ok");
			return;
		}

		using (StreamWriter outfile = 
			new StreamWriter(copyPath))
		{
			outfile.WriteLine("using UnityEngine;");
			outfile.WriteLine("using System.Collections;");
			outfile.WriteLine("");
			outfile.WriteLine("public enum " + fileName + "{");

			foreach( object audio in loadedAudio ){
				AudioClip clip = (AudioClip)audio;
				if(len == 1){					
					outfile.WriteLine("\t\t"+clip.name);
				}else{
					count++;
					if(count<len){
						outfile.WriteLine("\t\t"+clip.name + ",");
					}else{
						outfile.WriteLine("\t\t"+clip.name);
					}
				}			
			}

			outfile.WriteLine("}");
		}//File written

		Debug.Log("Create Audio list complete " + audioListname);
	}

	private void CreateConfig(){		

		SoundConfig soundConfig = (SoundConfig)ScriptableWizard.CreateInstance(typeof(SoundConfig));

		sfx = Enum.GetValues(typeof(SFX));
		sfxCount = sfx.Length;
		sfxNames = Enum.GetNames(typeof(SFX));

		for(int index=0;index<sfxCount;index++){
			AudioClip sfxClip =(AudioClip) Resources.Load("SFX/"+sfxNames[index],typeof(AudioClip));
			//Debug.Log( " check sfxClip " + sfxClip );
			soundConfig.sfxDictionary.Set((SFX)sfx.GetValue(index),sfxClip);
		}

		bgm = Enum.GetValues(typeof(BGM));
		bgmCount = bgm.Length;
		bgmNames = Enum.GetNames(typeof(BGM));

		for(int index=0;index<bgmCount;index++){
			AudioClip bgmClip =(AudioClip) Resources.Load("BGM/"+bgmNames[index],typeof(AudioClip));
			//Debug.Log( " check bgmClip " + bgmClip );
			soundConfig.bgmDictionary.Set((BGM)bgm.GetValue(index),bgmClip);
		}

		CreateFolder("Assets/Resources","Config");
		AssetDatabase.CreateAsset(soundConfig,"Assets/Resources/Config/SoundConfig.asset");
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = soundConfig;

		startDelayCreateConfig = false;

		EditorUtility.DisplayDialog("Success: ", "Sound Manager setup successfully","ok");
		Debug.Log("Create Sound Config complete");
	}

	private void Update(){

		if(!EditorApplication.isCompiling && startDelayCreateConfig){			
			CreateConfig();
		}
	}
}
