using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;


public class DamageEffectController : MonoBehaviour {

	public Image image;
	public float fadeInDuration;
	public float fadeOutDuration;
	public Color targetFadeInColor;
	public Color targetFadeOutColor;


	public bool trigerTest;
	public Tween colorTween;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(trigerTest){
			ActivateDamage();
		}
	}

	// 90 
	public void ActivateDamage(){
		if(trigerTest){
			trigerTest = false;
		}
		KillTween();
		ResetDamage();

		colorTween = image.DOColor(targetFadeInColor,fadeInDuration).SetEase(Ease.Linear).OnComplete(OnHitComplete);
	}

	public void ResetDamage(){		
		Color32 targetColor = new Color32(255,0,0,0);
		image.color = targetColor;
	}

	public void KillTween(){		
		DOTween.Kill(image);
	}

	public void ShowHide(bool val){
		this.gameObject.SetActive(val);
	}

	private void OnHitComplete(){		
		colorTween = image.DOColor(targetFadeOutColor,fadeOutDuration).SetEase(Ease.Linear).OnComplete(OnFadeComplete);
	}

	private void OnFadeComplete(){
		
	}
}
