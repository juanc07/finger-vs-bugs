using UnityEngine;
using System.Collections;

public interface IAudio{	

	void Init();
	void LoadAudio();

	void SetMainBGMVolume(float volume);
	void SetBGMVolume(BGM bgm, float volume);
	float GetMainBGMVolume();
	void DirectSetMainBGMVolume(float vol);

	void PlayBGM(BGM bgm,bool isLooping);
	void PlayRandomBGM(BGM[] bgmSet,bool isLooping);
	void StopBGM(BGM bgm);
	void StopAllBGM();
	void MuteMainBGM();
	void UnMuteMainBGM();

	void SetSFXVolume(SFX sfx, float volume);
	void SetMainSFXVolume(float volume);
	float GetSFXVolume();

	void PlaySfx(SFX sfx);
	void PlayRandomSfx(SFX[] sfxSet);
	void StopSfx(SFX sfx);
	void StopAllSFX();
	void MuteSfx();
	void UnMuteSfx();
}
