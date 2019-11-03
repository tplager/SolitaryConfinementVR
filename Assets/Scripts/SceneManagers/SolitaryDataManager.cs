using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton class that won't be destroyed on scene change
/// so that data can be transferred between scenes
/// @author Trenton Plager - tlp6760@rit.edu
/// </summary>
public class SolitaryDataManager :  MonoBehaviour{

    #region Fields
    public static SolitaryDataManager Instance;

    #region Time Fields
    [SerializeField]
    private int SECONDS_TILL_OUTDOOR_SCENE; //a customizable field for seconds until outdoor scene
    [SerializeField]
    private int LENGTH_OF_OUTDOOR_SCENE;    //a customizable field for how long the outdoor scene lasts in seconds
    [SerializeField]
    private int SECONDS_TILL_FIRST_MEAL;    //a customizable field for seconds from end of outdoor scene to first meal
    [SerializeField]
    private int SECONDS_TILL_SECOND_MEAL;   //a customizable field for seconds from first meal to second meal
    [SerializeField]
    private int SECONDS_TILL_END;           //a customizable field for seconds from second meal to end of the experience
    #endregion

    [SerializeField]
    private bool isOutdoorOver;             //a boolean representing whether the outdoor scene is over yet

    [SerializeField]
    private Quaternion sunRotation;         //a field to hold the rotation of the sun when the scene changes

    #region First Meal Fields
    private bool hasBreakfastApple;
    private bool hasCerealBowl;
    private bool hasEgg;
    private bool hasBreadSlice;
    private bool hasMilkCarton;

    private bool hasBreakfastFood;
    #endregion

    #region Second Meal Fields
    private bool hasLunchApple;
    private bool hasSandwich;
    private bool hasChips;
    private bool hasJuiceCup;
    private bool hasBread;
    private bool hasButterSlice;

    private bool hasDinnerFood;
    #endregion
    #endregion

    #region Properties
    #region Time Properties
    public int SecondsTillOutdoorScene { get { return SECONDS_TILL_OUTDOOR_SCENE; } }
    public int LengthOfOutdoorScene { get { return LENGTH_OF_OUTDOOR_SCENE; } }
    public int SecondsTillFirstMeal { get { return SECONDS_TILL_FIRST_MEAL; } }
    public int SecondsTillSecondMeal { get { return SECONDS_TILL_SECOND_MEAL; } }
    public int SecondsTillEnd { get { return SECONDS_TILL_END; } }
    #endregion

    public Quaternion SunRotation { get { return sunRotation; } set { sunRotation = value; } }

    #region First Meal Properties
    public bool HasBreakfastApple { get { return hasBreakfastApple; } set { hasBreakfastApple = value; } }
    public bool HasCerealBowl { get { return hasCerealBowl; } set { hasCerealBowl = value; } }
    public bool HasEgg { get { return hasEgg; } set { hasEgg = value; } }
    public bool HasBreadSlice { get { return hasBreadSlice; } set { hasBreadSlice = value; } }
    public bool HasMilkCarton { get { return hasMilkCarton; } set { hasMilkCarton = value; } }

    public bool HasBreakfastFood { get { return hasBreakfastFood; } set { hasBreakfastFood = value; } }
    #endregion

    #region Second Meal Properties
    public bool HasLunchApple { get { return hasLunchApple; } set { hasLunchApple = value; } }
    public bool HasSandwich { get { return hasSandwich; } set { hasSandwich = value; } }
    public bool HasChips { get { return hasChips; } set { hasChips = value; } }
    public bool HasJuiceCup { get { return hasJuiceCup; } set { hasJuiceCup = value; } }
    public bool HasBread { get { return hasBread; } set { hasBread = value; } }
    public bool HasButterSlice { get { return hasButterSlice; } set { hasButterSlice = value; } }

    public bool HasDinnerFood { get { return hasDinnerFood; } set { hasDinnerFood = value; } }
    #endregion

    public bool IsOutdoorOver
    {
        get { return isOutdoorOver; }
        set { isOutdoorOver = value; }
    }
    #endregion

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        //start all food fields at true
        hasBreakfastApple = true;
        hasCerealBowl = true;
        hasEgg = true;
        hasBreadSlice = true;
        hasMilkCarton = true;

        hasBreakfastFood = true;

        hasLunchApple = true;
        hasSandwich = true;
        hasChips = true;
        hasJuiceCup = true;
        hasBread = true;
        hasButterSlice = true;

        hasDinnerFood = true;
    }
	
	// Update is called once per frame
	void Update () {

	}
}
