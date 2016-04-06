using UnityEngine;
using System.Collections;

public class Win : MonoBehaviour {
	void OnTriggerEnter(Collider other)
	{
		if (other is CharacterController)
			Debug.Log ("You Won");

	}
}
