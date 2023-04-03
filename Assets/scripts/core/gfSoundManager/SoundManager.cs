using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SoundManager : MonoBehaviour,ICleanable {

	/* note: playing sfx and bgm is different on sfx you can play same sfx on multiple
	 * audio source but in bgm you cannot do that,but instead you can play a lot of different
	 * bgm at the same time using different audio source
	*/

	private const string TAG="SoundManager";

	private static SoundManager instance;
	private static GameObject container;

	private List<AudioData> sfxCollection = new List<AudioData>();
	private List<AudioData> bgmCollection = new List<AudioData>();

	// collection of audio source for each sfx and bgm
	private List<AudioSource> sfxAudioSourceCollection =new List<AudioSource>();
	private List<AudioSource> bgmAudioSourceCollection =new List<AudioSource>();

	// collection of volumes for each sfx and bgm
	private Dictionary<SFX,float> sfxVolumeCollection = new Dictionary<SFX, float>();
	private Dictionary<BGM,float> bgmVolumeCollection = new Dictionary<BGM, float>();
	
	private SoundConfig soundConfig;
	public bool isInitialized =false;
	public bool isReady =false;

	// tell if sfx is enable or disable
	public bool isSfxOn = true;

	// tell if bgm is enable or disable
	public bool isBgmOn =true;

	// bgm main volume
	private float bgmVolume;
	// before mute save the last bgm main volume here
	private float lastBgmVolume =1f;

	// sfx main volume
	private float sfxVolume;
	// before mute save the last sfx main volume here
	private float lastSFXVolume;

	// initial count of audio source for sfx
	private int sfxInitCount = 18;

	// initial count of audio source for bgm
	private int bgmInitCount = 1;

	// event for telling that sound manager is ready
	private Action SoundManagerReady;
	public event Action OnSoundManagerReady{
		add{SoundManagerReady+=value;}
		remove{SoundManagerReady-=value;}
	}

	// event for telling that bgm has completely finish playing
	private Action <BGM>BackGroundMusicPlayComplete;
	public event Action <BGM>OnBackGroundMusicPlayComplete{
		add{BackGroundMusicPlayComplete+=value;}
		remove{BackGroundMusicPlayComplete-=value;}
	}

	// event for telling that sfx has completely finish playing
	private Action <SFX>SoundEffectPlayComplete;
	public event Action <SFX>OnSoundEffectPlayComplete{
		add{SoundEffectPlayComplete+=value;}
		remove{SoundEffectPlayComplete-=value;}
	}
	
	public static SoundManager GetInstance(){
		if(instance==null){
			container = new GameObject();
			container.name ="SoundManager";
			instance = container.AddComponent(typeof(SoundManager)) as SoundManager;
			DontDestroyOnLoad(instance);
		}
		
		return instance;
	}
	
	public void Clean(){
		int sfxCount = sfxCollection.Count;
		for(int index=0;index < sfxCount;index++){
			sfxCollection[index] = null;
		}
		sfxCollection.Clear();
		
		int bgmCount = bgmCollection.Count;
		for(int index=0;index < bgmCount;index++){
			bgmCollection[index] = null;
		}
		bgmCollection.Clear();

		sfxAudioSourceCollection.Clear();
		bgmAudioSourceCollection.Clear();

		sfxVolumeCollection.Clear();
		bgmVolumeCollection.Clear();
		
		sfxCollection = null;
		bgmCollection = null;
		sfxAudioSourceCollection = null;
		bgmAudioSourceCollection = null;
		sfxVolumeCollection = null;
		bgmVolumeCollection = null;
	}

	private AudioClip CheckCachedBGM( BGM bgm ){
		int count = bgmCollection.Count;
		AudioClip clip = null;
		for(int index=0;index < count;index++){
			if(bgmCollection[index].name.Equals(bgm.ToString(),StringComparison.Ordinal)){
				clip = bgmCollection[index].clip;
				break;
			}
		}

		if(clip==null){
			clip = soundConfig.GetBGM(bgm);
			AudioData audioData = new AudioData();
			audioData.id = bgmCollection.Count+1;
			audioData.name = bgm.ToString();
			audioData.clip = clip;
			audioData.type = AudioData.AudioDataType.BGM;
			bgmCollection.Add(audioData);
		}

		return clip;
	}

	public void PlayBGM(BGM bgm,bool isLooping){
		if(!isBgmOn){
			return;
		}

		AudioClip clip = CheckCachedBGM(bgm);
		AudioSource audioBgm = SearchGetBGMAudioSource(clip);

		if(clip!=null && audioBgm!=null ){
			audioBgm.volume = bgmVolumeCollection[bgm] * bgmVolume;
			audioBgm.loop =isLooping;
			audioBgm.clip = clip;
			audioBgm.Play();
			StartCoroutine(BGMPlayComplete(audioBgm.clip.length,bgm));
		}else{
			Debug.Log("bgm not yet loaded!, please check your sound config");
		}
	}

	private IEnumerator BGMPlayComplete(float waitTime,BGM bgm){		
		yield return new WaitForSeconds(waitTime);
		if(null!=BackGroundMusicPlayComplete){
			BackGroundMusicPlayComplete(bgm);
		}
	}

	public void PlayRandomBGM(BGM[] bgmSet,bool isLooping){
		int randomBGM = UnityEngine.Random.Range(0,bgmSet.Length);
		PlayBGM(bgmSet[randomBGM],isLooping);
	}

	public void StopBGM(BGM bgm){		
		if(!isBgmOn){
			return;
		}

		AudioClip clip = CheckCachedBGM(bgm);
		AudioSource audioBgm = SearchGetBGMAudioSource(clip);

		if(clip!=null && audioBgm!=null ){
			if(audioBgm.clip.name.Equals(clip.name,StringComparison.Ordinal)){
				audioBgm.Stop();
				StopCoroutine("BGMPlayComplete");
			}
		}else{
			Debug.Log("bgm not yet loaded!, please check your sound config");
		}
	}

	public void PauseBGM(BGM bgm){
		if(!isBgmOn){
			return;
		}

		AudioClip clip = CheckCachedBGM(bgm);
		AudioSource audioBgm = SearchGetBGMAudioSource(clip);


		if(clip!=null && audioBgm!=null){
			if(audioBgm.clip.name.Equals(clip.name,StringComparison.Ordinal)){
				audioBgm.Pause();
				StopCoroutine("BGMPlayComplete");
			}
		}else{
			Debug.Log("bgm not yet loaded!, please check your sound config");
		}
	}

	public void UnPauseBGM(BGM bgm){
		if(!isBgmOn){
			return;
		}

		AudioClip clip = CheckCachedBGM(bgm);
		AudioSource audioBgm = SearchGetBGMAudioSource(clip);


		if(clip!=null && audioBgm!=null ){
			if(audioBgm.clip.name.Equals(clip.name,StringComparison.Ordinal)){
				audioBgm.UnPause();
				StopCoroutine("BGMPlayComplete");
			}
		}else{
			Debug.Log("bgm not yet loaded!, please check your sound config");
		}
	}

	public void MuteBGM(BGM bgm){
		if(!isBgmOn){
			return;
		}

		AudioClip clip = CheckCachedBGM(bgm);
		AudioSource audioBgm = SearchGetBGMAudioSource(clip);

		if(clip!=null && audioBgm!=null ){
			if(audioBgm.clip.name.Equals(clip.name,StringComparison.Ordinal)){
				audioBgm.mute =  true;
				StopCoroutine("BGMPlayComplete");
			}
		}else{
			Debug.Log("bgm not yet loaded!, please check your sound config");
		}
	}

	public void UnMuteBGM(BGM bgm){
		if(!isBgmOn){
			return;
		}

		AudioClip clip = CheckCachedBGM(bgm);
		AudioSource audioBgm = SearchGetBGMAudioSource(clip);

		if(clip!=null && audioBgm!=null ){
			if(audioBgm.clip.name.Equals(clip.name,StringComparison.Ordinal)){
				audioBgm.mute =  false;
				StopCoroutine("BGMPlayComplete");
			}
		}else{
			Debug.Log("bgm not yet loaded!, please check your sound config");
		}
	}

	public void StopAllBGM(){
		StopAllCoroutines();
		int count = bgmAudioSourceCollection.Count;
		for( int index=0;index<count; index++ ){
			bgmAudioSourceCollection[index].Stop();
		}
	}

	
	private AudioClip CheckCachedSFX( SFX sfx ){
		int count = sfxCollection.Count;
		AudioClip clip = null;
		for(int index=0;index < count;index++){
			if(sfxCollection[index].name.Equals(sfx.ToString(),StringComparison.Ordinal)){
				clip = sfxCollection[index].clip;
				break;
			}
		}
		
		if(clip==null){
			try
			{
				clip = soundConfig.GetSFX(sfx);
			}
			catch(Exception e)
			{
				Debug.Log(e.Message);
				return null;
			}
			AudioData audioData = new AudioData();
			audioData.id = sfxCollection.Count+1;
			audioData.name = sfx.ToString();
			audioData.clip = clip;
			audioData.type = AudioData.AudioDataType.SFX;
			sfxCollection.Add(audioData);
		}
		
		return clip;
	}

	public void PlaySfx(SFX sfx){		
		if(!isSfxOn){
			return;
		}

		AudioSource audioSfx = SearchGetSFXAudioSource();
		AudioClip clip = CheckCachedSFX(sfx);
		if(clip!=null){
			audioSfx.volume = sfxVolumeCollection[sfx] * sfxVolume;
			audioSfx.loop =false;
			audioSfx.clip = clip;
			audioSfx.Play();
			StartCoroutine(SFXPlayComplete(audioSfx.clip.length,sfx));
		}else{
			Debug.Log("Sfx not yet loaded!, please check your sound config");
		}
	}

	private IEnumerator SFXPlayComplete(float waitTime,SFX sfxName){		
		yield return new WaitForSeconds(waitTime);
		if(null!=SoundEffectPlayComplete){
			SoundEffectPlayComplete(sfxName);
		}
	}

	public void PlayRandomSfx(SFX[] sfxSet){
		int randomSFX = UnityEngine.Random.Range(0,sfxSet.Length);
		PlaySfx(sfxSet[randomSFX]);
	}

	public void StopSfx(SFX sfx){
		if(!isSfxOn){
			return;
		}

		AudioSource audioSfx = SearchGetSFXAudioSource();
		AudioClip clip = CheckCachedSFX(sfx);

		if(clip!=null){			
			if(audioSfx.clip.name.Equals(clip.name,StringComparison.Ordinal)){
				audioSfx.Stop();
				StopCoroutine("SFXPlayComplete");
			}
		}else{
			Debug.Log("Sfx not yet loaded!, please check your sound config");
		}
	}

	public void StopAllSFX(){
		StopAllCoroutines();
		int count = sfxAudioSourceCollection.Count;
		for( int index=0;index<count; index++ ){
			sfxAudioSourceCollection[index].Stop();
		}
	}

	// forgot what this thing does??
	private void EnableOrCreateAudioListener(){
		AudioListener[] audioListeners = GameObject.FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
		foreach(AudioListener audioListener in  audioListeners ){
			audioListener.enabled = false;
		}
		
		AudioListener ownAudioListener = this.gameObject.GetComponent<AudioListener>();
		if(ownAudioListener==null){
			this.gameObject.AddComponent<AudioListener>();
		}else{
			ownAudioListener.enabled = true;
		}
	}
	
	private AudioSource CreateAudioSource(string audioSourceName){
		AudioSource audioSource;
		
		GameObject audioSourceHolder = new GameObject();
		audioSourceHolder.name = audioSourceName;
		audioSourceHolder.transform.parent = this.gameObject.transform;
		audioSourceHolder.AddComponent<AudioSource>();
		audioSource = audioSourceHolder.GetComponent<AudioSource>();
		
		return audioSource;
	}
	
	private void CreateSFXAndBGMHolder(){
		//init sfx and bgm holder
		if(!isInitialized){
			for(int index=0;index<bgmInitCount;index++){
				bgmAudioSourceCollection.Add(CreateAudioSource("BGM_"+bgmAudioSourceCollection.Count));
			}

			for(int index=0;index<sfxInitCount;index++){
				sfxAudioSourceCollection.Add(CreateAudioSource("SFX_"+sfxAudioSourceCollection.Count));
			}
			
			isInitialized = true;
		}
	}

	private AudioSource SearchGetBGMAudioSource(AudioClip clip){
		int audioSourceCnt = bgmAudioSourceCollection.Count;
		AudioSource found = null;
		for(int index=0;index<audioSourceCnt;index++){
			if(bgmAudioSourceCollection[index]!= null){
				if(!bgmAudioSourceCollection[index].isPlaying){
					// save this first in case no audio source with the search clip
					found = bgmAudioSourceCollection[index];	
				}

				if(bgmAudioSourceCollection[index].clip != null){					
					if(bgmAudioSourceCollection[index].clip.name.Equals( clip.name,StringComparison.Ordinal )){
						// found already existing bgm with same clip get this instead
						found = bgmAudioSourceCollection[index];
						break;
					}
				}				
			}
		}

		if(found == null){
			// if didn't found any create a new one and used this instead
			bgmAudioSourceCollection.Add(CreateAudioSource("BGM_"+bgmAudioSourceCollection.Count));
			found = bgmAudioSourceCollection[bgmAudioSourceCollection.Count-1];
		}

		return found;
	}

	public void DirectSetMainBGMVolume(float vol){
		bgmVolume = vol;
		int audioSourceCnt = bgmAudioSourceCollection.Count;
		for(int index=0;index<audioSourceCnt;index++){
			if(bgmAudioSourceCollection[index]!= null){
				foreach(KeyValuePair<BGM, float> obj in bgmVolumeCollection){
					if(bgmAudioSourceCollection[index].clip != null){					
						if(bgmAudioSourceCollection[index].clip.name.Equals( obj.Key.ToString(),StringComparison.Ordinal )){
							bgmAudioSourceCollection[index].volume = obj.Value * bgmVolume;
						}
					}
				}
			}
		}
	}
	
	private AudioSource SearchGetSFXAudioSource(){
		int audioSourceCnt = sfxAudioSourceCollection.Count;
		AudioSource found = null;
		for(int index=0;index<audioSourceCnt;index++){
			if(sfxAudioSourceCollection[index]!= null){
				if(!sfxAudioSourceCollection[index].isPlaying){
					found = sfxAudioSourceCollection[index];
					break;
				}
			}
		}
		
		if(found == null){
			sfxAudioSourceCollection.Add(CreateAudioSource("SFX_"+sfxAudioSourceCollection.Count));
			found = sfxAudioSourceCollection[sfxAudioSourceCollection.Count-1];
		}
		
		return found;
	}

	public void SetMainBGMVolume(float volume){
		bgmVolume = volume;
	}	

	public float GetMainBGMVolume(){
		return bgmVolume;
	}

	// set the bgm volume for each bgm
	public void SetBGMVolume(BGM bgm,float volume){
		if(!bgmVolumeCollection.ContainsKey(bgm)){
			bgmVolumeCollection.Add(bgm,volume);
		}
	}

	public void MuteMainBGM(){
		isBgmOn =false;
		lastBgmVolume = bgmVolume;
		SetMainBGMVolume(0);
	}

	public void UnMuteMainBGM(){
		isBgmOn =true;
		SetMainBGMVolume(lastBgmVolume);
	}
	
	public void SetMainSFXVolume(float volume){
		sfxVolume = volume;
	}

	public float GetMainSFXVolume(){
		return sfxVolume;
	}

	// set the sfx volume for each sfx 
	public void SetSFXVolume(SFX sfx,float volume){
		if(!sfxVolumeCollection.ContainsKey(sfx)){
			sfxVolumeCollection.Add(sfx,volume);
		}
	}
	
	public void MuteMainSfx(){
		isSfxOn =false;
		lastSFXVolume = sfxVolume;
		SetMainSFXVolume(0);
	}
	
	public void UnMuteMainSfx(){
		isSfxOn =true;
		SetMainSFXVolume(lastSFXVolume);
	}

	// load and init audio bgm and sfx
	public void LoadAudio(){
		soundConfig = (SoundConfig)Resources.Load("Config/SoundConfig");
		CreateSFXAndBGMHolder();
		//EnableOrCreateAudioListener();		

		if(null!=SoundManagerReady){
			isReady = true;
			SoundManagerReady();
		}
	}
}
