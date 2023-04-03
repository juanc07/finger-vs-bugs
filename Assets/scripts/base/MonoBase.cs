using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBase : MonoBehaviour
{
	private const string TAG = "[ MonoBase ]";

	[HideInInspector]
	public GameEventManager gameEventManager;
	[HideInInspector]
	public SoundManager soundManager;

	public void Init()
	{
		Debug.Log(TAG + "Awake");
		// get instance
		soundManager = SoundManager.GetInstance();
		soundManager.LoadAudio();

		gameEventManager = GameEventManager.GetInstance();
	}
}
