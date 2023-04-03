using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

public class BugEventManager : MonoBehaviour {

	private static BugEventManager instance;
	private static GameObject container;

	private Action <BugEvent,BugController>BugStatusChange;
	public event Action <BugEvent,BugController>OnBugStatusChange{
		add{ BugStatusChange+=value; }
		remove{ BugStatusChange-=value; }
	}

	public static BugEventManager GetInstance(){
		if(instance == null){
			container = new GameObject();
			container.name = "AntEventManager";
			instance = container.AddComponent(typeof(BugEventManager)) as BugEventManager;
			DontDestroyOnLoad(instance.gameObject);
		}

		return instance;
	}

	// Use this for initialization
	void Start () {

	}

	public void DispatchAntStatusChange( BugEvent antEvent, BugController antController ){
		if(null!=BugStatusChange){
			BugStatusChange(antEvent,antController);
		}
	}
}

