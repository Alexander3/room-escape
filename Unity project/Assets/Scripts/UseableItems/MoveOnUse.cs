using UnityEngine;
using System.Collections;

public class MoveOnUse : Useable
{
    public Vector3 MoveShift;
    public float MoveSpeed = 6f;
	private bool _movingToTarget = false;
    private bool _moving;
    private Vector3 _originPosition;

    private Vector3 _objectSpaceMoveToPosition;

	new public void Start()
    {
		base.Start();
        _originPosition = transform.position;
        _objectSpaceMoveToPosition = _originPosition + MoveShift;
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
            transform.position = Vector3.Lerp(transform.position, _objectSpaceMoveToPosition, MoveSpeed * Time.deltaTime);
        else
            transform.position = Vector3.Lerp(transform.position, _originPosition, MoveSpeed * Time.deltaTime);

        if (transform.position == _originPosition || transform.position == _objectSpaceMoveToPosition)
            _moving = false;
    }
}
