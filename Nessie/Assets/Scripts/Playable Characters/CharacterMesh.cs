using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMesh : CharacterComponent
{
    private SkinnedMeshRenderer[] meshRenderers;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        if(!characterView.GetPhotonView.isMine)
            TurnOffMeshes();
    }

    private void TurnOffMeshes()
    {
        foreach(SkinnedMeshRenderer meshRenderer in meshRenderers)
        {
            meshRenderer.enabled = false;
        }
    }
}
