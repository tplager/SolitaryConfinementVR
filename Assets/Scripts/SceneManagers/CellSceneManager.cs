using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

/// <summary>
/// A script that manages all the data and behaviors of the cell scene
/// @author Trenton Plager -tlp6760@rit.edu
/// </summary>
public class CellSceneManager : MonoBehaviour {

    #region Fields
    #region Door Fields
    [SerializeField]
    private GameObject openingDoor;
    [SerializeField]
    private GameObject doorSourceObject; 
    private AudioSource doorAudioSource;
    #endregion

    #region Slider Fields
    //[SerializeField]
    //private GameObject slider;
    [SerializeField]
    private GameObject sliderSource;
    private AudioSource sliderAudioSource;
    [SerializeField]
    private GameObject animatedDoor; 
    private Animator sliderAnimator;
    #endregion

    #region Tray Fields
    [SerializeField]
    private GameObject brunchTray;
    [SerializeField]
    private GameObject dinnerTray;

    private bool trayInside;
    #endregion

    #region Cart Fields
    [SerializeField]
    private GameObject cartRollingSourceObject; 
    private AudioSource cartRollingSource;
    [SerializeField]
    private AudioClip cartRollingStartClip;

    private bool cartGone;

    private Vector3 cartStartPosition; 
    #endregion

    #region Booleans
    [SerializeField]
    private bool doorOpen;
    [SerializeField]
    private bool sliderOpen;
    [SerializeField]
    private bool doorUnlocking;
    [SerializeField]
    private bool isOutdoorDone;
    [SerializeField]
    private bool isFirstMealDone;
    [SerializeField]
    private bool isSecondMealDone;
    #endregion

    #region Time Fields
    [SerializeField]
    private float cellTicker;

    private int SECONDS_TILL_OUTDOOR_SCENE;
    private int SECONDS_TILL_FIRST_MEAL;
    private int SECONDS_TILL_SECOND_MEAL;
    private int SECONDS_TILL_END;
    #endregion

    #region Scene Fields
    public GameObject sceneChangeCube;

    [SerializeField]
    private int fadeDuration;
    #endregion

    #region Rain Fields
    [SerializeField]
    private GameObject rainVideo;
    [SerializeField]
    private GameObject rainSource;
    private AudioSource rainAudioSource;
    #endregion
    #endregion

    // Use this for initialization
    void Start () {
        doorOpen = false;
        cellTicker = 0;
        doorAudioSource = doorSourceObject.GetComponent<AudioSource>();
        sliderAudioSource = sliderSource.GetComponent<AudioSource>();

        SECONDS_TILL_OUTDOOR_SCENE = SolitaryDataManager.Instance.SecondsTillOutdoorScene;
        SECONDS_TILL_FIRST_MEAL = SolitaryDataManager.Instance.SecondsTillFirstMeal;
        SECONDS_TILL_SECOND_MEAL = SolitaryDataManager.Instance.SecondsTillSecondMeal + SECONDS_TILL_FIRST_MEAL;
        SECONDS_TILL_END = SolitaryDataManager.Instance.SecondsTillEnd + SECONDS_TILL_SECOND_MEAL;

        isOutdoorDone = SolitaryDataManager.Instance.IsOutdoorOver;

        rainAudioSource = rainSource.GetComponent<AudioSource>();

        sliderAnimator = animatedDoor.GetComponent<Animator>();

        cartRollingSource = cartRollingSourceObject.GetComponent<AudioSource>();
        cartStartPosition = cartRollingSourceObject.transform.position;
        cartRollingSourceObject.GetComponent<PathFollower>().enabled = false; 
    }

    // Update is called once per frame
    void Update () {
        if (!isOutdoorDone)         //if the outdoor scene isn't done yet check cell time until it reaches that time
                                    //then open the door and go to the outdoor scene
        {
            if (!doorOpen)          //if the door isn't already open, check the current time
            {
                CheckCellTime();
            }
            else                    //otherwise start opening the door
            {
                OpenDoor();
            }
        }
        else                                    //if the outdoor scene is already done check the time to operate the slider and send meals in
        {
            if (!sliderOpen)                    //if the slider isn't open, check the time
            {
                CheckCellTime();
                if (cartRollingSourceObject.GetComponent<PathFollower>().enabled && cartRollingSource.volume > 0)
                {
                    DecreaseSourceVolume(cartRollingSource);
                    if (cartRollingSource.volume == 0)
                    {
                        ResetCart();
                    }
                }
            }
            else if (sliderOpen && !trayInside) //if the slider is open and the tray isn't inside, send the tray inside
            {
                SendFoodTrayIn();
            }

            //if the tray is inside and the cart isn't gone yet 
            //decrease the cart's volume until it reaches 0
            if (trayInside && !cartGone)
            {
                DecreaseSourceVolume(cartRollingSource);
                if (cartRollingSource.volume == 0)
                {
                    ResetCart();
                }
            }

            if (sliderOpen && trayInside)       //if the slider is open and the tray is inside
                                                //check if there is no more food on the correct tray
            {
                if ((!isFirstMealDone && !SolitaryDataManager.Instance.HasBreakfastFood) || (isFirstMealDone && !SolitaryDataManager.Instance.HasDinnerFood))  
                {
                    TakeTrayOut();
                    EnableCart();
                }
            }
        }

        if (rainVideo.GetComponent<MeshRenderer>().enabled)             //if the rain video is enabled 
                                                                        //turn up the volume on the audio source until it reaches 0.5
        {
            if (rainAudioSource.volume >= 0.5)
            {
                rainAudioSource.volume += 0.01f; 
            }
        }
    }

    /// <summary>
    /// Checks the time based on the SECONDS_TILL... fields and the booleans
    /// Changes other booleans and runs methods based on these times
    /// </summary>
    public void CheckCellTime()
    {
        //if the outdoor scene isn't done increment the cell ticker by deltaTime until that marker is hit
        if (!isOutdoorDone)
        {
            if (cellTicker < SECONDS_TILL_OUTDOOR_SCENE)
            {
                cellTicker += Time.deltaTime;

                if (cellTicker > SECONDS_TILL_OUTDOOR_SCENE - 9.5 && !doorUnlocking)
                {
                    doorAudioSource.Play();
                    doorUnlocking = true; 
                }
            }
            else
            {
                doorOpen = true;
            }
        }
        else                                //otherwise increment cellTicker until the markers for the meals are hit
        {
            cellTicker += Time.deltaTime;

            if ((cellTicker > SECONDS_TILL_FIRST_MEAL && !isFirstMealDone) 
                || (cellTicker > SECONDS_TILL_SECOND_MEAL && !isSecondMealDone))
            {
                OperateSlider();
            }
            else if ((cellTicker > SECONDS_TILL_FIRST_MEAL - 5.5f && !isFirstMealDone)
                || (cellTicker > SECONDS_TILL_SECOND_MEAL - 5.5f && !isSecondMealDone))
            {
                EnableCart();
            }
            else if (cellTicker > SECONDS_TILL_END)
            {
                EndExperience();
            }
            else
            {
                sliderOpen = false;

            }
        }
    }

    /// <summary>
    /// Opens the cell door and moves to the outdoor scene
    /// </summary>
    public void OpenDoor()
    {
        //Debug.Log(openingDoor.transform.rotation.eulerAngles.y);
        if (openingDoor.transform.rotation.eulerAngles.y >= 85)
        {
            openingDoor.transform.Rotate(new Vector3(0, 1, 0), -4 * (Time.deltaTime));
        }
        else
        {
            doorAudioSource.Stop();
            sceneChangeCube.GetComponent<SceneChange>().ChangeScene();
            doorOpen = false; 
        }
    }
    
    /// <summary>
    /// Runs the slider animation and changes the sliderOpen boolean to its opposite
    /// </summary>
    public void OperateSlider()
    {
        sliderAnimator.SetTrigger("MakeOpen");
        sliderAudioSource.Play();

        sliderOpen = !sliderOpen;
    }

    /// <summary>
    /// Turns on the rain video and plays its sound
    /// </summary>
    public void ActivateRainVideo()
    {
        rainVideo.GetComponent<MeshRenderer>().enabled = true;
        rainAudioSource.volume = 0;
        rainAudioSource.Play();
    }

    /// <summary>
    /// Sends the correct food tray in based on the
    /// isFirstMealDone boolean
    /// </summary>
    public void SendFoodTrayIn()
    {
        bool foodTrayZMoveDone = false;
        bool foodTrayXMoveDone = false;

        GameObject foodTray;

        if (isFirstMealDone)
        {
            foodTray = dinnerTray;
        }
        else
        {
            foodTray = brunchTray;
        }

        if (foodTray.transform.localPosition.x < 3.3)
        {
            foodTray.transform.Translate(0.1f, 0, 0);
        }
        else
        {
            Debug.Log("X Move Done");
            foodTrayXMoveDone = true;
        }

        if (foodTray.transform.localPosition.z > 0.4 && foodTrayXMoveDone)
        {
            foodTray.transform.Translate(0, 0, -0.1f);
            foodTrayZMoveDone = false;
        }
        else
        {
            Debug.Log("Z Move Done");
            foodTrayZMoveDone = true;
        }

        if (foodTrayZMoveDone && foodTrayXMoveDone)
        {
            trayInside = true;
            Debug.Log("Tray Inside");
        }
    }

    /// <summary>
    /// Takes the correct food tray out based on the isFirstMealDone boolean
    /// and runs the OperateSlider method and changes the 
    /// </summary>
    public void TakeTrayOut()
    {
        bool foodTrayZMoveDone = false;
        bool foodTrayXMoveDone = false;

        GameObject foodTray;

        if (isFirstMealDone)
        {
            foodTray = dinnerTray;
        }
        else
        {
            foodTray = brunchTray;
        }

        if (foodTray.transform.localPosition.z < 2)
        {
            foodTray.transform.Translate(0, 0, 0.1f);
        }
        else
        {
            foodTrayZMoveDone = true;
        }

        if (foodTray.transform.localPosition.x > -3.1 && foodTrayZMoveDone)
        {
            foodTray.transform.Translate(-0.1f, 0, 0);
            foodTrayXMoveDone = false;
        }
        else
        {
            foodTrayXMoveDone = true;
        }



        if (foodTrayZMoveDone && foodTrayXMoveDone)
        {
            trayInside = false;
            Debug.Log("Tray Outside");

            OperateSlider();

            if (isFirstMealDone)
            {
                isSecondMealDone = true;
            }
            else
            {
                isFirstMealDone = true;
            }
        }
    }

    /// <summary>
    /// Ends the experience and fades to black
    /// </summary>
    public void EndExperience()
    {
        SteamVR_Fade.Start(Color.black, fadeDuration);
    }

    /// <summary>
    /// Decreases the volume of a passed-in source until it hits 0
    /// </summary>
    /// <param name="source">The source to decrease volume of</param>
    public void DecreaseSourceVolume(AudioSource source)
    {
        if (source.volume > 0)
        {
            source.volume -= 0.01f; 
        }
    }

    /// <summary>
    /// Enables the cart moving and begins playing the sound clip associated with it
    /// </summary>
    public void EnableCart()
    {
        cartGone = false;
        cartRollingSourceObject.GetComponent<PathFollower>().enabled = true;
        cartRollingSource.PlayOneShot(cartRollingStartClip);
        cartRollingSource.PlayDelayed(cartRollingStartClip.length);
        cartRollingSource.volume = 1; 
    }

    /// <summary>
    /// Moves the cart back to its starting position and disables it and its sound
    /// </summary>
    public void ResetCart()
    {
        cartGone = true;
        cartRollingSource.Stop();
        cartRollingSourceObject.GetComponent<PathFollower>().enabled = false;
        cartRollingSource.transform.position = cartStartPosition;
    }
}
