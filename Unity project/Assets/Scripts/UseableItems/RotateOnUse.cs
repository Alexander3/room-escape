using UnityEngine;
using System.Collections;

public class RotateOnUse : Useable
{
	public Vector3 Rotation = new Vector3(0,60,0);
	public float MoveSpeed = 6f;
	public bool NonStop = false;
	private bool _movingToTarget = false;
	private bool _moving;
	private Quaternion _originRot;
	private Quaternion _targetRot;

	new public void Start()
	{
		base.Start();
		_originRot = transform.localRotation;
		_targetRot = Quaternion.Euler(_originRot.eulerAngles + Rotation);
	}

	new public void Update()
	{
		base.Update();
		if (_moving)
			Move();
	}

	override protected void doUse()
	{     
		_moving = true;
		_movingToTarget = NonStop || !_movingToTarget;
		if (NonStop)
			_targetRot = Quaternion.Euler(transform.localRotation.eulerAngles + Rotation);
	}      

	private void Move()
	{
		if (_movingToTarget)
			transform.localRotation = Quaternion.Lerp(transform.localRotation, _targetRot ,MoveSpeed * Time.deltaTime);
		else
			transform.localRotation = Quaternion.Lerp(transform.localRotation, _originRot ,MoveSpeed * Time.deltaTime);

		if (transform.rotation == _originRot || transform.rotation == _targetRot)
			_moving = false;
	}
}

