using UnityEngine;

public class InteractableFinalObject : MonoBehaviour, IInteractable
{
     public Material DefaultMat;
     public Material HighlightMat;
     public GameObject HighlightedObject;
     public LevelManager levelManager;
     public bool isLocked => !levelManager.isEnemiesCleared;

     public void Highlight()
     {
          if (isLocked) return;
          HighlightedObject.GetComponent<Renderer>().material = HighlightMat;
     }

     public void Interact()
     {
          if (isLocked) return;
          levelManager.OnCompletionZone();
     }

     public void UnHighlight()
     {
          if (isLocked) return;
          HighlightedObject.GetComponent<Renderer>().material = DefaultMat;
     }

     private void Start()
     {
          if (HighlightedObject.GetComponent<Renderer>().material != null)
          {
               DefaultMat = HighlightedObject.GetComponent<Renderer>().material;
          }
     }

}
