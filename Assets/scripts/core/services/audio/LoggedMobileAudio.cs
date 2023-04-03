using UnityEngine;
using System.Collections;

public class LoggedMobileAudio :IAudio {

	private IAudio audioServiceWrapper;
	private string TAG = "LoggedMobileAudio";

	public LoggedMobileAudio(IAudio wrapper){
		audioServiceWrapper = wrapper;
	}

	public void Init(){
		Debug.Log( TAG + " Init" );
		audioServiceWrapper.Init();
	}

	public void LoadAudio(){
		Debug.Log( TAG + " LoadAudio" );
		audioServiceWrapper.LoadAudio();
	}

	public void SetMainBGMVolume(float volume){
		Debug.Log( TAG + " SetBGMVolume volume " + volume );
		audioServiceWrapper.SetMainBGMVolume(volume);
	}

	public void SetBGMVolume(BGM bgm,float volume){
		Debug.Log( TAG + " SetBGMVolume volume " + volume );
		audioServiceWrapper.SetBGMVolume(bgm, volume);
	}

	public float GetMainBGMVolume(){
		Debug.Log( TAG + " GetBGMVolume volume " );
		return audioServiceWrapper.GetMainBGMVolume();
	}

	public void PlayBGM(BGM bgm,bool isLooping){
		Debug.Log( TAG + " PlayBGM bgm " + bgm);
		audioServiceWrapper.PlayBGM(bgm,isLooping);
	}

	public void PlayRandomBGM(BGM[] bgmSet,bool isLooping){
		Debug.Log( TAG + " PlayRandomBGM bgmSet " + bgmSet);
		audioServiceWrapper.PlayRandomBGM(bgmSet,isLooping);
	}

	public void StopBGM(BGM bgm){
		Debug.Log( TAG + " StopBGM bgm ");
		audioServiceWrapper.StopBGM(bgm);
	}

	public void StopAllBGM(){
		Debug.Log( TAG + " StopAllBGM ");
		audioServiceWrapper.StopAllBGM();
	}

	public void MuteMainBGM(){
		Debug.Log( TAG + " MuteBGM"  );
		audioServiceWrapper.MuteMainBGM();
	}

	public void UnMuteMainBGM(){
		Debug.Log( TAG + " UnMuteBGM"  );
		audioServiceWrapper.UnMuteMainBGM();
	}

	public void SetSFXVolume(SFX sfx,float volume){
		Debug.Log( TAG + " SetSFXVolume volume " + volume );
		audioServiceWrapper.SetSFXVolume(sfx, volume);
	}

	public void SetMainSFXVolume(float volume){
		Debug.Log( TAG + " SetMainSFXVolume volume " + volume );
		audioServiceWrapper.SetMainSFXVolume( volume);
	}

	public float GetSFXVolume(){
		Debug.Log( TAG + " GetSFXVolume volume ");
		return audioServiceWrapper.GetSFXVolume();
	}

	public void PlaySfx(SFX sfx){
		audioServiceWrapper.PlaySfx(sfx);
	}

	public void PlayRandomSfx(SFX[] sfxSet){
		Debug.Log( TAG + " PlayRandomSfx sfxSet " + sfxSet );
		audioServiceWrapper.PlayRandomSfx(sfxSet);
	}

	public void StopSfx(SFX sfx){
		audioServiceWrapper.StopSfx(sfx);
	}

	public void StopAllSFX(){
		Debug.Log( TAG + " StopAllSFX ");
		audioServiceWrapper.StopAllSFX();
	}

	public void MuteSfx(){
		audioServiceWrapper.MuteSfx();
	}

	public void UnMuteSfx(){
		audioServiceWrapper.UnMuteSfx();
	}

	public void DirectSetMainBGMVolume(float vol){
		audioServiceWrapper.DirectSetMainBGMVolume(vol);
	}
}
