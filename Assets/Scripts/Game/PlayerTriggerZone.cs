using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerTriggerZone : MonoBehaviour
{
     public UnityAction PlayerEnteredTriggerZone;
     public UnityAction PlayerExitedTriggerZone;

    private void OnTriggerEnter(Collider other)
     {
          if(other.CompareTag("Player"))
          {
               PlayerEnteredTriggerZone?.Invoke();
          }
     }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerExitedTriggerZone?.Invoke();
        }
    }
}
