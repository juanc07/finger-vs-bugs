using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class HPBarController : MonoBehaviour {

	private Action <float>HPBarValueChange;
	public event Action <float>OnHPBarValueChange{
		add{ HPBarValueChange+=value; }
		remove{ HPBarValueChange-=value; }
	}

	public Image hpBar;
	public float value;
	public float maxValue;

	// update the hp bar value 
	public void UpdateValue(int val){
		if(value > 0){
			value+=val;
			UpdateHPBar();
		}
	}

	/// <summary>
	/// for Setting the current value and max value
	/// </summary>
	/// <param name="value">Value.</param>
	/// <param name="maxValue">Max value.</param>
	public void SetValue(int value,int maxValue){		
		this.value=value;
		this.maxValue = maxValue;
		UpdateHPBar();
	}

	/// <summary>
	/// Reset this the hp bar based on set current value and max value
	/// </summary>
	public void Reset(){
		this.value=maxValue;
		UpdateHPBar();
	}

	/// <summary>
	/// Updates the HP bar based on current hp value and hp max value
	/// </summary>
	private void UpdateHPBar(){
		if(hpBar!=null){
			hpBar.fillAmount = value/maxValue;
			if(null!=HPBarValueChange){
				HPBarValueChange(hpBar.fillAmount);
			}
		}
	}
}
