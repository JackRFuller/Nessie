using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    protected PlayerView playerView;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        playerView = GetComponent<PlayerView>();
    }   
}
