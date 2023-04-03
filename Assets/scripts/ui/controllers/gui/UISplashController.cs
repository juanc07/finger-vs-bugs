using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UISplashController : MonoBehaviour {

	public Image logo;

	public float scaleUpDuration;
	public float scaleUpValue;

	public float scaleDownDuration;
	public float scaleDownValue;

	public GameObject uiPanel;
	public GameObject PopUpPanel;
	public GameObject splashScreenPanel;
	public Image splashBG;


	// Use this for initialization
	IEnumerator Start () {
		// Start after one second delay (to ignore Unity hiccups when activating Play mode in Editor)
		yield return new WaitForSeconds(0.5f);
		AnimateScaleUpShow();
	}

	private void AnimateScaleUpShow(){
		Sequence s = DOTween.Sequence();
		s.Append(logo.gameObject.transform.DOScale(scaleUpValue,scaleUpDuration));
		s.Insert(0f,logo.DOFade(1f,scaleUpDuration/2));
		s.Append(logo.gameObject.transform.DOScale(scaleDownValue,scaleDownDuration));
		s.OnComplete(FadeSplash);
		s.Play();
	}

	private void FadeSplash(){

		Sequence s = DOTween.Sequence();
		s.Append(logo.DOFade(0f,1f));
		s.Append(splashBG.DOFade(0f,1f));
		s.OnComplete(AnimationComplete);
		s.Play();

		/*DOTween.To( ()=>{
			return splashBG.color.a;
		},(float val)=>{
			Color currColor = splashBG.color;
			currColor.a = val;
			splashBG.color = currColor;
		},0f,0.5f).OnComplete(AnimationComplete);*/
	}

	private void AnimationComplete(){
		Debug.Log("FadeComplete");
		uiPanel.gameObject.SetActive(true);
		PopUpPanel.gameObject.SetActive(true);
		splashScreenPanel.SetActive(false);
		//SceneManager.LoadScene(1);
	}
}
