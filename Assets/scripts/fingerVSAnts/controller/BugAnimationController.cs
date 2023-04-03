using UnityEngine;
using System.Collections;
using System;

public class BugAnimationController : MonoBehaviour {

	private Animator animator;
	//private AntHitBehavior antHitBehavior;
	private AntWalkBehavior antWalkBehavior;
	//private AntAttackBehavior antAttackBehavior;

	private Action <AnimationState>HitStateChange;
	public event Action <AnimationState>OnHitStateChange{
		add{ HitStateChange+=value; }
		remove{ HitStateChange-=value; }
	}

	private Action <AnimationState>AttackAnimationStateChange;
	public event Action <AnimationState>OnAttackAnimationStateChange{
		add{ AttackAnimationStateChange+=value; }
		remove{ AttackAnimationStateChange-=value; }
	}

	private Action <AnimationState>IdleStateChange;
	public event Action <AnimationState>OnIdleStateChange{
		add{ IdleStateChange+=value; }
		remove{ IdleStateChange-=value; }
	}

	private Action <AnimationState>WalkStateChange;
	public event Action <AnimationState>OnWalkStateChange{
		add{ WalkStateChange+=value; }
		remove{ WalkStateChange-=value; }
	}

	private Action <BehaviorState>WalkStateBehaviorChange;
	public event Action <BehaviorState>OnWalkBehaviorStateChange{
		add{ WalkStateBehaviorChange+=value; }
		remove{ WalkStateBehaviorChange-=value; }
	}

	private Action <BehaviorState>HitStateBehaviorChange;
	public event Action <BehaviorState>OnHitStateBehaviorChange{
		add{ HitStateBehaviorChange+=value; }
		remove{ HitStateBehaviorChange-=value; }
	}

	private Action <BehaviorState>AttackStateBehaviorChange;
	public event Action <BehaviorState>OnAttackStateBehaviorChange{
		add{ AttackStateBehaviorChange+=value; }
		remove{ AttackStateBehaviorChange-=value; }
	}

	// Use this for initialization
	void Awake () {
		animator = this.gameObject.GetComponent<Animator>();
		antWalkBehavior = animator.GetBehaviour<AntWalkBehavior>();
		//antHitBehavior = animator.GetBehaviour<AntHitBehavior>();
		//antAttackBehavior = animator.GetBehaviour<AntAttackBehavior>();
	}

	private void Start(){
		AddEventListener();
	}

	private void OnDestroy(){
		RemoveEventListener();
	}

	private void AddEventListener(){		
		antWalkBehavior.OnStateChange+=onWalkBehaviorStateChange;
		//antHitBehavior.OnStateChange+=onHitBehaviorStateChange;
		//antAttackBehavior.OnStateChange+=onAttackStateChange;
	}

	private void RemoveEventListener(){		
		antWalkBehavior.OnStateChange-=onWalkBehaviorStateChange;
		//antHitBehavior.OnStateChange-=onHitBehaviorStateChange;
		//antAttackBehavior.OnStateChange-=onAttackStateChange;
	}

	public void SetIsAlive(bool val){
		if(animator!=null){
			animator.SetBool("isAlive",val);	
		}
	}

	public void PlayRespawn(bool val){
		if(animator!=null){
			animator.SetBool("isRespawn",val);	
		}
	}

	public void PlayAttack(bool val){
		if(animator!=null){
			animator.SetBool("isAttacking",val);	
		}
	}

	public void setAttackType(int val){
		if(animator!=null){
			animator.SetInteger("attackType",val);	
		}
	}

	public void setDeathType(int val){
		if(animator!=null){
			animator.SetInteger("deathType",val);	
		}
	}

	public void PlayHit(bool val){
		if(animator!=null){
			animator.SetBool("isHit",val);	
		}
	}

	public bool GetIsHit(){
		if(animator!=null){
			return animator.GetBool("isHit");	
		}else{
			return false;
		}
	}

	public void PausedAnimation(){
		if(animator!=null){			
			animator.enabled = false;
		}
	}

	public void ResumeAnimation(){
		if(animator!=null){			
			animator.enabled = true;
		}
	}

	public void PlayWalk(bool val){
		if(animator!=null){
			if(this.gameObject.activeSelf){
				animator.SetBool("isWalking",val);		
			}
		}
	}

	public void PlayIdle(bool val){
		if(animator!=null){
			if(animator.isInitialized){
				animator.SetBool("isIdle",val);		
			}
		}
	}

	// animation events
	public void HitAnimationStart(){
		if(null!=HitStateChange){
			HitStateChange(AnimationState.START);
		}
	}

	public void HitAnimationMid(){
		if(null!=HitStateChange){
			HitStateChange(AnimationState.MID);
		}
	}

	public void HitAnimationEnd(){		
		if(null!=HitStateChange){
			HitStateChange(AnimationState.END);
		}
	}

	public void IdleAnimationStart(){
		if(null!=IdleStateChange){
			IdleStateChange(AnimationState.START);
		}
	}

	public void IdleAnimationMid(){
		if(null!=IdleStateChange){
			IdleStateChange(AnimationState.MID);
		}
	}

	public void IdleAnimationEnd(){
		if(null!=IdleStateChange){
			IdleStateChange(AnimationState.END);
		}
	}

	public void WalkAnimationStart(){
		if(null!=WalkStateChange){
			WalkStateChange(AnimationState.START);
		}
	}

	public void WalkAnimationMid(){
		if(null!=WalkStateChange){
			WalkStateChange(AnimationState.MID);
		}
	}

	public void WalkAnimationEnd(){
		if(null!=WalkStateChange){
			WalkStateChange(AnimationState.END);
		}
	}

	public void AttackAnimationStart(){
		//Debug.Log( " AttackAnimationStart " );
		if(null!=AttackAnimationStateChange){
			AttackAnimationStateChange(AnimationState.START);
		}
	}

	public void AttackAnimationMid(){
		//Debug.Log( " AttackAnimationMid " );
		if(null!=AttackAnimationStateChange){
			AttackAnimationStateChange(AnimationState.MID);
		}
	}

	public void AttackAnimationEnd(){
		//Debug.Log( " AttackAnimationEnd " );
		if(null!=AttackAnimationStateChange){
			AttackAnimationStateChange(AnimationState.END);
		}
	}
	// animation events



	// state behavior events
	private void onWalkBehaviorStateChange(BehaviorState state){
		if(null!=WalkStateBehaviorChange){
			WalkStateBehaviorChange(state);
		}
	}

	private void onHitBehaviorStateChange(BehaviorState state){
		if(null!=HitStateBehaviorChange){
			HitStateBehaviorChange(state);
		}
	}

	private void onAttackStateChange(BehaviorState state){
		//Debug.Log( " AnimationController onAttackStateChange  state: " + state );
		if(null!=AttackStateBehaviorChange){
			AttackStateBehaviorChange(state);
		}
	}
	// state behavior events
}
