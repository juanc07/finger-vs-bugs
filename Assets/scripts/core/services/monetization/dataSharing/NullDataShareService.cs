using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NullDataShareService : IDataShare {
	private const string TAG = "[NullDataShareService]";

	public void Init(string apiKey, Action<bool> onComplete){
		Debug.Log(TAG + "using null Data Share Service!");
	}
}
