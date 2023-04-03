using UnityEngine;
using System.Collections;

public interface ICommand{
	GameObject gameobj{set;get;}
	void Execute();
}
