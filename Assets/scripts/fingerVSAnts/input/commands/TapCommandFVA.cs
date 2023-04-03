using UnityEngine;
using System.Collections;

public class TapCommandFVA : ICommandFVA {
	public void Execute(){	
		
	}

	public void Execute(BugController controller){	
		controller.TakeDamage();
	}	
}