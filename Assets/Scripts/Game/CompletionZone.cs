using UnityEngine;
using UnityEngine.Events;

public class CompletionZone : MonoBehaviour
{
     public UnityAction PlayerEnteredCompletionZone;

     private void OnTriggerEnter(Collider other)
     {
          if(other.CompareTag("Player"))
          {
               PlayerEnteredCompletionZone?.Invoke();
               //OpenVictoryMenu();
          }
     }
}
