using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Vehicle : MonoBehaviour {

    #region Fields
    public Vector3 vehiclePosition;     //the position of the vehicle
    public Vector3 velocity;            //the velocity of the vehicle
    public Vector3 direction;           //the direction vector of the vehicle
    public Vector3 acceleration;        //the acceleration vector of the vehicle
    public float mass;                  //the mass of the vehicle
    public float maxSpeed;              //the maximum speed of the vehicle
    public float maxForce;              //the maximum force of the vehicle
    public Vector3 ultimateForce;       //the sum of all the forces in the vehicle

    public float frictCoeff;            //a friction coefficient for use in friction calculations

    public Material forwardMaterial;
    public Material rightMaterial;
    public Material futureMaterial;

    public bool showDebugLines;
    #endregion

    // Use this for initialization
    protected void Start()
    {
        vehiclePosition = transform.position;
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        vehiclePosition = transform.position;

        CalcSteeringForces();

        ApplyForce(ultimateForce);

        velocity += acceleration * Time.deltaTime;
        vehiclePosition += velocity * Time.deltaTime;

        direction = velocity.normalized;
        acceleration = Vector3.zero;

        transform.position = vehiclePosition;

        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        vehiclePosition = transform.position;
    }

    /// <summary>
    /// Applies a force to the vehicle
    /// </summary>
    /// <param name="force">The force being applied</param>
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    /// <summary>
    /// Applies friction to the vehicle
    /// </summary>
    /// <param name="coeff">The coefficient of friction</param>
    public Vector3 GenerateFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction = friction * coeff;
        return friction;
    }

    /// <summary>
    /// Seeks a target position
    /// </summary>
    /// <param name="targetPosition">The position of the target to seek</param>
    /// <returns>A force vector to apply to the vehicle to move it towards the target</returns>
    public Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = targetPosition - transform.position;
        desiredVelocity.y = 0; 
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 seekingForce = desiredVelocity - velocity;

        return seekingForce;
    }

    /// <summary>
    /// Seeks a target gameobject
    /// </summary>
    /// <param name="target">The gameobject to seek</param>
    /// <returns>A force vector to apply to the vehicle to move it towards the target</returns>
    public Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }

    /// <summary>
    /// Flees from a target position
    /// </summary>
    /// <param name="targetPosition">The position of the target to flee from</param>
    /// <returns>A force vector to apply to the vehicle to move it away from the target</returns>
    public Vector3 Flee(Vector3 targetPosition)
    {
        Vector3 desiredVelocity = transform.position - targetPosition;
        desiredVelocity.y = 0;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 fleeingForce = desiredVelocity - velocity;

        return fleeingForce;
    }

    /// <summary>
    /// Flees from a target gameobject
    /// </summary>
    /// <param name="target">The gameobject to flee from</param>
    /// <returns>A force vector to apply to the vehicle to move it away from the target</returns>
    public Vector3 Flee(GameObject target)
    {
        return Flee(target.transform.position);
    }

    /// <summary>
    /// Seeks the target's future position
    /// </summary>
    /// <param name="target">The gameobject to pursue</param>
    /// <returns>A force vector to apply to the vehicle to move it towards the target's future position</returns>
    public Vector3 Pursuit(GameObject target)
    {
        Vector3 targetPosition = target.transform.position + target.GetComponent<Vehicle>().velocity * Time.deltaTime;
        return Seek(targetPosition);
    }

    /// <summary>
    /// Flees from the target's future position
    /// </summary>
    /// <param name="target">The gameobject to evade</param>
    /// <returns>A force vector to apply to the vehicle to move it away from the target's future position</returns>
    public Vector3 Evade(GameObject target)
    {
        Vector3 targetPosition = target.transform.position + target.GetComponent<Vehicle>().velocity * Time.deltaTime;
        return Flee(targetPosition);
    }

    /// <summary>
    /// Orients the objects in the direction of their direction vector
    /// </summary>
    public void OrientAgent()
    {
        float rotation = Mathf.Atan2(direction.x, direction.z);
        rotation = (rotation * Mathf.Rad2Deg);

        transform.rotation = Quaternion.Euler(0, rotation, 0);
    }

    /// <summary>
    /// An abstract method to calculate all of the steering forces acting on a vehicle
    /// That are exclusive to that type of vehicle
    /// </summary>
    public abstract void CalcSteeringForces();
}
