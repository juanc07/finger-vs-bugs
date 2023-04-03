using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;

public class BugHitColorController : MonoBehaviour {

	private BugEventManager antEventManager;

	private Action AntFade;
	public event Action OnAntFade{
		add{ AntFade+=value; }
		remove{ AntFade-=value; }
	}

	public Renderer antRenderer;
	private BugController antController;
	private Color originalColor;
	public float fadeDuration;

	private void Awake(){
		antEventManager = BugEventManager.GetInstance();
		antController = this.gameObject.GetComponent<BugController>();
		//originalColor = GetMainMaterialColor();
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
		
	}

	private void AddEventListener(){
		if(antEventManager!=null){
			antEventManager.OnBugStatusChange+=OnAntStatusChange;
		}
	}

	private void RemoveEventListener(){
		if(antEventManager!=null){
			antEventManager.OnBugStatusChange-=OnAntStatusChange;
		}
	}

	public void ResetColor(){
		//SetMainMaterialColor(originalColor);
	}

	public void Hit(){
		/*if(antRenderer!=null){
			Color baseColor = Color.red;
			DOTween.To(GetColor,SetColor,baseColor,0.85f).OnComplete(OnHitComplete);
		}*/
	}

	public void Fade(){
		if(antRenderer!=null){
			CancelInvoke("OnFadeComplete");
			Invoke("OnFadeComplete",fadeDuration);
			//Color baseColor = Color.clear;
			//DOTween.To(GetMainMaterialColor,SetMainMaterialColor,baseColor,fadeDuration).OnComplete(OnFadeComplete);
		}
	}

	private Color GetColor(){
		if(antRenderer!=null){			
			Material mat = antRenderer.material;
			if(mat!=null){
				return mat.GetColor ("_EmissionColor");
			}else{
				return Color.black;
			}
		}else{
			return Color.black;
		}
	}

	private void SetColor(Color baseColor){
		if(antRenderer!=null){			
			Material mat = antRenderer.material;
			if(mat!=null){
				mat.SetColor ("_EmissionColor", baseColor);	
			}
		}
	}

	private Color GetMainMaterialColor(){
		if(antRenderer!=null){			
			Color mainterialColor= antRenderer.material.color;
			return mainterialColor;
		}else{
			return Color.black;
		}
	}

	private void SetMainMaterialColor(Color color){
		if(antRenderer!=null){			
			antRenderer.material.color = color;
		}
	}

	public void KillTween(){		
		//DOTween.Kill(antRenderer);
	}

	private void OnHitComplete(){
		//SetColor(Color.black);
	}

	private void OnFadeComplete(){
		if(null!=AntFade){
			AntFade();
		}
	}

	private void OnAntStatusChange( BugEvent antEvent, BugController antController){
		// to do powerup if god finger do check id hit every one
		if(this.antController.getID() == antController.getID()){
			if(antEvent == BugEvent.HIT){
				Hit();	
			}else if(antEvent == BugEvent.DECAY){
				Fade();
			}	
		}
	}
}
