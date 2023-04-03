using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIComboController : MonoBehaviour {

	public Text comboText;
	private Sequence s;

	public void SetCombo(int val){
		if(comboText!=null){
			comboText.text = string.Format("COMBO\n x {0}",val);
			ResetProperties();
			AnimateScaleUp();
		}
	}

	private void ResetProperties(){
		//DOTween.Complete(comboText.color.a);
		DOTween.Kill(comboText.color.a);

		//DOTween.Complete(comboText.gameObject.transform.localScale);
		DOTween.Kill(comboText.gameObject.transform.localScale);

		comboText.gameObject.transform.localScale = new Vector3(1f,1f,1f);

		Color currColor = comboText.color;
		currColor.a = 1f;
		comboText.color = currColor;
	}

	private void AnimateScaleUp(){
		DOTween.To( ()=>{
			return comboText.gameObject.transform.localScale;
		},(Vector3 val)=>{
			comboText.gameObject.transform.localScale = val;
		},new Vector3(1.8f,1.8f,1.8f),0.1f ).OnComplete(AnimateScaleDown);
	}

	private void AnimateScaleDown(){		
		DOTween.To( ()=>{
			return comboText.gameObject.transform.localScale;
		},(Vector3 val)=>{
			comboText.gameObject.transform.localScale = val;
		},new Vector3(1f,1f,1f),0.1f ).OnComplete(FadeText);
	}

	private void FadeText(){
		CancelInvoke("DelayFade");
		Invoke("DelayFade",2f);
	}

	private void DelayFade(){
		DOTween.To( ()=>{
			return comboText.color.a;
		},(float val)=>{
			Color currColor = comboText.color;
			currColor.a = val;
			comboText.color = currColor;
		},0f,0.5f).OnComplete(FadeComplete);
	}

	private void FadeComplete(){
		Debug.Log("FadeComplete");
	}

	public void OnDisable(){
		DOTween.Complete(comboText.color.a);
		DOTween.Kill(comboText.color.a);
		DOTween.Complete(comboText.gameObject.transform.localScale);
		DOTween.Kill(comboText.gameObject.transform.localScale);
	}
}
