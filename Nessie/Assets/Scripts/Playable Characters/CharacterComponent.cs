using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayableCharacter;
using Photon;

public class CharacterComponent : Photon.MonoBehaviour
{
    protected CharacterView characterView;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        characterView = GetComponent<CharacterView>();

        if(!characterView.GetPhotonView.isMine)
            this.enabled = false;
    }  
}
