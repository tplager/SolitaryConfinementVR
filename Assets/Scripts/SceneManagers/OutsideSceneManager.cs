using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to manage the outdoor scene
/// @author Trenton Plager - tlp6760@rit.edu
/// </summary>
public class OutsideSceneManager : MonoBehaviour {

    #region Fields
    [SerializeField]
    private GameObject rotatingGateHinge;   //the object for the gate that rotates
    private AudioSource gateAudioSource;    //the audio source for that gate
    [SerializeField]
    private float outsideTicker;            //a float that increments according to deltaTime
    [SerializeField]
    private bool gateOpen;                  //a bool representing whether the gate is open or not

    private int LENGTH_OF_OUTDOOR_SCENE;    //the length of the outdoor scene

    [SerializeField]
    private GameObject morningBirds;        //the object holding the morning birds sound 
    private AudioSource morningBirdsSource; 
    [SerializeField]
    private GameObject walkingCrowd;        //the object holding the walking crowd sound
    private AudioSource walkingCrowdSource; 
    [SerializeField]
    private GameObject crowdTalking;        //the object holding the crowd talking sound
    private AudioSource crowdTalkingSource;

    [SerializeField]
    private GameObject sceneChangeCube;     //a cube with a trigger collider so that if the player
                                            //tries to walk out, the scene changes
    #endregion

    // Use this for initialization
    void Start () {
        gateOpen = false;
        outsideTicker = 0;
        gateAudioSource = rotatingGateHinge.GetComponent<AudioSource>();

        morningBirdsSource = morningBirds.GetComponent<AudioSource>();
        walkingCrowdSource = walkingCrowd.GetComponent<AudioSource>();
        crowdTalkingSource = crowdTalking.GetComponent<AudioSource>();

        morningBirdsSource.PlayDelayed(3.5f);
        walkingCrowdSource.PlayDelayed(8.0f);
        crowdTalkingSource.PlayDelayed(8.2f);

        morningBirdsSource.volume = 0;
        walkingCrowdSource.volume = 0;
        crowdTalkingSource.volume = 0;

        LENGTH_OF_OUTDOOR_SCENE = SolitaryDataManager.Instance.LengthOfOutdoorScene;
	}
	
	// Update is called once per frame
	void Update () {
        if (!gateOpen)
        {
            CheckOutsideTime(); 
        }
        else
        {
            OpenGate(); 
        }

        if (outsideTicker > 3.5f)
        {
            AmbienceTracker();
        }
    }

    /// <summary>
    /// Checks the outsideTicker and increments it according to the result of the check
    /// </summary>
    public void CheckOutsideTime()
    {
        if (outsideTicker < LENGTH_OF_OUTDOOR_SCENE)
        {
            outsideTicker += Time.deltaTime;
        }
        else //if the ticker reaches the end of the scene, open the gate and play the sound
        {
            gateOpen = true;
            gateAudioSource.Play();
            SolitaryDataManager.Instance.IsOutdoorOver = true;
        }
    }

    /// <summary>
    /// Open the gate by rotating and change the scene once it reaches a specified rotation
    /// </summary>
    public void OpenGate()
    {
        Debug.Log(rotatingGateHinge.transform.rotation.eulerAngles.y);
        if (rotatingGateHinge.transform.rotation.eulerAngles.y <= 270)
        {
            rotatingGateHinge.transform.Rotate(new Vector3(0, 1, 0), 4 * (Time.deltaTime));
        }
        else
        {
            gateAudioSource.Stop();
            sceneChangeCube.GetComponent<SceneChange>().ChangeScene();
            gateOpen = false;
        }
    }

    /// <summary>
    /// Tracks the ambience volume in the outdoor scene and raises it until it reaches specified values
    /// </summary>
    public void AmbienceTracker()
    {
        if (morningBirdsSource.volume < 0.5)
        {
            morningBirdsSource.volume += 0.002f; 
        }
        if (walkingCrowdSource.volume < 0.8)
        {
            walkingCrowdSource.volume += 0.001f; 
        }
        if (crowdTalkingSource.volume < 0.6)
        {
            crowdTalkingSource.volume += 0.001f; 
        }
    }
}
