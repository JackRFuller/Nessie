using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using System;

public class NetworkManager : Photon.MonoBehaviour
{
    [SerializeField] private int numberOfPlayersRequired = 2;

    public int NumberOfRequiredPlayers {get {return numberOfPlayersRequired;}}
    public event Action<int> PlayerJoinedOrCreatedRoom;
    public event Action ServerFull;

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("Pre-Alpha");
    }

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("ConnectedToMaster");
    }

    public virtual void JoinOrCreateRoom()
    {
          RoomOptions roomOptions = new RoomOptions();
          roomOptions.MaxPlayers = 2;
          PhotonNetwork.JoinOrCreateRoom("newRoom",roomOptions,null);      
    }

    public virtual void OnJoinedRoom()
    {
        Debug.Log("Player Connected To Room");
        int numberOfPlayersInRoom = PhotonNetwork.playerList.Length;

        if(numberOfPlayersInRoom == numberOfPlayersRequired)
        {
            Debug.Log("Server Filled");
            ShowDebugPlayerList();      
            ServerFull();
        }

        PlayerJoinedOrCreatedRoom(numberOfPlayersInRoom);
    }

    public virtual void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("OnPhotonPlayerConnected");
        if(PhotonNetwork.playerList.Length == numberOfPlayersRequired)
        {            
            Debug.Log("Server Filled");
            ShowDebugPlayerList();  
            ServerFull();
        }        

        PlayerJoinedOrCreatedRoom(PhotonNetwork.playerList.Length);
    }

    #region UIMenuFunctions

    public void SetClientPlayerName(string playerName)
    {
        PhotonNetwork.player.NickName = playerName;
    }

    #endregion

    #region DebugItems

    private void ShowDebugPlayerList()
    {
        for(int i =0; i < PhotonNetwork.playerList.Length;i++)
        {
            Debug.Log("Player " + PhotonNetwork.playerList[i].NickName);
        }
    }

    #endregion
}
