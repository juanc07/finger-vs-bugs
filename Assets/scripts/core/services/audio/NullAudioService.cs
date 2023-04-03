using UnityEngine;
using System.Collections;

public class NullAudioService :IAudio {

	public void Init(){
		Debug.LogWarning("you are using null audio service");
	}

	public void LoadAudio(){
		Debug.LogWarning("you are using null audio service");
	}

	public void SetMainBGMVolume(float volume){
		Debug.LogWarning("you are using null audio service");
	}

	public float GetMainBGMVolume(){
		Debug.LogWarning("you are using null audio service");
		return 0f;
	}

	public void SetBGMVolume( BGM bgm,float volume){
		Debug.LogWarning("you are using null audio service");
	}

	public void PlayBGM(BGM bgm,bool isLooping){
		Debug.LogWarning("you are using null audio service");
	}

	public void PlayRandomBGM(BGM[] bgmSet,bool isLooping){		
		Debug.LogWarning("PlayRandomBGM you are using null audio service");
	}

	public void StopBGM(BGM bgm){
		Debug.LogWarning("you are using null audio service");
	}

	public void MuteMainBGM(){
		Debug.LogWarning("you are using null audio service");
	}

	public void StopAllBGM(){
		Debug.LogWarning("StopAllBGM you are using null audio service");
	}

	public void UnMuteMainBGM(){
		Debug.LogWarning("you are using null audio service");
	}

	public void SetSFXVolume( SFX sfx,float volume){
		Debug.LogWarning("you are using null audio service");
	}

	public void SetMainSFXVolume(float volume){
		Debug.Log( " SetMainSFXVolume volume " + volume );
	}

	public void AdjustSFXVolume(float volume){
		Debug.LogWarning("AdjustSFXVolume, you are using null audio service");
	}

	public float GetSFXVolume(){
		Debug.LogWarning("you are using null audio service");
		return 0f;
	}

	public void PlaySfx(SFX sfx){
		Debug.LogWarning("you are using null audio service");
	}

	public void PlayRandomSfx(SFX[] sfxSet){
		Debug.LogWarning("PlayRandomSfx you are using null audio service");
	}

	public void StopSfx(SFX sfx){
		Debug.LogWarning("you are using null audio service");
	}

	public void StopAllSFX(){
		Debug.LogWarning("StopAllSFX you are using null audio service");
	}

	public void MuteSfx(){
		Debug.LogWarning("you are using null audio service");
	}

	public void UnMuteSfx(){
		Debug.LogWarning("you are using null audio service");
	}

	public void DirectSetMainBGMVolume(float vol){
		Debug.LogWarning("[DirectSetMainBGMVolume] you are using null audio service");
	}
}
