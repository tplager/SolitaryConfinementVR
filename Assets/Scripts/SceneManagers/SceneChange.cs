using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using UnityEngine.SceneManagement;

/// <summary>
/// Changes the current scene to the next scene in the array
/// @author Trenton Plager - tlp6760@rit.edu
/// </summary>
public class SceneChange : MonoBehaviour {

    #region Fields
    public int currLevel;

    public string[] levelNames;

    [SerializeField]
    private GameObject sun;

    private bool loading; 
    #endregion

    // Use this for initialization
    void Start () {
        loading = false; 
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void OnTriggerEnter(Collider other)
    {
        ChangeScene();
    }

    /// <summary>
    /// Changes the scene
    /// </summary>
    public void ChangeScene()
    {
        if (!loading)
        {
            loading = true;
            currLevel = (currLevel + 1) % 2; //next level
            SolitaryDataManager.Instance.SunRotation = sun.transform.rotation;
            SteamVR_LoadLevel.Begin(levelNames[currLevel]);
            Debug.Log("Triggered");
        }
    }
}
