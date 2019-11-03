using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to manage what happens when food objects are picked up or put down
/// @author Trenton Plager - tlp6760@rit.edu
/// </summary>
public class PickupFood : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// This method is called when food is picked up by the player's hand
    /// It updates the SolitaryDataManager's fields
    /// </summary>
    public void OnFoodPickup()
    {
        //check to make sure it is food
        //it should be because this script should only be placed on food
        if (gameObject.tag == "Food")
        {
            //grab the object's name
            string name = gameObject.name;

            //change the SolitaryDataManager's fields based on what food is picked up 
            switch (name)
            {
                case "Apple":
                    SolitaryDataManager.Instance.HasBreakfastApple = false;
                    break;
                case "CerealBowl":
                case "CerealBowlFull(Clone)":
                    SolitaryDataManager.Instance.HasCerealBowl = false;
                    break;
                case "MilkCarton":
                    SolitaryDataManager.Instance.HasMilkCarton = false;
                    break;
                case "Egg":
                    SolitaryDataManager.Instance.HasEgg = false;
                    break;
                case "BreadSlice":
                    SolitaryDataManager.Instance.HasBreadSlice = false;
                    break;
                case "LunchApple":
                    SolitaryDataManager.Instance.HasLunchApple = false;
                    break;
                case "Sandwich":
                    SolitaryDataManager.Instance.HasSandwich = false;
                    break;
                case "Chips":
                    SolitaryDataManager.Instance.HasChips = false;
                    break;
                case "JuiceCup":
                    SolitaryDataManager.Instance.HasJuiceCup = false;
                    break;
                case "Bread":
                case "ButteredBread(Clone)":
                    SolitaryDataManager.Instance.HasBread = false;
                    break;
                case "ButterSlice":
                    SolitaryDataManager.Instance.HasButterSlice = false;
                    break;
                default:
                    Debug.Log("Name not recognized");
                    break;
            }
        }
    }

    /// <summary>
    /// This method is called when the player lets go of a food object
    /// It sets the object's isKinematic field to false so that gravity and physics wil run
    /// </summary>
    public void OnFoodDetach()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        //Debug.Log(gameObject.GetComponent<Rigidbody>().isKinematic);
    }
}
