using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayableCharacter;

public class CharacterAnimator : CharacterComponent
{
    private Animator characterAnim;

    protected override void Start()
    {
        base.Start();
        characterAnim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {      
        SetCharacterSpeed();
    }

    private void SetCharacterSpeed()
    {
        float speed = Mathf.Abs(characterView.GetCharacterInput.DirectionalRawInput.x) + Mathf.Abs(characterView.GetCharacterInput.DirectionalRawInput.z);       
        characterAnim.SetFloat("speed",speed);
    }
}
