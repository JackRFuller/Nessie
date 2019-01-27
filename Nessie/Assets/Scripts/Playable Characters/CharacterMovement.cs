using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : CharacterComponent
{
    private CharacterController characterController;

    [Header("Movement Attributes")]
    [SerializeField] private float movementSpeed = 1;

    protected override void Start() 
    {
        base.Start();
        
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        MoveCharacter();
    }

    private void MoveCharacter()
    {        
        Vector3 movementDirection = characterView.GetCharacterInput.DirectionalInput;        

        if(movementDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(movementDirection);            
        }
       
        movementDirection.Normalize();
        movementDirection *= movementSpeed; 
        characterController.Move(movementDirection * Time.deltaTime);
    }
}
