using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayableCharacter;

public class CharacterInput : CharacterComponent
{
    private Vector3 directionInput;
    private Vector3 directionalRawInput; //Used for Animation

    private bool isInteracting;

    public Vector3 DirectionalInput { get {return directionInput;}}
    public Vector3 DirectionalRawInput { get {return directionalRawInput;}}
    public bool IsInteracting { get {return isInteracting;}}

    private void Update()
    {          
        GetDirectionInput();
        GetInteractInput();
    }  

    private void GetDirectionInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        directionInput = new Vector3(horizontal,0,vertical);

        float horizontalRaw = Input.GetAxisRaw("Horizontal");
        float verticalRaw = Input.GetAxisRaw("Vertical");

        directionalRawInput = new Vector3(horizontalRaw,0,verticalRaw);
    }

    private void GetInteractInput()
    {
        isInteracting = Input.GetKey(KeyCode.E)? true:false; 
    }
}
