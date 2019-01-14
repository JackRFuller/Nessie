using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterView : MonoBehaviour
{
    public event Action CharacterViewSetup;

    private PlayerView playerView;
    private CharacterMovement characterMovement;

    public PlayerView GetPlayerView { get {return playerView;}}
    
    private void Awake() 
    {
       
    }

    public void SetupCharacter(PlayerView _playerView)
    {
        playerView = _playerView;
         characterMovement = this.gameObject.AddComponent<CharacterMovement>();

        
        if(CharacterViewSetup != null)
            CharacterViewSetup();
    }
}
