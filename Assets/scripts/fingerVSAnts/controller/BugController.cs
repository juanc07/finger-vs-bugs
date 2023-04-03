using UnityEngine;
using System.Collections;
using System;

public class BugController : MonoBehaviour {

	private BugEventManager antEventManager;
	public BugAnimationController antAnimationController;
	private BugHitColorController antHitColorController;

	private int id;
	public BugType bugType;
	public int hp;
	public int fullHP;
	public float moveSpeed = 0.25f;

	// states
	public BugState bugState;
	public bool isInPool;

	public float decay = 15f;

	public bool hasHitDelay;
	public float hitDelay = 0.9f;

	private Transform antTransform;
	private BoxCollider antCollider;


	public int minRespawnPositionX;
	public int maxRespawnPositionX;

	public int minRespawnPositionZ;
	public int maxRespawnPositionZ;
	public int maxZ;

	public GameMode gameMode;
	public bool allowToMove = true;

	void Awake () {	
		antEventManager = BugEventManager.GetInstance();
		antTransform = this.gameObject.GetComponent<Transform>();
		antHitColorController = this.gameObject.GetComponent<BugHitColorController>();
		antCollider =  this.gameObject.GetComponent<BoxCollider>();
		Init();
	}

	// Use this for initialization
	void Start () {		
		AddEventListener();
	}

	private void OnDestroy(){
		RemoveEventListener();
	}
	
	// Update is called once per frame
	void Update () {
		if(allowToMove){
			CheckWalk();	
		}
	}

	public void SetAllowToMove(bool val){		
		allowToMove = val;
		if(val){
			ResumeAnimation();
		}else{
			PausedAnimation();
		}
	}

	public void ResetAllowToMove(){
		CancelInvoke();
		hasHitDelay = false;
		allowToMove = true;
		PlayWalkAnimation(true);
		ResumeAnimation();
	}

	private void Init(){
		hp=fullHP;
		EnableDisableCollider(true);
		SetIsAlive(true);
		SetState(BugState.WALK);
		PlayWalkAnimation(true);
	}

	public void SetState(BugState state){
		bugState = state;
	}

	public BugState GetState(){
		return bugState;
	}

	private void AddEventListener(){
		if(antAnimationController!=null){
			antAnimationController.OnWalkStateChange+=OnWalkStateChange;
		}

		if(antHitColorController!=null){
			antHitColorController.OnAntFade+=OnAntFade;
		}
	}

	private void RemoveEventListener(){
		if(antAnimationController!=null){
			antAnimationController.OnWalkStateChange-=OnWalkStateChange;
		}

		if(antHitColorController!=null){
			antHitColorController.OnAntFade-=OnAntFade;
		}
	}

	public void setID(int id){
		this.id = id;
	}

	public int getID(){
		return this.id;
	}

	public bool TakeDamage(){		
		if(hp > 0  && !hasHitDelay){
			hp--;
			PlayWalkAnimation(false);
			SetState(BugState.HIT);
			antEventManager.DispatchAntStatusChange(BugEvent.HIT, this);
			CheckIfDead();

			hasHitDelay = true;
			CancelInvoke("RefreshHitDelay");
			Invoke("RefreshHitDelay",hitDelay);

			return true;
		}else{
			return false;
		}
	}

	private void RefreshHitDelay(){
		if(GetState()!=BugState.DEAD && hp > 0){
			SetState(BugState.WALK);
			PlayWalkAnimation(true);	
		}

		hasHitDelay = false;
	}

	private void DispatchAntDecayEvent(){
		antEventManager.DispatchAntStatusChange(BugEvent.DECAY, this);
	}

	private void CheckIfDead(){
		if(hp <= 0){
			SetStatusDead();
		}
	}

	private void SetStatusDead(){
		if(GetState() != BugState.DEAD){
			SetState(BugState.DEAD);

			int rnd = UnityEngine.Random.Range(0,3);
			SetDeathAnimation(rnd);
			SetIsAlive(false);

			EnableDisableCollider(false);
			antEventManager.DispatchAntStatusChange(BugEvent.DIED, this);
			StartDecay();	
		}
	}

	private void EnableDisableCollider( bool val ){
		if(antCollider!=null){
			antCollider.enabled = val;
		}
	}

	private void StartDecay(){
		Invoke("DispatchAntDecayEvent",decay * Time.deltaTime);
	}

	private void ClearDecay(){
		CancelInvoke("DispatchAntDecayEvent");
	}

	private void SetIsAlive(bool val){
		if(antAnimationController!=null){
			antAnimationController.SetIsAlive(val);
		}
	}

	private void PlayIdleAnimation(bool val){
		if(antAnimationController!=null){			
			antAnimationController.PlayIdle(val);
		}
	}

	private void PlayWalkAnimation(bool val){
		if(antAnimationController!=null){			
			antAnimationController.PlayWalk(val);	
		}
	}

	private void SetDeathAnimation(int val){
		if(antAnimationController!=null){			
			antAnimationController.setDeathType(val);
		}
	}

	private void PlayRespawnAnimation(bool val){
		if(antAnimationController!=null){			
			antAnimationController.PlayRespawn(val);
		}
	}

	private void PausedAnimation(){
		if(antAnimationController!=null){			
			antAnimationController.PausedAnimation();	
		}
	}

	private void ResumeAnimation(){
		if(antAnimationController!=null){			
			antAnimationController.ResumeAnimation();	
		}
	}

	private void CheckWalk(){
		if(GetState() == BugState.DEAD){
			return;
		}

		if(antTransform!=null){
			MoveDown();
			CheckPositionZ();
		}
	}

	private void MoveDown(){
		Vector3 tempPosition = antTransform.position;
		tempPosition.z -= moveSpeed * Time.deltaTime;
		antTransform.position = tempPosition;
		//Debug.Log( " antTransform.position z " + antTransform.position.z );
	}

	public void KnockBack( float val ){
		Vector3 tempPosition = antTransform.position;
		tempPosition.z += val;
		antTransform.position = tempPosition;
		//Debug.Log( " antTransform.position z " + antTransform.position.z );
	}

	private void CheckPositionZ(){
		if(antTransform.position.z <= maxZ && !isInPool){
			DeActivate(false);
			antEventManager.DispatchAntStatusChange(BugEvent.ESCAPE, this);
		}
	}

	public void DeActivate(bool isForced){
		if(!isInPool || isForced){
			//remove tween on render materials
			ClearDecay();
			if(antHitColorController!=null){
				antHitColorController.KillTween();	
			}


			isInPool = true;
			this.gameObject.SetActive(false);
		}
	}

	public void Activate(float xPos, float zPos){
		//Debug.Log("Activate ant 1 isInPool " + isInPool);
		if(isInPool){			
			isInPool = false;
			this.gameObject.SetActive(true);

			if(antHitColorController!=null){
				antHitColorController.ResetColor();	
			}

			Respawn(xPos,zPos);
			Init();
			antEventManager.DispatchAntStatusChange(BugEvent.SPAWN, this);

			//Debug.Log("Activate ant 2");
		}
	}

	private void Respawn(float xPos, float zPos){
		//float rndX = UnityEngine.Random.Range( minRespawnPositionX, maxRespawnPositionX );
		//float rndZ = UnityEngine.Random.Range( minRespawnPositionZ, maxRespawnPositionZ );
		float rndX = xPos;
		float rndZ = zPos;

		Vector3 tempPosition = antTransform.position;
		tempPosition.x = rndX;
		tempPosition.z = rndZ;
		antTransform.position = tempPosition;

		SetState(BugState.RESPAWN);
		PlayRespawnAnimation(true);
	}

	private void OnWalkStateChange(AnimationState animationState){
		//Debug.Log("<color=blue> OnWalkStateChange </color>" + animationState);
		if(animationState == AnimationState.START){
			PlayRespawnAnimation(false);
		}else if(animationState == AnimationState.END){
			
		}	
	}
	// animation state events

	// ant hit color controller events
	private void OnAntFade(){
		if(!isInPool){
			DeActivate(false);
		}
	}
}
