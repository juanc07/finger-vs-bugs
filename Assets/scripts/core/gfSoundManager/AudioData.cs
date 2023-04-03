using UnityEngine;
using System.Collections;

public class AudioData{
	public int id;
	public string name;
	public AudioClip clip;
	public AudioDataType type=AudioDataType.SFX;

	public enum AudioDataType{
		SFX,
		BGM
	}
}
