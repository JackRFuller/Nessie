using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRangeManager : MonoBehaviour
{
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings("Pre-Alpha");
    }

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("ConnectedToMaster");
        JoinOrCreateRoom();
    }

    public virtual void JoinOrCreateRoom()
    {
          RoomOptions roomOptions = new RoomOptions();
          roomOptions.MaxPlayers = 2;
          PhotonNetwork.JoinOrCreateRoom("newRoom",roomOptions,null);      
    }

    public virtual void OnJoinedRoom()
    {
       Debug.Log("OnJoinedRoom");
       PhotonNetwork.Instantiate("Player", new Vector3(0, 12, -13), Quaternion.Euler(new Vector3(45,0,0)), 0);
    }
}
