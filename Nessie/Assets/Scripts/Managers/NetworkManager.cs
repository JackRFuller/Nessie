using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;
using System;

public class NetworkManager : Photon.MonoBehaviour
{
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

        if(numberOfPlayersInRoom > 1)
        {
            Debug.Log("Server Filled");
            Debug.Log("Player One: " + PhotonNetwork.playerList[0].NickName);
            Debug.Log("Player Two: " + PhotonNetwork.playerList[1].NickName);

            ServerFull();
        }

        PlayerJoinedOrCreatedRoom(numberOfPlayersInRoom);
    }

    public virtual void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        Debug.Log("OnPhotonPlayerConnected");
        if(PhotonNetwork.playerList.Length == 2)
        {            
            Debug.Log("Server Filled");
            Debug.Log("Player One: " + PhotonNetwork.playerList[0].NickName);
            Debug.Log("Player Two: " + PhotonNetwork.playerList[1].NickName);

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
}
