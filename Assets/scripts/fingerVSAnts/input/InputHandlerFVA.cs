using UnityEngine;
using System.Collections;

public class InputHandlerFVA{

	private ICommandFVA tapCommand;
	private ICommandFVA nullCommand;

	public InputHandlerFVA(){		
		nullCommand = new NullCommandFVA();
	}

	public void SetTap(ICommandFVA command){
		this.tapCommand = command;
	}

	//for binding commands
	public ICommandFVA HandleInput(){
		#if UNITY_5 || UNITY_EDITOR || UNITY_EDITOR_64 
		if ( Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100)) {
				//Debug.Log( " click something " + hit.collider.gameObject.name );
				BugController antController = hit.collider.gameObject.GetComponent<BugController>();
				if(antController!=null){
					antController.TakeDamage();	
				}
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
					//Debug.Log( " click something " + hit.collider.gameObject.name );
					BugController antController = hit.collider.gameObject.GetComponent<BugController>();
					if(antController!=null){
						antController.TakeDamage();	
					}
				}
			}
		}

		#endif


		return nullCommand;
	}
}
