using UnityEngine;
using System.Collections;

public interface ICommandFVA{	
	void Execute();
	void Execute(BugController controller);
}

