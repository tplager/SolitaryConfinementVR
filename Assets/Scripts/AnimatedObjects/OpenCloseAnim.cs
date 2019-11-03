using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// A class for testing that allows for greater modularity by extracting the custom
/// fields operating the animator of an object to the inspector
/// so that the user can attach this script to multiple objects
/// @author Trenton Plager -tlp6760@rit.edu
/// </summary>
public class OpenCloseAnim : MonoBehaviour {

    #region Fields
    public SteamVR_Action_Boolean openClose;    //the boolean value in the action set
    public SteamVR_Input_Sources handType;      //the hand being added to the listener
    public Animator anim;                       //the animator for the object
    public GameObject player;                   //the player 
    public Vector3 correctionVector;            //the vector that must be applied so that the door opens correctly
    public string animationTrigger;             //a customizable field to allow for greater modularity - 
                                                //represents the trigger on the animator
    public int distanceSquaredField;            //a customizable field to allow for greater modularity - 
                                                //represents the distance the player can be from the door to operate the animator
    #endregion

    private void Start()
    {
        openClose.AddOnStateDownListener(TriggerDown, handType);
        //gateOpenClosed.AddOnStateUpListener(TriggerUp, handType);
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
        distanceVector.y = 0; 
        float distanceSquared = distanceVector.sqrMagnitude;
        //Debug.Log(distanceSquared);
        //Debug.Log("Player pos: " + player.transform.position);
        if (distanceSquared <= distanceSquaredField)
        {
            anim.SetTrigger(animationTrigger);
        }
    }
}
