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
    [SerializeField] private float maxPathLength = 10;

    [Header("HUD Elements")]
    [SerializeField] private GameObject movementTargetIndicatorPrefab;
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
        GameObject movementIndicator = Instantiate(movementTargetIndicatorPrefab,Vector3.zero,Quaternion.identity);
        movementTargetIndicator = movementIndicator.GetComponent<MovementTargetIndicator>();
    }   

    private void Update()
    {
        SendOutRayCastFromMousePosition();
    } 

    private void SendOutRayCastFromMousePosition()
    {
        if(movementState != MovementState.Standing)
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
                        navMeshAgent.destination = hit.point;
                        movementState = MovementState.Moving;
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
        //movementTargetIndicator.DrawPath(navMeshPath);
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
}
