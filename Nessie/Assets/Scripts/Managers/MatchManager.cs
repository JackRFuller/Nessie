using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private Transform[] runnerSpawnPoints;
    [SerializeField] private Transform[] attackerSpawnPoints;

    public event Action MatchStarted;

    private void Start() 
    {
        GameManager.Instance.NetworkManager.ServerFull += SetupMatch;
    }

    private void SetupMatch()
    {
        //Work out which player is in what role
        if(PhotonNetwork.player.IsMasterClient)
        {
            int attackerOrRunner = UnityEngine.Random.Range(0,1);
            GameManager.Instance.PhotonView.RPC("DetermineWhoIsAttackerAndWhoIsRunner",PhotonTargets.All,attackerOrRunner);
        }
    }

    #region Match Setup

    [PunRPC]
    private void DetermineWhoIsAttackerAndWhoIsRunner(int playerType)
    {
        if(playerType == 0)
        {
            if(PhotonNetwork.player.IsMasterClient) 
                SetupGuard();           
            else
                SetupThief();                        
        }
        else
        {
            if(PhotonNetwork.player.IsMasterClient) 
                SetupGuard();           
            else
                SetupThief();  
        }

        MatchStarted();
    }

    private void SetupGuard()
    {        
        GameObject character = PhotonNetwork.Instantiate("Guard", attackerSpawnPoints[UnityEngine.Random.Range(0,attackerSpawnPoints.Length)].position, Quaternion.identity, 0);
        //character.GetComponent<CharacterView>().SetupCharacter(playerView);
    }

    private void SetupThief()
    {        
        GameObject character = PhotonNetwork.Instantiate("Thief", runnerSpawnPoints[UnityEngine.Random.Range(0,runnerSpawnPoints.Length)].position, Quaternion.identity, 0);
        //character.GetComponent<CharacterView>().SetupCharacter(playerView);
    }


    #endregion
}
