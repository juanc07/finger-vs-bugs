using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IDataShare{

	void Init(string apiKey, Action<bool> onComplete);
}
