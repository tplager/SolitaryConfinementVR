using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A script to allow the player to "eat" food when food interacts with the collider on the player's head
/// @author Trenton Plager - tlp6760@rit.edu
/// </summary>
public class EatFood : MonoBehaviour {

    private AudioSource eatSoundSource; 

	// Use this for initialization
	void Start () {
        eatSoundSource = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// A method that runs when something enters the collider of the player's head
    /// </summary>
    /// <param name="other">The other collider that interacts with the player's head collider</param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Food")
        {
            string name = other.gameObject.name;

            Destroy(other.gameObject);
            eatSoundSource.Play();

            //change the SolitaryDataManager's fields based on what food is eaten
            //switch (name)
            //{
            //    case "Apple":
            //        SolitaryDataManager.Instance.HasBreakfastApple = false; 
            //        break;
            //    case "CerealBowl":
            //    case "CerealBowlFull(Clone)":
            //        SolitaryDataManager.Instance.HasCerealBowl = false;
            //        break;
            //    case "MilkCarton":
            //        SolitaryDataManager.Instance.HasMilkCarton = false; 
            //        break;
            //    case "Egg":
            //        SolitaryDataManager.Instance.HasEgg = false; 
            //        break;
            //    case "BreadSlice":
            //        SolitaryDataManager.Instance.HasBreadSlice = false;
            //        break;
            //    case "LunchApple":
            //        SolitaryDataManager.Instance.HasLunchApple = false;
            //        break;
            //    case "Sandwich":
            //        SolitaryDataManager.Instance.HasSandwich = false;
            //        break;
            //    case "Chips":
            //        SolitaryDataManager.Instance.HasChips = false;
            //        break;
            //    case "JuiceCup":
            //        SolitaryDataManager.Instance.HasJuiceCup = false;
            //        break;
            //    case "Bread":
            //    case "ButteredBread(Clone)":
            //        SolitaryDataManager.Instance.HasBread = false;
            //        break;
            //    case "ButterSlice":
            //        SolitaryDataManager.Instance.HasButterSlice = false;
            //        break;
            //    default:
            //        Debug.Log("Name not recognized");
            //        break;
            //}
        }
    }
}
