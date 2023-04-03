using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class PreloadSigningAlias : MonoBehaviour {

	static PreloadSigningAlias ()
	{
		PlayerSettings.Android.keystorePass = GameConfig.KEYSTORE_PASS;
		PlayerSettings.Android.keyaliasName = GameConfig.KEYSTORE_ALIAS_NAME;
		PlayerSettings.Android.keyaliasPass = GameConfig.KEYSTORE_ALIAS_PASS;
	}
}
