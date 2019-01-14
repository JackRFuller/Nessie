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
                SetupAttacker();           
            else
                SetupRunner();                        
        }
        else
        {
            if(PhotonNetwork.player.IsMasterClient) 
                SetupRunner();           
            else
                SetupAttacker();  
        }

        MatchStarted();
    }

    private void SetupAttacker()
    {
        GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(0, 10, 14), Quaternion.Euler(new Vector3(45,180,0)), 0);
        PlayerView playerView = player.GetComponent<PlayerView>();
        GameObject character = PhotonNetwork.Instantiate("Attacker", attackerSpawnPoints[UnityEngine.Random.Range(0,attackerSpawnPoints.Length)].position, Quaternion.identity, 0);
        character.GetComponent<CharacterView>().SetupCharacter(playerView);
    }

    private void SetupRunner()
    {
        GameObject player = PhotonNetwork.Instantiate("Player", new Vector3(0, 12, -13), Quaternion.Euler(new Vector3(45,0,0)), 0);
        PlayerView playerView = player.GetComponent<PlayerView>();
        GameObject character = PhotonNetwork.Instantiate("Runner", runnerSpawnPoints[UnityEngine.Random.Range(0,runnerSpawnPoints.Length)].position, Quaternion.identity, 0);
        character.GetComponent<CharacterView>().SetupCharacter(playerView);
    }


    #endregion
}
