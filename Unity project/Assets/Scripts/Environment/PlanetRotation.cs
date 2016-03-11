using UnityEngine;
using System.Collections;

public class PlanetRotation : MonoBehaviour {
	
	int _speed;
	
	void Start () {
		_speed = Random.Range (2,6);
	}
	void Update () {
		transform.Rotate(Vector3.forward*Time.deltaTime*_speed);
	}
}
