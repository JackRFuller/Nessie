using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRangeManager : MonoBehaviour
{
    public bool SpawnInThief;
    public bool SpawnInGuard;

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

       if(SpawnInGuard)
        PhotonNetwork.Instantiate("Guard", new Vector3(2, 1, 0), Quaternion.identity,0);

        if(SpawnInThief)
        PhotonNetwork.Instantiate("Thief", new Vector3(-2, 1, 0), Quaternion.identity,0);
    }
}
