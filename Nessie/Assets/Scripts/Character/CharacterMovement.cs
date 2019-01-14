using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : CharacterComponent
{
    private Camera playerCamera;
    private NavMeshAgent navMeshAgent;
    private NavMeshPath navMeshPath;    

    [Header("Movement Attributes")]
    [SerializeField] private float maxPathLength = 100;
    private Vector3 pathDestination;
    private bool pathLocked = false;

    private MovementTargetIndicator movementTargetIndicator;

    private MovementState movementState = MovementState.Standing;
    private enum MovementState
    {
        Standing,
        Moving,
    }

    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        playerCamera = characterView.GetPlayerView.PlayerCamera;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshPath = new NavMeshPath();

        //Load in HUD Elements
        GameObject movementIndicator = Instantiate(Resources.Load("MovementTarget"),Vector3.zero,Quaternion.identity) as GameObject;
        movementTargetIndicator = movementIndicator.GetComponent<MovementTargetIndicator>();
        movementState = MovementState.Standing;

        GameManager.Instance.TurnManager.NewTurnStarted += ResetOnNewTurn;
        GameManager.Instance.TurnManager.PlayOutTurn += MoveCharacter;
    }

    private void Update()
    {
        SendOutRayCastFromMousePosition();

        CheckPlayerIsAtDestination();
    } 

    #region Character Movement

    private void MoveCharacter()
    {
        navMeshAgent.destination = pathDestination;
        movementState = MovementState.Moving;
    }

    private void CheckPlayerIsAtDestination()
    {
        if(movementState == MovementState.Moving)
        {
            if(!navMeshAgent.pathPending)
            {
                if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if(!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                         GameManager.Instance.PhotonView.RPC("CharacterAtDestination",PhotonTargets.All);
                         movementState = MovementState.Standing;
                    }
                }
            }
        }
    }

    #endregion

    private void ResetOnNewTurn()
    {
        pathLocked = false;
    }

    #region PathFinding Logic

    private void SendOutRayCastFromMousePosition()
    {        
        if(pathLocked)
            return;
       
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.collider.CompareTag("Ground"))
            {    
                bool validPath = CheckIfPathIsValid(hit.point);
                
                if(validPath)
                {
                    if(Input.GetMouseButton(0))
                    {
                        pathDestination = hit.point;
                        pathLocked = true;
                    }
                }                
                movementTargetIndicator.UpdateMoveTargetIndicator(true,validPath,hit.point);                
            }
            else
            {
                movementTargetIndicator.UpdateMoveTargetIndicator(false,false,hit.point);                
            }
        }
    }

    private bool CheckIfPathIsValid(Vector3 targetPosition)
    {
        NavMesh.CalculatePath(transform.position,targetPosition,NavMesh.AllAreas,navMeshPath);
       
         //Debug
        for(int i = 0; i < navMeshPath.corners.Length - 1; i++)        
            Debug.DrawLine(navMeshPath.corners[i], navMeshPath.corners[i+1],Color.red);
        

        if(navMeshPath.status == NavMeshPathStatus.PathComplete)
        {
            float pathLength = GetPathLength();

            if(pathLength < maxPathLength)
            {
                return true;
            }    
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private float GetPathLength()
    {
        float length = 0;

        for(int i = 1; i < navMeshPath.corners.Length; i++)
        {
            length += Vector3.Distance(navMeshPath.corners[i-1],navMeshPath.corners[i]);
        }

        return length;
    }

    #endregion
}
