using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFollower : Vehicle {

    public GameObject[] pathNodes;
    public int currentPathNode;
    public float timeTillMove;
    public float ticker;
    public float sqrDistance;
    public float velocityMag;

    public GameObject[] path;
    public Material pathMaterial;


    // Use this for initialization
    new void Start ()
    {
        sqrDistance = (pathNodes[0].transform.position - transform.position).sqrMagnitude;
        base.Start();
	}
	
	// Update is called once per frame
	new void Update ()
    {
        base.Update();
        velocityMag = velocity.magnitude;
        OrientAgent();

        
	}

    public override void CalcSteeringForces()
    {
        ultimateForce = Vector3.zero;

        sqrDistance = (pathNodes[currentPathNode].transform.position - transform.position).sqrMagnitude;

        if (ticker >= timeTillMove * Time.deltaTime)
        {
            currentPathNode++;
            ticker = 0;
            if (currentPathNode > 3)
            {
                currentPathNode = 0;
            }
        }

        if ((pathNodes[currentPathNode].transform.position-transform.position).sqrMagnitude < 81)
        {
            ultimateForce += GenerateFriction(frictCoeff) * 5 * Mathf.Pow(10,10);
            ticker += Time.deltaTime;
        }
        ultimateForce += (Seek(pathNodes[currentPathNode]) * sqrDistance / 500);

        if (ultimateForce != Vector3.zero)
        {
            ultimateForce = ultimateForce.normalized * maxForce;
        }
    }

    /// <summary>
    /// Displays debug lines for this object
    /// if the showDebugLines field is true
    /// </summary>
    public void OnRenderObject()
    {
        if (showDebugLines == true)
        {
            forwardMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + transform.forward);
            GL.End();

            rightMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + transform.right);
            GL.End();

            futureMaterial.SetPass(0);
            GL.Begin(GL.LINES);
            GL.Vertex(transform.position);
            GL.Vertex(transform.position + velocity.normalized);
            GL.End();

            for (int i = 0; i < path.Length; i++)
            {
                pathMaterial.SetPass(0);
                GL.Begin(GL.LINES);
                GL.Vertex(path[i].transform.position);
                if (i != path.Length - 1)
                {
                    GL.Vertex(path[i + 1].transform.position);
                }
                else
                {
                    GL.Vertex(path[0].transform.position);
                }
                GL.End();
            }
        }
    }
}
