using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// A class from testing that operates the imported door animation
/// using the E and R keys
/// @author Trenton Plager -tlp6760@rit.edu
/// </summary>
public class Door : MonoBehaviour {

    #region Fields
    public Animator anim;           //the animator for the door
    [SerializeField]                
    private AudioSource sliderSound;//the audio source for the slider
    #endregion

    private void Start()
    {
        anim = GetComponent<Animator>(); 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))        //if E is pressed, open the door
        {
            anim.SetTrigger("MakeOpen"); 
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("MakeSliderOpen");  //if R is pressed, open the slider
            sliderSound.Play();
        }
    }
}
