using UnityEngine;
using System.Collections;

public interface ILogger{
	void Log(string val);
	void Enable();
	void Disable();
}
