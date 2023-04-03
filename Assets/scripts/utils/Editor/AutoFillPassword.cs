#if UNITY_EDITOR
using UnityEditor;
//using UnityEditor.Build;
using UnityEngine;

//public class AutoFillPassword :  IPreprocessBuild{
public class AutoFillPassword{
	public int CallbackOrder { get { return 0; } }
	public void OnPreprocessBuild(BuildTarget target, string path) {
		Debug.Log("MyCustomBuildProcessor.OnPreprocessBuild for target " + target + " at path " + path);

		PlayerSettings.Android.keystorePass = "KEYSTORE_PASS";
		PlayerSettings.Android.keyaliasName = "ALIAS_NAME";
		PlayerSettings.Android.keyaliasPass = "ALIAS_PASSWORD";
	}
}
#endif


