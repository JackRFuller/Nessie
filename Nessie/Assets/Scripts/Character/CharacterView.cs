using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private PlayerView playerView;

    public PlayerView GetPlayerView { get {return playerView;}}

    public void SetupCharacter(PlayerView _playerView)
    {
        playerView = _playerView;
    }
}
