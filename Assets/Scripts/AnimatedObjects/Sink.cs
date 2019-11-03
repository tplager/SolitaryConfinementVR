using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for testing that operates the handles on the old sink model
/// @author Trenton Plager -tlp6760@rit.edu
/// </summary>
public class Sink : MonoBehaviour {

    //fields
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) //if T is pressed turn the left handle
        {
            anim.SetTrigger("LeftOn");
        }

        if (Input.GetKeyDown(KeyCode.Y)) //if Y is pressed turn the right handle on
        {
            anim.SetTrigger("RightOn");
        }
    }
}
