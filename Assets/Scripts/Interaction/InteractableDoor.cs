using UnityEngine;

// TODO: might want to have multiple highlighted objects. 
// Can make lists of default materials if need be
public class InteractableDoor : MonoBehaviour, IInteractable
{
     public Material DefaultMat;
     public Material HighlightMat;
     public GameObject HighlightedObject;
     public bool isLocked = false;
     [SerializeField] private Animator animator;

     public void Highlight()
     {
          if(isLocked) return;
          HighlightedObject.GetComponent<Renderer>().material = HighlightMat;
     }

     public void ForceInteract()
     {
          animator.SetTrigger("Interacted");
     }

     public void Interact()
     {
          if (isLocked) return;
          animator.SetTrigger("Interacted");
     }

     public void UnHighlight()
     {
          if (isLocked) return;
          HighlightedObject.GetComponent<Renderer>().material = DefaultMat;
     }

     // Start is called once before the first execution of Update after the MonoBehaviour is created
     void Start()
    {
          if(animator == null) { animator = GetComponent<Animator>(); }
          
          // if Object has a default mat, use that. Otherwise it will use the default mat set in the editor
        if(HighlightedObject.GetComponent<Renderer>().material != null)
          {
               DefaultMat = HighlightedObject.GetComponent<Renderer>().material;
          }
    }
}
