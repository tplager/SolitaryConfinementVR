using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using UnityEngine.SceneManagement;

/// <summary>
/// A scripti that controls various functions of the scene such as ambient sounds,
/// buttons being pressed, and spawning the player initially
/// </summary>
public class SceneController : MonoBehaviour {

    #region Fields
    private PlumbingManager plumbingManager;    //the plumbing manager

    [SerializeField]
    private GameObject playerPrefab;            //the prefab for the player

    [SerializeField]
    private GameObject waterDrippingSound;      //the object holding the water dripping sound

    private float soundTicker;
    private bool waterDripPlaying;

    //[SerializeField]
    //private GameObject breakfastTrayFullCereal;
    #endregion

    // Use this for initialization
    void Start ()
    {
        plumbingManager = gameObject.GetComponent<PlumbingManager>();

        //if there is no player, spawn a player object where the player prefab is located 
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            GameObject playerMarker = GameObject.Find("PlayerMarker");
            Instantiate(playerPrefab, playerMarker.transform.position, playerMarker.transform.rotation);
            Debug.Log("Spawning player at " + playerMarker.transform.position);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = GameObject.Find("PlayerMarker").transform.position;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
		for (int handIndex = 0; handIndex < Player.instance.hands.Length; handIndex++)
        {
            Hand hand = Player.instance.hands[handIndex];
            if (hand != null)
            {
                hand.HideController(true);
                hand.SetSkeletonRangeOfMotion(Valve.VR.EVRSkeletalMotionRange.WithoutController);
            }
        }

        if (!waterDripPlaying && SceneManager.GetActiveScene().name == "CellScene")
        {
            soundTicker += Time.deltaTime;

            if (soundTicker > 0.5)
            {
                waterDripPlaying = true;
                waterDrippingSound.GetComponent<AudioSource>().Play();
            }
        }
    }

    /// <summary>
    /// Resets the sink's timer to 0 and sets it to true
    /// telling the water to continue running
    /// </summary>
    /// <param name="fromHand">The hand that activates the button</param>
    public void OnSinkButtonDown(Hand fromHand)
    {
        plumbingManager.IsWaterRunning = true;
        plumbingManager.SinkTicker = 0;
        plumbingManager.SinkAudioSource.Play();
    }

    /// <summary>
    /// Plays the toilet's water animation when its button is pressed
    /// </summary>
    /// <param name="fromHand">The hand that activates the button</param>
    public void OnToiletButtonDown(Hand fromHand)
    {
        plumbingManager.ToiletWaterAnimator.SetTrigger("ToiletFlushed");
        plumbingManager.ToiletAudioSource.Play();
    }

    //public void OnBreakfastTrayPickup()
    //{
    //    //if (!SolitaryDataManager.Instance.HasMilkCarton)
    //    //{
    //    //    GameObject.Find("BrunchTrayPickup").GetComponent<ItemPackageSpawner>().itemPackage.itemPrefab = breakfastTrayFullCereal;
    //    //}
    //}
}
