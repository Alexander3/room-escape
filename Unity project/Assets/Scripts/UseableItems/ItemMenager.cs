using UnityEngine;
using Scripts;
using Assets;

using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ItemMenager : MonoBehaviour
{
    public float MaxSightDistance = 3.0f;
    public float PullingSpeed = 5f;
    public GameObject CenterEyeAnchor;

    private IControls _controls;
	private Useable _seenUsable;

	private GameObject _pickedItem;
	private Pickable _currentPickable;
	private bool stopLerpingPickedItem;

    private bool _hasItem = false;


    private void Start()
    {
        _controls = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<IControls>();
    }
    void Update()
    {
        if (_hasItem)
        {
            MoveItem();
            if (_controls.Drop) DropItem();
        }
        else
        {
            LookAhead();
        }
        if (_controls.Use) UseItem();
    }
    
    void LookAhead()
    {
		Useable selectedUsable;
		RaycastHit hit;

		bool hitOccured = Physics.Raycast(CenterEyeAnchor.transform.position, CenterEyeAnchor.transform.forward, out hit, MaxSightDistance);
		selectedUsable = hitOccured ? hit.collider.gameObject.GetComponents<Useable>().FirstOrDefault(x => x.enabled) : null;

		if (selectedUsable != _seenUsable) {
			if (_seenUsable != null) {
				_seenUsable.UnHighlightItem ();
				_seenUsable = null;
			}
			if (selectedUsable != null) {
				_seenUsable = selectedUsable;
				_seenUsable.HighlightItem ();
			}
		}
    }
    void UseItem()
    {
        if (_hasItem)
        {
			_pickedItem.GetComponents<Useable>().First(x => x.enabled).Use();
        }
        else if (_seenUsable != null)
        {
			if (_seenUsable is Pickable) PickItem();
            else _seenUsable.Use();
        }
    }
    void PickItem()
    {
        _hasItem = true;
		_pickedItem = _seenUsable.gameObject;
        _pickedItem.GetComponent<Rigidbody>().useGravity = false;
		_currentPickable = (Pickable)_seenUsable;
		_currentPickable.UnHighlightItem();
        _seenUsable = null;
		stopLerpingPickedItem = false;
    }
    void DropItem()
    {
        _pickedItem.GetComponent<Rigidbody>().useGravity = true;
        _pickedItem = null;
		_currentPickable = null;
        _hasItem = false;
    }
    void MoveItem()
    {
		Quaternion targetRot = Camera.main.transform.rotation * _currentPickable.rotationShift;
		Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward + Camera.main.transform.rotation * _currentPickable.handShift;
        _pickedItem.GetComponent<Rigidbody>().velocity = (newPosition - _pickedItem.transform.position) * PullingSpeed;
		if (!stopLerpingPickedItem) {
            _pickedItem.transform.rotation = Quaternion.Lerp (_pickedItem.transform.rotation, targetRot, Time.deltaTime * _currentPickable.MoveSpeed);
			stopLerpingPickedItem = Quaternion.Angle(_pickedItem.transform.rotation, targetRot) < 1f;
		}else
			_pickedItem.transform.rotation = targetRot;
    }
}
