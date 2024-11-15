using UnityEngine;

public class SFXDoor : MonoBehaviour
{
    [SerializeField] SFXMaker Open = null;
    [SerializeField] SFXMaker Close = null;

    public void OpenDoor()
    {
        if(Open != null)
        {
            Open.MakeSound();
        }
    }

    public void CloseDoor()
    {
        if (Close != null)
        {
            Close.MakeSound();
        }

    }
}
