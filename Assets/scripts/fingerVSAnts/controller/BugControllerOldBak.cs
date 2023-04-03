using UnityEngine;
using System.Collections;
using System;

public class BugControllerOldBak : MonoBehaviour {
	
	private Action <BugEvent,BugType>AntStatusChange;
	public event Action <BugEvent,BugType>OnAntStatusChange{
		add{ AntStatusChange+=value; }
		remove{ AntStatusChange-=value; }
	}

	public BugAnimationController antAnimationController;
	private BugHitColorController antHitColorController;

	public BugType antType;
	public int hp;
	public int fullHP;
	public float moveSpeed = 0.25f;

	// states
	public BugState antState;
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

	void Awake () {		
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
		CheckWalk();
	}

	private void Init(){
		hp=fullHP;
		EnableDisableCollider(true);
		SetIsAlive(true);

		//SetState(AntState.IDLE);
		SetState(BugState.WALK);
		//PlayIdleAnimation(true);
		PlayWalkAnimation(true);
	}

	public void SetState(BugState state){
		antState = state;
	}

	public BugState GetState(){
		return antState;
	}

	private void AddEventListener(){
		if(antAnimationController!=null){			
			antAnimationController.OnIdleStateChange+=OnIdleStateChange;
			antAnimationController.OnWalkStateChange+=OnWalkStateChange;
			antAnimationController.OnHitStateChange+=OnHitStateChange;
			antAnimationController.OnAttackAnimationStateChange += OnAttackAnimationStateChange;
		}

		if(antHitColorController!=null){
			antHitColorController.OnAntFade+=OnAntFade;
		}
	}

	private void RemoveEventListener(){
		if(antAnimationController!=null){			
			antAnimationController.OnIdleStateChange-=OnIdleStateChange;
			antAnimationController.OnWalkStateChange-=OnWalkStateChange;
			antAnimationController.OnHitStateChange-=OnHitStateChange;
			antAnimationController.OnAttackAnimationStateChange -= OnAttackAnimationStateChange;
		}

		if(antHitColorController!=null){
			antHitColorController.OnAntFade-=OnAntFade;
		}
	}

	public void TakeDamage(){		
		//if(hp > 0 && GetState() != AntState.HIT){
		if(hp > 0 && GetState() != BugState.HIT && GetState() != BugState.ATTACK){	
			//Debug.Log("hit ant!");
			hp--;
			PlayWalkAnimation(false);
			SetState(BugState.HIT);
			PlayHitAnimation(true);
			DispatchAntStatusChange(BugEvent.HIT, antType);
		}else if( hp > 0 && !hasHitDelay ){			
			//Debug.Log("hit ant!");
			hasHitDelay = true;
			hp--;
			PlayWalkAnimation(false);
			SetState(BugState.HIT);
			DispatchAntStatusChange(BugEvent.HIT, antType);
			RefreshHit();

			CancelInvoke("RefreshHitDelay");
			Invoke("RefreshHitDelay",hitDelay);
		}
	}

	private void RefreshHitDelay(){
		hasHitDelay = false;
	}

	private void DispatchAntStatusChange( BugEvent antEvent, BugType antType ){
		if(null!=AntStatusChange){
			AntStatusChange(antEvent,antType);
		}
	}

	private void DispatchAntDecayEvent(){
		DispatchAntStatusChange(BugEvent.DECAY, antType);
	}

	private void RefreshHit(){
		if(hp <= 0){
			SetStatusDead();
		}else{			
			SetStatusAttack();
		}
	}

	private void SetStatusDead(){
		if(GetState() != BugState.DEAD){
			SetState(BugState.DEAD);

			int rnd = UnityEngine.Random.Range(0,3);
			SetDeathAnimation(rnd);
			SetIsAlive(false);

			EnableDisableCollider(false);
			DispatchAntStatusChange(BugEvent.DIED, antType);
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

	private void SetStatusAttack(){
		SetState(BugState.ATTACK);

		int rnd = UnityEngine.Random.Range(0,3);
		SetAttackAnimation(rnd);

		PlayAttackAnimation(true);
	}

	private void SetIsAlive(bool val){
		if(antAnimationController!=null){
			antAnimationController.SetIsAlive(val);
		}
	}

	private void PlayHitAnimation(bool val){
		if(antAnimationController!=null){			
			antAnimationController.PlayHit(val);	
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

	private void PlayAttackAnimation(bool val){
		if(antAnimationController!=null){			
			antAnimationController.PlayAttack(val);	
		}
	}

	private void SetDeathAnimation(int val){
		if(antAnimationController!=null){			
			antAnimationController.setDeathType(val);
		}
	}

	private void SetAttackAnimation(int val){
		if(antAnimationController!=null){			
			antAnimationController.setAttackType(val);
		}
	}

	private void PlayRespawnAnimation(bool val){
		if(antAnimationController!=null){			
			antAnimationController.PlayRespawn(val);
		}
	}

	private void CheckWalk(){
		if(GetState() == BugState.DEAD || GetState() == BugState.HIT || GetState() == BugState.ATTACK){
			return;
		}

		if( GetState() == BugState.WALK){
			if(antTransform!=null){
				MoveDown();
				CheckPositionZ();
			}
		}
	}

	private void MoveDown(){
		Vector3 tempPosition = antTransform.position;
		tempPosition.z -= moveSpeed * Time.deltaTime;
		antTransform.position = tempPosition;
		//Debug.Log( " antTransform.position z " + antTransform.position.z );
	}

	private void CheckPositionZ(){
		if(antTransform.position.z <= maxZ && !isInPool){
			DeActivate(false);
			DispatchAntStatusChange(BugEvent.ESCAPE, antType);
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

	public void Activate(){
		//Debug.Log("Activate ant 1 isInPool " + isInPool);
		if(isInPool){			
			isInPool = false;
			this.gameObject.SetActive(true);

			if(antHitColorController!=null){
				antHitColorController.ResetColor();	
			}

			Respawn();
			Init();

			//Debug.Log("Activate ant 2");
		}
	}

	private void Respawn(){
		float rndX = UnityEngine.Random.Range( minRespawnPositionX, maxRespawnPositionX );
		float rndZ = UnityEngine.Random.Range( minRespawnPositionZ, maxRespawnPositionZ );

		// hack
		if( antType == BugType.Warrior ){
			//rndZ += 100;
		}

		Vector3 tempPosition = antTransform.position;
		tempPosition.x = rndX;
		tempPosition.z = rndZ;
		antTransform.position = tempPosition;

		SetState(BugState.RESPAWN);
		PlayRespawnAnimation(true);
	}

	// animation state events
	private void OnIdleStateChange(AnimationState animationState){
		//Debug.Log("<color=red> OnIdleStateChange </color> " + animationState);

		/*if(animationState == AnimationState.START){
			if(GetState()!=AntState.DEAD && hp > 0){
				SetState(AntState.IDLE);
			}

			PlayRespawnAnimation(false);
		}else if(animationState == AnimationState.END){
			PlayIdleAnimation(false);

			if(GetState()!=AntState.DEAD && GetState()!=AntState.HIT && GetState()!=AntState.ATTACK 
				&& hp > 0){
				SetState(AntState.WALK);
				PlayWalkAnimation(true);	
			}
		}*/
	}

	private void OnWalkStateChange(AnimationState animationState){
		//Debug.Log("<color=blue> OnWalkStateChange </color>" + animationState);

		if(animationState == AnimationState.START){
			/*if(GetState()!=AntState.DEAD && hp > 0){
				SetState(AntState.WALK);
				PlayWalkAnimation(true);	
			}*/

			PlayRespawnAnimation(false);
			/*
			PlayAttackAnimation(false);
			PlayRespawnAnimation(false);*/
		}else if(animationState == AnimationState.END){
			
		}	
	}

	public void OnAttackAnimationStateChange(AnimationState animationState){
		//Debug.Log( " OnAttackAnimationStateChange animationState : " + animationState );

		if(animationState == AnimationState.START){
			
		}else if(animationState == AnimationState.END){
			PlayAttackAnimation(false);
			if(GetState()!=BugState.DEAD && hp > 0){
				SetState(BugState.WALK);
				PlayWalkAnimation(true);	
			}
		}
	}

	private void OnHitStateChange(AnimationState animationState){
		//Debug.Log( " OnHitStateChange animationState : " + animationState );

		if(animationState == AnimationState.START){

		}else if(animationState == AnimationState.END){			
			PlayHitAnimation(false);
			RefreshHit();
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
