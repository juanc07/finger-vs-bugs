using UnityEngine;
using System.Collections;
using System;

public class AntHitBehavior : StateMachineBehaviour {

	private Action <BehaviorState>StateChange;
	public event Action <BehaviorState>OnStateChange{
		add{ StateChange+=value; }
		remove{ StateChange-=value; }
	}

	private BehaviorState behaviorState;

	public override void OnStateMachineExit (Animator animator, int stateMachinePathHash)
	{
		base.OnStateMachineExit (animator, stateMachinePathHash);
		//Debug.Log(" OnStateMachineExit animator " + animator.name);
	}

	public override void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateEnter (animator, stateInfo, layerIndex);
		behaviorState = BehaviorState.ENTER;
		//Debug.Log(" OnStateEnter animator " + animator.name + " stateInfo " + stateInfo.normalizedTime + " layerIndex " + layerIndex);
		if(null!=StateChange){
			StateChange(behaviorState);
		}
	}

	public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateExit (animator, stateInfo, layerIndex);
		behaviorState = BehaviorState.EXIT;
		//Debug.Log(" OnStateExit animator " + animator.name + " stateInfo " + stateInfo.length + " layerIndex " + layerIndex);
		if(null!=StateChange){
			StateChange(behaviorState);
		}
	}

	public override void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		base.OnStateUpdate (animator, stateInfo, layerIndex);
		behaviorState = BehaviorState.UPDATE;
		if(null!=StateChange){
			StateChange(behaviorState);
		}
		//Debug.Log(" OnStateUpdate animator " + animator.name + " stateInfo " + stateInfo.length + " layerIndex " + layerIndex);
	}
}


