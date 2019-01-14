﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFieldOfView : CharacterComponent
{
    [Header("Field of View Attributes")]
    [SerializeField] private float viewRadius;
    [Range(0,360)]
    [SerializeField] private float viewAngle;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private LayerMask obstacleMask;

    [Header("Field of View Visualisation Attributes")]
    [SerializeField] private int meshResolution;
    [SerializeField] private MeshFilter viewMeshFilter;
    [SerializeField] private int edgeResolveIterations;
	[SerializeField] private float edgeDstThreshold;
    private Mesh viewMesh;

    public float ViewRadius { get {return viewRadius;}}
    public float ViewAngle { get {return viewAngle;}}

    protected override void Start()
    {
        base.Start();

        GameManager.Instance.TurnManager.CalculateTurnResult += FindVisibleTargets;

        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
    }

    private void LateUpdate() 
    {
        DrawFieldOfView();
    }

    private void FindVisibleTargets()
    {
        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position,viewRadius,targetMask);

        bool foundRunner = false;

        for(int i = 0; i < targetsInViewRadius.Length;i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            if(Vector3.Angle(transform.forward,dirToTarget) < viewAngle * 0.5f)
            {
                float distToTarget = Vector3.Distance(transform.position,target.position);

                if(!Physics.Raycast(transform.position,dirToTarget,distToTarget,obstacleMask))
                {
                    Debug.Log("FOUND RUNNER!");
                    foundRunner = true;
                }
            }
        }

        if(!foundRunner)
        {
            GameManager.Instance.PhotonView.RPC("AttackerFailedToFindRunner",PhotonTargets.All);
        }
    }

    void DrawFieldOfView()
    {
       int stepCount = Mathf.RoundToInt(viewAngle * meshResolution);
		float stepAngleSize = viewAngle / stepCount;
		List<Vector3> viewPoints = new List<Vector3> ();
		ViewCastInfo oldViewCast = new ViewCastInfo ();
		for (int i = 0; i <= stepCount; i++) {
			float angle = transform.eulerAngles.y - viewAngle / 2 + stepAngleSize * i;
			ViewCastInfo newViewCast = ViewCast (angle);

			if (i > 0) {
				bool edgeDstThresholdExceeded = Mathf.Abs (oldViewCast.distance - newViewCast.distance) > edgeDstThreshold;
				if (oldViewCast.hit != newViewCast.hit || (oldViewCast.hit && newViewCast.hit && edgeDstThresholdExceeded)) {
					EdgeInfo edge = FindEdge (oldViewCast, newViewCast);
					if (edge.pointA != Vector3.zero) {
						viewPoints.Add (edge.pointA);
					}
					if (edge.pointB != Vector3.zero) {
						viewPoints.Add (edge.pointB);
					}
				}
			}

			viewPoints.Add (newViewCast.point);
			oldViewCast = newViewCast;
		}

		int vertexCount = viewPoints.Count + 1;
		Vector3[] vertices = new Vector3[vertexCount];
		int[] triangles = new int[(vertexCount-2) * 3];

		vertices [0] = Vector3.zero;
		for (int i = 0; i < vertexCount - 1; i++) {
			vertices [i + 1] = transform.InverseTransformPoint(viewPoints [i]);

			if (i < vertexCount - 2) {
				triangles [i * 3] = 0;
				triangles [i * 3 + 1] = i + 1;
				triangles [i * 3 + 2] = i + 2;
			}
		}

		viewMesh.Clear ();

		viewMesh.vertices = vertices;
		viewMesh.triangles = triangles;
		viewMesh.RecalculateNormals ();
	}
    
	private EdgeInfo FindEdge(ViewCastInfo minViewCast, ViewCastInfo maxViewCast) {
		float minAngle = minViewCast.angle;
		float maxAngle = maxViewCast.angle;
		Vector3 minPoint = Vector3.zero;
		Vector3 maxPoint = Vector3.zero;

		for (int i = 0; i < edgeResolveIterations; i++) {
			float angle = (minAngle + maxAngle) / 2;
			ViewCastInfo newViewCast = ViewCast (angle);

			bool edgeDstThresholdExceeded = Mathf.Abs (minViewCast.distance - newViewCast.distance) > edgeDstThreshold;
			if (newViewCast.hit == minViewCast.hit && !edgeDstThresholdExceeded) {
				minAngle = angle;
				minPoint = newViewCast.point;
			} else {
				maxAngle = angle;
				maxPoint = newViewCast.point;
			}
		}

		return new EdgeInfo (minPoint, maxPoint);
	}

    private ViewCastInfo ViewCast(float globalAngle)
    {
        Vector3 direction = DirFromAngle(globalAngle,true);
        RaycastHit hit;

        if(Physics.Raycast(transform.position,direction,out hit, viewRadius,obstacleMask))
        {
            return new ViewCastInfo(true,hit.point,hit.distance,globalAngle);
        }
        else
        {
            return new ViewCastInfo(false,transform.position + direction * viewRadius,viewRadius,globalAngle);
        }
    }



    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad),0,Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector3 point;
        public float distance;
        public float angle;

        public ViewCastInfo(bool _hit, Vector3 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }

    public struct EdgeInfo {
		public Vector3 pointA;
		public Vector3 pointB;

		public EdgeInfo(Vector3 _pointA, Vector3 _pointB) {
			pointA = _pointA;
			pointB = _pointB;
		}
	}

}
