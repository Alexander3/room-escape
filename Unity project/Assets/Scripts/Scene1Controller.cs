using UnityEngine;


class Scene1Controller : BaseController
{
    public GameObject RightDoor = null;
    public GameObject LeftDoor = null;

    public void Start()
    {
        Anime.ChainUsed += () =>
        {
            unlock(RightDoor);
            unlock(LeftDoor);
        };

    }
}
