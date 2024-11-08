using UnityEngine;

public class InteractableDoor : MonoBehaviour, IInteractable
{
     public Material DefaultMat;
     public Material HighlightMat;
     public GameObject HighlightedObject;
     private Animator animator;

     public void Highlight()
     {
          HighlightedObject.GetComponent<Renderer>().material = HighlightMat;
     }

     public void Interact()
     {
          animator.SetTrigger("Interacted");
     }

     public void UnHighlight()
     {
          HighlightedObject.GetComponent<Renderer>().material = DefaultMat;
     }

     // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
    {
          animator = GetComponent<Animator>();
          // if Object has a default mat, use that. Otherwise it will use the default mat set in the editor
        if(HighlightedObject.GetComponent<Renderer>().material != null)
          {
               DefaultMat = HighlightedObject.GetComponent<Renderer>().material;
          }
    }
}
