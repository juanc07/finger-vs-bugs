using UnityEngine;
using System.Collections;

public class AutoRotationHelper : MonoBehaviour {

	public bool on;

	// Use this for initialization
	void Start () {
		/*if(on){			
			Screen.orientation = ScreenOrientation.AutoRotation;
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
		}*/
	}

	void Update() {
		if (Input.deviceOrientation == DeviceOrientation.Portrait){
			Screen.orientation = ScreenOrientation.Portrait;
		}else if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown){
			Screen.orientation = ScreenOrientation.PortraitUpsideDown;
		}			
	}
}
