using UnityEngine;
using System.Collections;

public class FlipOnUse : Useable
{
	public Vector3 Rotation = new Vector3(0,60,0);
	public float MoveSpeed = 6f;
	private bool _movingToTarget = false;
	private bool _moving;
	private Quaternion _originRot;
	private Quaternion _targetRot;

	new public void Start()
	{
		base.Start();
		_originRot = transform.rotation;
		_targetRot = new Quaternion ();
		_targetRot.eulerAngles = (_originRot.eulerAngles + Rotation);
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
		_movingToTarget = !_movingToTarget;
	}      

	private void Move()
	{
		if (_movingToTarget)
			transform.rotation = Quaternion.Lerp(transform.rotation, _targetRot ,MoveSpeed * Time.deltaTime);
		else
			transform.rotation = Quaternion.Lerp(transform.rotation, _originRot ,MoveSpeed * Time.deltaTime);

		if (transform.rotation == _originRot || transform.rotation == _targetRot)
			_moving = false;
	}
}

