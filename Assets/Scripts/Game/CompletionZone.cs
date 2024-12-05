using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CompletionZone : MonoBehaviour
{
     public UnityAction PlayerEnteredCompletionZone;
     public UnityAction PlayerExitedCompletionZone;

    private void OnTriggerEnter(Collider other)
     {
          if(other.CompareTag("Player"))
          {
               PlayerEnteredCompletionZone?.Invoke();
          }
     }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerExitedCompletionZone?.Invoke();
        }
    }
}
