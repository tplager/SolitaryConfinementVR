using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to manage the various plumbing objects in the cell scene
/// such as the sink and the toilet
/// @author Trenton Plager - tlp6760@rit.edu
/// </summary>
public class PlumbingManager : MonoBehaviour {

    #region Fields
    [SerializeField]
    private GameObject waterFlow;
    [SerializeField]
    private GameObject sinkWaterPool;
    [SerializeField]
    private ParticleSystem waterParticles;
    [SerializeField]
    private GameObject toiletWaterPool; 
    private bool isWaterRunning;
    private float sinkTicker;
    [SerializeField]
    private int sinkTickerThreshold;

    [SerializeField]
    private GameObject sinkAudioObject;
    private AudioSource sinkAudioSource; 
    
    [SerializeField]
    private float waterFlowPosDownInc;
    [SerializeField]
    private float waterFlowScaleUpInc; 
    [SerializeField]
    private float sinkWaterPoolPosUpInc;
    [SerializeField]
    private float sinkWaterPoolPosDownInc;

    private Animator toiletWaterAnimator;

    [SerializeField]
    private GameObject toiletAudioObject;
    private AudioSource toiletAudioSource; 
    #endregion

    #region Properties
    public bool IsWaterRunning { set { isWaterRunning = value; } }
    public int SinkTicker { set { sinkTicker = value; } }
    public Animator ToiletWaterAnimator { get { return toiletWaterAnimator; } }
    public AudioSource SinkAudioSource { get { return sinkAudioSource; } }
    public AudioSource ToiletAudioSource { get { return toiletAudioSource; } }
    #endregion

    // Use this for initialization
    void Start () {
        toiletWaterAnimator = toiletWaterPool.GetComponent<Animator>();
        sinkAudioSource = sinkAudioObject.GetComponent<AudioSource>();
        toiletAudioSource = toiletAudioObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        RunWater(); 	
	}

    /// <summary>
    /// Manages the water running in the sink
    /// </summary>
    public void RunWater()
    {
        //if the water is running bring the cylinder of water down and the pool of water up
        if (isWaterRunning)
        {
            //if the cylinder of water is below 1.21 raise it
            if (waterFlow.transform.localScale.y <= 1.21f)
            {
                //scale up water flow's y scale until it reaches 1.21
                Vector3 newWaterFlowScale = waterFlow.transform.localScale;
                newWaterFlowScale.y += waterFlowScaleUpInc;
                waterFlow.transform.localScale = newWaterFlowScale;
            }
            else
            {
                //if the sink's water pool is below 4.6 raise it immediately to 4.6
                if (sinkWaterPool.transform.position.y <= 4.6f)
                {
                    Vector3 newSinkWaterPosition = sinkWaterPool.transform.position;
                    newSinkWaterPosition.y = 4.6f;
                    sinkWaterPool.transform.position = newSinkWaterPosition;
                }

                //raise sink's water pool gradually until it reaches 4.93
                if (sinkWaterPool.transform.position.y <= 4.93f)
                {
                    Vector3 newSinkWaterPosition = sinkWaterPool.transform.position;
                    newSinkWaterPosition.y += sinkWaterPoolPosUpInc;
                    sinkWaterPool.transform.position = newSinkWaterPosition;
                }
                //if the sink's water pool hsa reached 4.93, play the water particles
                //and increment the ticker
                else
                {
                    waterParticles.Play();
                    sinkTicker += Time.deltaTime; 
                }

                //if the ticker reaches the threshold set the water running to false
                //and reset the ticker
                if (sinkTicker >= sinkTickerThreshold)
                {
                    isWaterRunning = false;
                    sinkTicker = 0; 
                }
            }
        }
        //if the water is not running, lower the water pool and the water cylinder
        else
        {
            //if the water cylinder is above 4.76 and is scaled above 0.066
            //move it down until it reaches 4.76
            if (waterFlow.transform.position.y >= 4.76f && waterFlow.transform.localScale.y > 0.066f)
            {
                //move the water flow down until it reaches 4.76 
                Vector3 newWaterFlowPos = waterFlow.transform.position;
                newWaterFlowPos.y -= waterFlowPosDownInc;
                waterFlow.transform.position = newWaterFlowPos;
            }
            //if the water cylinder is below 4.76 and/or is scaled below 0.066 
            //stop the water particles and scale down the water cylinder
            else
            {
                waterParticles.Stop();

                Vector3 newWaterFlowScale = waterFlow.transform.localScale;
                newWaterFlowScale.y = 0.066f;
                waterFlow.transform.localScale = newWaterFlowScale;

                Vector3 newWaterFlowPos = waterFlow.transform.position;
                newWaterFlowPos.y = 6.43f;
                waterFlow.transform.position = newWaterFlowPos;
                
                //lower sink's water pool gradually until it reaches 4.0
                if (sinkWaterPool.transform.position.y >= 4.0f)
                {
                    Vector3 newSinkWaterPosition = sinkWaterPool.transform.position;
                    newSinkWaterPosition.y -= sinkWaterPoolPosDownInc;
                    sinkWaterPool.transform.position = newSinkWaterPosition;
                }
            }
        }
    }
}
