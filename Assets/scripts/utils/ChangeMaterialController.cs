using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialController : MonoBehaviour {

	public Renderer targetRenderer;
	public Material[] mats;

	
	public void RandomChangeMaterial(){
		int rnd = Random.Range(0,mats.Length);
		targetRenderer.material = mats[rnd];
	}
}
