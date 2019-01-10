using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UINetworkMainMenu : MonoBehaviour
{
    private NetworkManager networkManager;

    [Header("UI Elements")]
    [SerializeField] private GameObject playerNameInputFieldObj;
    [SerializeField] private TMP_Text playerNameInputFieldText;
    
    [SerializeField] private GameObject connectToRoomButton;
    [SerializeField] private GameObject waitingForOpponentText;

    private void Start() 
    {
        networkManager = GameManager.Instance.NetworkManager;   
         networkManager.PlayerJoinedOrCreatedRoom += HideInitialConnectToServerUI; 
        networkManager.PlayerJoinedOrCreatedRoom += ToggleWaitingForOpponentMessage; 
        networkManager.ServerFull += HideMenu;

        ToggleWaitingForOpponentMessage(0);
    }

    public void OnPlayerNameUpdated(string playerName)
    {
        networkManager.SetClientPlayerName(playerNameInputFieldText.text);
    }
    
    public void OnClickConnectToGame()
    {
        networkManager.JoinOrCreateRoom();
    }

    private void HideInitialConnectToServerUI(int numberOfPlayers)
    {
        playerNameInputFieldObj.SetActive(false);
        connectToRoomButton.SetActive(false);
    }

    private void ToggleWaitingForOpponentMessage(int numberOfPlayers)
    {
        if(numberOfPlayers == 0 || numberOfPlayers == 2)
            waitingForOpponentText.SetActive(false);
        if(numberOfPlayers == 1)
            waitingForOpponentText.SetActive(true);
    }

    private void HideMenu()
    {
        this.gameObject.SetActive(false);
    }

   
}
