using UnityEngine;
using System.Collections;

public class AsteroidRotation : MonoBehaviour {

	int _xRotationSpeed,_yRotationSpeed,_zRotationSpeed;

	void Start () {
		_xRotationSpeed = Random.Range (-40, 40);
		_yRotationSpeed = Random.Range (-40, 40);
		_zRotationSpeed = Random.Range (-40, 40);
	}

	void Update () {
		transform.Rotate (Vector3.forward * Time.deltaTime * _xRotationSpeed);
		transform.Rotate (Vector3.up * Time.deltaTime * _yRotationSpeed);
		transform.Rotate (Vector3.left * Time.deltaTime * _zRotationSpeed);
	}
}
