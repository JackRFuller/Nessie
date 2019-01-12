using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Camera playerCamera;
    private PlayerInput playerInput;

    public Camera PlayerCamera { get {return playerCamera;}}
    public PlayerInput GetPlayerInput { get {return playerInput;}}
   
    void Awake()
    {
        playerCamera = GetComponent<Camera>();
        playerInput = GetComponent<PlayerInput>();
    }

    
}
