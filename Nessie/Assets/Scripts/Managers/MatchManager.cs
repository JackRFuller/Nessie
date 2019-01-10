using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
    private void Start() 
    {
        GameManager.Instance.NetworkManager.ServerFull += MatchStarted;
    }

    private void MatchStarted()
    {
        GameManager.Instance.UIManager.UIMatchMenu.MatchMessage.ShowMatchMessage("Match Started");
    }
}
