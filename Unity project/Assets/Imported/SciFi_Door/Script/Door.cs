using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	private Animator anim;
	public void Start()
	{
		GameObject door = GameObject.FindWithTag("SF_Door");
		anim = door.GetComponent<Animator>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "KeyCard" && enabled)
			anim.SetBool("open", true);

	}
	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.tag == "KeyCard" && enabled)
			anim.SetBool("open", false);
	}
}
