using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayObject : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] protected GameObject interactableCanvas;
    [SerializeField] protected GameObject interactActionObject;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.gameObject.tag = "Interactable";
        TurnOffUI();
    }    

    public virtual void CharacterFoundInteractable()
    {
        TurnOnUI();

        if(!interactActionObject.activeInHierarchy)
            interactActionObject.SetActive(true);

        Debug.Log("Character Found Interactable");
    }

    public virtual void CharacterIsInteractingWithObject()
    {

    }

    public virtual void CharacterNoLongerInteracting()
    {
         if(interactActionObject.activeInHierarchy)
            interactActionObject.SetActive(false);

        Debug.Log("Character No Longer Interacting");
    }

    public virtual void TurnOnUI()
    {
        interactableCanvas.SetActive(true);
    }

    public virtual void TurnOffUI()
    {
        interactableCanvas.SetActive(false);
    }
}
