using UnityEngine;
using System.Collections;

public class InputHandler{

	private ICommand buttonLeft;
	private ICommand buttonRight;

	private ICommand tap;
	private ICommand tapLeft;
	private ICommand tapRight;

	private ICommand nullCommand;

	public InputHandler(){		
		nullCommand = new NullCommand();
	}

	//for binding commands
	public void SetButtonLeft(ICommand command){
		this.buttonLeft = command;
	}

	public void SetButtonRight(ICommand command){
		this.buttonRight = command;
	}

	public void SetTapLeft(ICommand command){
		this.tapLeft = command;
	}

	public void SetTapRight(ICommand command){
		this.tapRight = command;
	}

	public void SetTap(ICommand command){
		this.tap = command;
	}

	//for binding commands
	public ICommand HandleInput(){
		#if UNITY_5 || UNITY_EDITOR || UNITY_EDITOR_64 
		if ( Input.GetMouseButtonDown(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast(ray, out hit, 100)) {
				tap.gameobj = hit.collider.gameObject;
				return tap;
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
					tap.gameobj = hit.collider.gameObject;
					return tap;
				}
			}
		}

		#endif


		#if UNITY_ANDROID || UNITY_IOS || UNITY_IPHONE || UNITY_PRO_LICENSE
		foreach (Touch touch in Input.touches) {
			if(touch.phase == TouchPhase.Stationary){
				if (touch.position.x < Screen.width/2) {
					//move left
					return tapLeft;
				}else if (touch.position.x > Screen.width/2) {
					//move right
					return tapRight;
				} 	
			}else if(touch.phase == TouchPhase.Ended){
				//do nothing
				return nullCommand;
			}
		}
		#endif


		#if UNITY_5 || UNITY_EDITOR  || UNITY_EDITOR_32 || UNITY_EDITOR_64 || UNITY_EDITOR_WIN
		if(Input.GetKey(KeyCode.RightArrow)){			
			//MoveRight;
			return buttonRight;
		}else if(Input.GetKey(KeyCode.LeftArrow)){
			//MoveLeft;
			return buttonLeft;
		}else{
			//do nothing
			return nullCommand;
		}
		#endif
	}
}
