using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// A class that operates the gate of the outdoor recreation area
/// if the trigger of the VR controller is down
/// For testing
/// @author Trenton Plager -tlp6760@rit.edu
/// </summary>
public class Fence : MonoBehaviour {

    #region Fields
    public SteamVR_Action_Boolean openClose;    //the value in the SteamVR Action set
    public SteamVR_Input_Sources handType;      //the hand that is being added to the listener
    public Animator anim;                       //the gate's animator component
    public GameObject player;                   //the player 
    public Vector3 correctionVector;            //the vector that must be applied so that the door opens correctly
    #endregion

    private void Start()
    {
        openClose.AddOnStateDownListener(TriggerDown, handType);
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

    }

    /// <summary>
    /// A method that runs when the trigger on the VR controller is operated
    /// </summary>
    /// <param name="fromAction">The action being impacted</param>
    /// <param name="fromSource">The source of the action</param>
    public void TriggerDown(SteamVR_Action_Boolean fromAction, 
        SteamVR_Input_Sources fromSource)
    {
        Debug.Log("Trigger is down");

        Vector3 distanceVector = player.transform.position - (transform.position + correctionVector);
        float distanceSquared = distanceVector.sqrMagnitude;
        if (distanceSquared <= 49)
        {
            anim.SetTrigger("MakeOpen");    //setting the animator's parameter so that it operates
        }
    }
}
