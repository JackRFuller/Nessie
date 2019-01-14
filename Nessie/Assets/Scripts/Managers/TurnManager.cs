using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    public event Action NewTurnStarted;
    public event Action PlayOutTurn;
    public event Action CalculateTurnResult;

    private float turnTimer;
    [SerializeField] private float maxTurnTimerLength = 60;

    private int turnCount = 0;
    private int numberOfPlayersWhoHaveLockedDownTheirTurn;
    private int numOfCharactersAtDestination;

    public float TurnTimer {get {return turnTimer;}}

    private TurnState turnState;
    private enum TurnState
    {
        Running,
        Paused,
    }

    private void Start() 
    {
        GameManager.Instance.MatchManager.MatchStarted += StartNewTurn;
        turnState = TurnState.Paused;
    }

    private void StartNewTurn()
    {
        string matchMessage = turnCount == 0? "Match Started": "New Turn";
        GameManager.Instance.UIManager.UIMatchMenu.MatchMessage.ShowMatchMessage(matchMessage);

        turnTimer = maxTurnTimerLength;
        turnState = TurnState.Running;

        numberOfPlayersWhoHaveLockedDownTheirTurn = 0;
        numOfCharactersAtDestination = 0;
        turnCount += 1;
        
        NewTurnStarted();
    }
    
    private void Update()
    {
        RunTurnTimer();
    }

    private void RunTurnTimer()
    {
        if(turnState == TurnState.Paused)
            return;

        turnTimer -= Time.deltaTime;

        if(turnTimer <= 0)
        {
            Debug.Log("Turn Over");
        }
    }

    //Called from UIConfirmTurn
    [PunRPC]
    private void PlayerLockedTurn()
    {
        numberOfPlayersWhoHaveLockedDownTheirTurn++;

        if(numberOfPlayersWhoHaveLockedDownTheirTurn == GameManager.Instance.NetworkManager.NumberOfRequiredPlayers)
        {
            PlayOutTurn();
            turnState = TurnState.Paused;
           
        }
    }

    //Called From CharacterMovement
    [PunRPC]
    private void CharacterAtDestination()
    {
        numOfCharactersAtDestination++;
        if(numOfCharactersAtDestination == GameManager.Instance.NetworkManager.NumberOfRequiredPlayers)
        {
            Debug.Log("Characters At Destination");
            CalculateTurnResult();
        }
    }

    [PunRPC]
    private void AttackerFailedToFindRunner()
    {
        Debug.Log("No Runner Found");
        StartNewTurn();
    }


}
