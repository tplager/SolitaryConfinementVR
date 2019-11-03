using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class for testing that operates the toilet's lid when G is pressed
/// @author Trenton Plager -tlp6760@rit.edu
/// </summary>
public class Toilet : MonoBehaviour
{
    //fields
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G)) //if G is pressed lift or lower the lid
        {
            anim.SetTrigger("ToiletOpen");
        }
    }
}
