using UnityEngine;
using System.Collections;

public class TouchInputController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	public HitObject HandleInput(){

		#if UNITY_5 || UNITY_EDITOR || UNITY_EDITOR_64 
		if ( Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100)) {
				HitObject hitObject = new HitObject();
				hitObject.hitObject = hit.collider.gameObject;
				hitObject.hit = hit;

				return hitObject;
			}
		}
		#endif

		#if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE || UNITY_PRO_LICENSE
		for(int i = 0; i < Input.touchCount; i++)
		{
			Touch touch = Input.GetTouch(i);
			if (touch.phase == TouchPhase.Began) {
				Ray ray = Camera.main.ScreenPointToRay( new Vector3(touch.position.x,touch.position.y,0f) );
				RaycastHit hit;

				if (Physics.Raycast(ray, out hit, 100)) {
					HitObject hitObject = new HitObject();
					hitObject.hitObject = hit.collider.gameObject;
					hitObject.hit = hit;
					return hitObject;
				}
			}
		}

		#endif

		return null;
	}
}
