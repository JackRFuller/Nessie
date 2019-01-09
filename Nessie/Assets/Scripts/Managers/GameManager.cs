using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get {return instance;}}

    private NetworkManager networkManager;
    
    public NetworkManager NetworkManager {get{return networkManager;}}

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

        networkManager = GetComponent<NetworkManager>();
    }
}
