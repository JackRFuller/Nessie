using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get {return instance;}}

    private NetworkManager networkManager;
    private UIManager uiManager;
    
    public NetworkManager NetworkManager {get{return networkManager;}}
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

        networkManager = GetComponent<NetworkManager>();
        uiManager = GetComponent<UIManager>();
    }
}
