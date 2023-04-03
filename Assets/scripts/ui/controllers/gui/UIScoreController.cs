using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIScoreController : MonoBehaviour {

	public Text scoreText;

	public void SetScore(int val){		
		if(scoreText!=null){
			//scoreText.text = string.Format("SCORE {0:000000}",val);
			scoreText.text = string.Format("{0}",val);
		}
	}
}
