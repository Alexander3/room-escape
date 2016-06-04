using UnityEngine;

class BaseController : MonoBehaviour
{
    protected static void unlock(GameObject obj)
    {
        obj.GetComponent<Useable>().CanBeUsed = true;
        Debug.Log(obj.name + " unlocked");
    }
    protected static void lockAgain(GameObject obj)
    {
        obj.GetComponent<Useable>().CanBeUsed = false;
    }

}