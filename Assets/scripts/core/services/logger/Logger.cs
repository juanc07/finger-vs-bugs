using UnityEngine;
using System.Collections;

public class Logger : ILogger{

	public bool enable;

	public void Log(string val){
		if(enable){
			Debug.Log( val );	
		}
	}

	public void Enable(){
		enable = true;
	}

	public void Disable(){
		enable = false;
	}
}
	