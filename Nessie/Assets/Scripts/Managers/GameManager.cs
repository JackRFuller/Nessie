using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get {return instance;}}

    private PhotonView photonView;
    private NetworkManager networkManager;
    private MatchManager matchManager;
    private TurnManager turnManager;
    private UIManager uiManager;
    
    public PhotonView PhotonView { get {return photonView;}}
    public NetworkManager NetworkManager {get{return networkManager;}}
    public MatchManager MatchManager { get {return matchManager;}}
    public TurnManager TurnManager { get {return turnManager;}}
    public UIManager UIManager {get {return uiManager;}}

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        photonView = GetComponent<PhotonView>();
        networkManager = GetComponent<NetworkManager>();
        matchManager = GetComponent<MatchManager>();
        turnManager = GetComponent<TurnManager>();
        uiManager = GetComponent<UIManager>();
    }
}
