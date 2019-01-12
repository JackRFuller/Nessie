using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterController : PlayerComponent
{
    private GameObject character;
    private CharacterView characterView;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        character = PhotonNetwork.Instantiate("Character", Vector3.zero, Quaternion.identity, 0);
        characterView = character.GetComponent<CharacterView>();
        characterView.SetupCharacter(playerView);
    }    
}
