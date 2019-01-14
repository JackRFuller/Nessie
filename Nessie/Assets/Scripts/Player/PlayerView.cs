using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Camera playerCamera;
    private PhotonView photonView;
    private PlayerInput playerInput;

    public Camera PlayerCamera { get {return playerCamera;}}
    public PhotonView GetPhotonView { get {return photonView;}}
    public PlayerInput GetPlayerInput { get {return playerInput;}}
   
    void Awake()
    {
        playerCamera = GetComponent<Camera>();
        photonView = GetComponent<PhotonView>();
        playerInput = GetComponent<PlayerInput>();

        if(photonView.ownerId != PhotonNetwork.player.ID)
        {
            playerCamera.enabled = false;
            playerInput.enabled = false;
        }
    }

    
}
