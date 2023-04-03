using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundConfig: ScriptableObject{
	[System.Serializable]
	public class BGMDictionary:InspectorDictionary<BGM,AudioClip>{}
	public BGMDictionary bgmDictionary = new BGMDictionary();

	[System.Serializable]
	public class SfxDictionary:InspectorDictionary<SFX,AudioClip>{}
	public SfxDictionary sfxDictionary = new SfxDictionary();
	
	public AudioClip GetBGM(BGM bgm){
		return bgmDictionary.Get(bgm);
	}

	public AudioClip GetSFX(SFX sfx){
		return sfxDictionary.Get(sfx);
	}
}
