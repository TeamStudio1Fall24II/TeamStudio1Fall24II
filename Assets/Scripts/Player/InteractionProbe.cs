using UnityEngine;

public class InteractionProbe : MonoBehaviour
{
     public Camera playerCamera;

     public float RaycastInterval = 0.1f;
     public float lookDistance = 2.0f;
     public LayerMask interactableLayer;

     private bool isPointingAtInteractable = false;
     private float raycastTimer = 0.0f;

     private IInteractable currentInteractable;

    // Update is called once per frame
    void Update()
    {
          // timer
          raycastTimer += Time.deltaTime;
          if(raycastTimer < RaycastInterval) { return; }
          raycastTimer = 0.0f;

          Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
          RaycastHit hit;

          Debug.DrawLine(ray.origin, ray.origin + ray.direction * lookDistance, Color.yellow);

          // pointing at air
          if (!Physics.Raycast(ray, out hit, lookDistance, interactableLayer))
          {
               if (currentInteractable != null)
               {
                    // clear last object
                    currentInteractable?.UnHighlight();
                    currentInteractable = null;
                    isPointingAtInteractable = false;
               }
               return;
          }

          IInteractable pointingAt = hit.collider.GetComponent<IInteractable>();
          // pointing at non-interactable
          if(pointingAt == null)
          {
               if(currentInteractable != null)
               {
                    // clear last object
                    currentInteractable?.UnHighlight();
                    currentInteractable = null;
                    isPointingAtInteractable = false;
               }
               return;
          }
               
          // Pointing at something new
          if(pointingAt != currentInteractable)
          {
               // clear last object
               currentInteractable?.UnHighlight();

               currentInteractable = pointingAt;
               currentInteractable.Highlight();
               isPointingAtInteractable = true;
          }

          // pointing at same interactable, don't change anything
    }

     public void Interact()
     {
          if(isPointingAtInteractable)
          {
               currentInteractable?.Interact();
          }
     }
}
