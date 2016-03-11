using UnityEngine;
using System.Collections;

public class LookAtTarget : MonoBehaviour
{

    public Transform Target;

    void Update()
    {
        transform.LookAt(Target);
    }
}
