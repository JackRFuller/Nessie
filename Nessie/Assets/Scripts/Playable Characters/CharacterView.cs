using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayableCharacter
{
    public class CharacterView : MonoBehaviour
    {
        private PhotonView photonView;
        private CharacterInput characterInput;
        private CharacterMovement characterMovement;

        public PhotonView GetPhotonView { get {return photonView;}}
        public CharacterInput GetCharacterInput { get {return characterInput;}}
        public CharacterMovement GetCharacterMovement { get {return characterMovement;}}

        private void Awake() 
        {
            photonView = GetComponent<PhotonView>();
            characterInput = GetComponent<CharacterInput>();
            characterMovement = GetComponent<CharacterMovement>();
        }
    }
}


