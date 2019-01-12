using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementTargetIndicator : MonoBehaviour
{
    private List<Transform> pathCorners;
    private List<Transform> pathLines;

    [Header("HUD Elements")]
    [SerializeField] private GameObject pathCornerSprite;
    [SerializeField] private GameObject pathLineSprite;
    private SpriteRenderer movementIndicatorSprite;

    private void Start() 
    {
        pathCorners = new List<Transform>();
        pathLines = new List<Transform>();

        movementIndicatorSprite = GetComponentInChildren<SpriteRenderer>();
    }

    public void UpdateMoveTargetIndicator(bool isOnGround, bool validPath, Vector3 targetPosition)
    {
        if(!isOnGround)
        {
            movementIndicatorSprite.enabled = false;
            return;
        }
        else
        {
             movementIndicatorSprite.enabled = true;

            //Set Position
            Vector3 newPosition = new Vector3(targetPosition.x,0.05f,targetPosition.z);
            transform.position = newPosition;

            movementIndicatorSprite.color = validPath? Color.white: Color.red;
        }

    }

    

    public void DrawPath(NavMeshPath path)
    {
        for(int i =0; i < path.corners.Length -1; i++)
        {
            if(pathCorners.Count <= i)
            {
                GameObject pathCorner = Instantiate(pathCornerSprite);
                pathCorner.transform.parent = this.transform;
                pathCorners.Add(pathCorner.transform);

                GameObject pathLine = Instantiate(pathLineSprite);
                pathLine.transform.parent = this.transform;
                pathLines.Add(pathLine.transform);
            }

            //Set Corners
            Vector3 pathCornerPosition = new Vector3(path.corners[i].x,0.05f,path.corners[i].z);
            pathCorners[i].position = pathCornerPosition;
            pathCorners[i].gameObject.SetActive(true);

            //Set Path
            //Mid Point
            Vector3 midPointBetweenCorners = (path.corners[i] + path.corners[i+1]) * 0.5f;
            midPointBetweenCorners.y = 0.05f;
            pathLines[i].position = midPointBetweenCorners;

            //Set Size
            float size = Vector3.Distance(path.corners[i], path.corners[i+1]);
            Vector3 newSize = new Vector3(7,1,size * 100);
            pathLines[i].localScale = newSize;

            //Look towards next point                     
            pathLines[i].LookAt(new Vector3(path.corners[i+1].x,0,path.corners[i+1].z));
        }

        //Turn off remaining corners
        int difference = pathCorners.Count - (path.corners.Length);
        for(int j = 0; j < difference; j++)
        {
            if(pathCorners.Count - difference > 0)
            {
                pathCorners[pathCorners.Count - difference].gameObject.SetActive(false);
            }
        }       

        
    }
}
