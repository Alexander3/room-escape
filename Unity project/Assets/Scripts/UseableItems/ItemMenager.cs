using UnityEngine;
using Scripts;
using Assets;

using System.Collections;
using System.Collections.Generic;

public class ItemMenager : MonoBehaviour
{
    public float MaxSightDistance = 300.0f;
    public float MaxSightAngle = 45.0f;
    public float PullingSpeed = 5f;
    public float EyeShift = 0.5f;
    public GameObject CenterEyeAnchor;
    public GameObject AlternativeCamera;
    public GameObject Player;

    private IControls _controls;
    private List<GameObject> _nearbyUsables;
    private GameObject _seenUsable;
    private GameObject _pickedItem;
    private Vector3 _handPosShift;
    private Quaternion _handRotShift;
    private bool _hasItem = false;

    private void Start()
    {
        if (!CenterEyeAnchor.activeInHierarchy) CenterEyeAnchor = AlternativeCamera;
        _controls = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<IControls>();
        _nearbyUsables = new List<GameObject>();
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Useable>() != null && !_nearbyUsables.Contains(other.gameObject))
        {
            _nearbyUsables.Add(other.gameObject);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Useable>() != null)
        {
            _nearbyUsables.Remove(other.gameObject);
        }
    }
    void LookAhead()
    {
		Vector3 eyeOrigin = CenterEyeAnchor.transform.position;//+ CenterEyeAnchor.transform.forward * EyeShift;
        Vector3 eyeDirection = CenterEyeAnchor.transform.forward * MaxSightDistance;
        GameObject selectedUsable = null;
        float selectedUsableCameraAngle = float.NaN;
        foreach (GameObject usable in _nearbyUsables)
        {
            Vector3 usableDirection = usable.transform.position - eyeOrigin;
            float usableCameraAngle = Vector3.Angle(usableDirection, eyeDirection);
            RaycastHit hit;
            if ((selectedUsable == null && usableCameraAngle < MaxSightAngle || usableCameraAngle < selectedUsableCameraAngle) && Physics.Raycast(eyeOrigin, usableDirection, out hit) && hit.collider.gameObject == usable)
            {
                selectedUsable = usable;
                selectedUsableCameraAngle = usableCameraAngle;
            }
        }
        if (_seenUsable != null && _seenUsable != selectedUsable)
        {
            _seenUsable.GetComponent<Useable>().UnHighlightItem();
            _seenUsable = null;
        }
        if (selectedUsable != null)
        {
            _seenUsable = selectedUsable;
            _seenUsable.GetComponent<Useable>().HighlightItem();

        }
    }
    void UseItem()
    {
        if (_hasItem)
        {
            _pickedItem.GetComponent<Useable>().Use();
        }
        else if (_seenUsable != null)
        {
            if (_seenUsable.GetComponent<Pickable>() != null) PickItem();
            else _seenUsable.GetComponent<Useable>().Use();
        }
    }
    void PickItem()
    {
        _hasItem = true;
        _pickedItem = _seenUsable;
        _pickedItem.GetComponent<Rigidbody>().useGravity = false;
       // Physics.IgnoreCollision(_pickedItem.GetComponent<Collider>(), Player.GetComponent<CharacterController>(), true);
        Pickable pickable = _pickedItem.GetComponent<Pickable>();
        _handPosShift = pickable.handShift;
        _handRotShift = pickable.rotationShift;
        pickable.UnHighlightItem();
        _seenUsable = null;
    }
    void DropItem()
    {
        _pickedItem.GetComponent<Rigidbody>().useGravity = true;
        //Physics.IgnoreCollision(_pickedItem.GetComponent<Collider>(), Player.GetComponent<CharacterController>(), false);
        _pickedItem = null;
        _hasItem = false;
    }
    void MoveItem()
    {
        Vector3 newPosition = Camera.main.transform.position + Camera.main.transform.forward + Camera.main.transform.rotation * _handPosShift;
        _pickedItem.GetComponent<Rigidbody>().velocity = (newPosition - _pickedItem.transform.position) * PullingSpeed;
        /* Dodaj / na poczatku tej linii, aby w czasie trwania gry moc edytowac polozenie trzymanego przedmiotu
        Pickable pickable = _pickedItem.GetComponent<Pickable>();
        _handPosShift = pickable.HandShift;
        _handRotShift = pickable.RotationShift;
        //*/
        _pickedItem.transform.rotation = Camera.main.transform.rotation * _handRotShift;
    }
}
