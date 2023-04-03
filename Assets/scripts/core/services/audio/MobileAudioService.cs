using UnityEngine;
using System.Collections;

public class MobileAudioService :IAudio {

	private SoundManager soundManager;

	public void Init(){
		soundManager = SoundManager.GetInstance();
	}

	public void LoadAudio(){
		soundManager.LoadAudio();
	}

	public void SetMainBGMVolume(float volume){
		soundManager.SetMainBGMVolume(volume);
	}

	public float GetMainBGMVolume(){
		return soundManager.GetMainBGMVolume();
	}

	public void SetBGMVolume(BGM bgm, float volume){
		soundManager.SetBGMVolume(bgm,volume);
	}

	public void PlayBGM(BGM bgm,bool isLooping){
		soundManager.PlayBGM(bgm,isLooping);
	}

	public void PlayRandomBGM(BGM[] bgmSet,bool isLooping){
		Debug.Log(" PlayRandomBGM bgmSet " + bgmSet);
		soundManager.PlayRandomBGM(bgmSet,isLooping);
	}

	public void StopBGM(BGM bgm){
		soundManager.StopBGM(bgm);
	}

	public void StopAllBGM(){
		soundManager.StopAllBGM();
	}

	public void MuteMainBGM(){
		soundManager.MuteMainBGM();
	}

	public void UnMuteMainBGM(){
		soundManager.UnMuteMainBGM();
	}

	public void SetSFXVolume(SFX sfx, float volume){
		soundManager.SetSFXVolume(sfx,volume);
	}

	public void SetMainSFXVolume(float volume){
		soundManager.SetMainSFXVolume(volume);
	}

	public float GetSFXVolume(){
		return soundManager.GetMainSFXVolume();
	}

	public void PlaySfx(SFX sfx){
		soundManager.PlaySfx(sfx);
	}

	public void PlayRandomSfx(SFX[] sfxSet){
		soundManager.PlayRandomSfx(sfxSet);
	}

	public void StopSfx(SFX sfx){
		soundManager.StopSfx(sfx);
	}

	public void StopAllSFX(){
		soundManager.StopAllSFX();
	}

	public void MuteSfx(){
		soundManager.MuteMainSfx();
	}

	public void UnMuteSfx(){
		soundManager.UnMuteMainSfx();
	}

	public void DirectSetMainBGMVolume(float vol){
		soundManager.DirectSetMainBGMVolume(vol);
	}
}
