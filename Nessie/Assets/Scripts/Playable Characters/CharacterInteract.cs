using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteract : CharacterComponent
{
    private Transform interactableTransform;
    private GameplayObject interactable;

    private void Update()
    {
        InteractWithObject();
    }

    private void InteractWithObject()
    {
        Ray ray = new Ray(new Vector3(transform.position.x,transform.position.y * 0.5f,transform.position.z),transform.forward);
        Debug.DrawRay(ray.origin,ray.direction,Color.red,1);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 2))
        {
            if(hit.collider.CompareTag("Interactable"))
            {
                if(hit.transform == null || hit.transform != interactableTransform)
                {
                    AddInteractableToCache(hit);
                    Debug.Log("Added Interactable");
                }

                interactable.CharacterFoundInteractable();

                if(characterView.GetCharacterInput.IsInteracting)
                {
                             
                }                
            }
            else
            {
                if(interactableTransform != null)
                {
                    RemoveInteractableFromCache();
                }
            }
        }
        else
        {
            if(interactableTransform != null)
            {
                RemoveInteractableFromCache();
            }
        }
    }

    private void AddInteractableToCache(RaycastHit hit)
    {
        interactableTransform = hit.transform;
        interactable = interactableTransform.GetComponent<GameplayObject>();
    }

    private void RemoveInteractableFromCache()
    {
        interactable.CharacterNoLongerInteracting();
        interactable = null;
        interactableTransform = null;
    }
}
