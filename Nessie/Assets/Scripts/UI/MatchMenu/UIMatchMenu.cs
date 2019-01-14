using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMatchMenu : MonoBehaviour
{
    [SerializeField] private UIMatchMessage matchMessage;
    [SerializeField] private GameObject matchUIObjects;
    
    public UIMatchMessage MatchMessage { get {return matchMessage;}}

    private void Start() 
    {
        //matchUIObjects.SetActive(false);
        GameManager.Instance.NetworkManager.ServerFull += TurnOnMatchUI;
    }

    private void TurnOnMatchUI()
    {
         matchUIObjects.SetActive(true);
    }
    
}
