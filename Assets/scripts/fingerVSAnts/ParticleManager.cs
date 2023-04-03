using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour {
	
	private Dictionary<int,List<GameObject>> instantiatedObjects = new Dictionary<int, List<GameObject>>();
	public GameObject hitParticle;
	public int initialCount;
	public bool isPrewarm;

	// Use this for initialization
	void Start () {
		addObjectToPool( hitParticle, initialCount );

		if(isPrewarm){
			Prewarm();
		}

		//Invoke("Test", 2f);	
		//InvokeRepeating("Test",0.3f,0.5f);
	}

	private void Prewarm(){
		for(int i = 0; i < initialCount; i++){
			GameObject hitParticle = ShowParticle(ParticleType.HIT);
			hitParticle.transform.position = new Vector3(0f,0f,300f);
		}
	}

	private void Test(){
		GetParticle(hitParticle);

		/*for(int i = 0; i < 100; i++)
		{
			if(GetParticle(hitParticle) != null){
				Debug.Log(" GetParticle sucess");
			}else{
				Debug.Log(" GetParticle fail");
			}

		}*/
	}
	
	private void addObjectToPool(GameObject sourceObject, int number)
	{
		int uniqueId = sourceObject.GetInstanceID();

		//Add new entry if it doesn't exist
		if(!instantiatedObjects.ContainsKey(uniqueId)){
			instantiatedObjects.Add(uniqueId, new List<GameObject>());
		}

		//Add the new objects
		GameObject newObj;
		for(int i = 0; i < number; i++)
		{
			newObj = (GameObject)Instantiate(sourceObject);
			newObj.transform.position= new Vector3(Random.Range(-15f,15f),0,Random.Range(-30f,30f));
			newObj.SetActive(false);

			instantiatedObjects[uniqueId].Add(newObj);
			newObj.transform.parent = this.transform;
		}
	}

	public GameObject ShowParticle(ParticleType particleType){
		if(particleType == ParticleType.HIT){
			return GetParticle(hitParticle);
		}

		return null;
	}

	private GameObject GetParticle(GameObject sourceObject){
		//found object
		GameObject foundObject = null;

		int uniqueId = sourceObject.GetInstanceID();
		if(!instantiatedObjects.ContainsKey(uniqueId)){
			return foundObject;
		}


		int count = instantiatedObjects[uniqueId].Count;
		for(int i = 0; i < count; i++)
		{
			if(!instantiatedObjects[uniqueId][i].activeSelf){
				foundObject =  instantiatedObjects[uniqueId][i];
				foundObject.SetActive(true);
				break;
			}
		}



		return foundObject;
	}
}
