using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script that controls the rotation of the sun
/// </summary>
public class LightController : MonoBehaviour {

    #region Fields
    public float DayLength;
    private float rotationSpeed;

    private GameObject timeManager;
    #endregion

    // Use this for initialization
    void Start () {
        timeManager = GameObject.Find("TimeManager");
        Quaternion sunRotation = timeManager.GetComponent<SolitaryDataManager>().SunRotation;
        gameObject.transform.rotation = sunRotation;
	}
	
	// Update is called once per frame
	void Update () {
        rotationSpeed = Time.deltaTime / DayLength;
        transform.Rotate(0, rotationSpeed, 0);
    }
}
