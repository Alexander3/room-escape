using UnityEngine;
using System.Collections;

public class DropOnUse : Useable
{

	override protected void doUse()
	{     
		Rigidbody rg = GetComponent<Rigidbody> ();
		rg.isKinematic = false;
		rg.useGravity = true;
		GetComponent<Pickable>().enabled = true;
		enabled=false;
	}
}

