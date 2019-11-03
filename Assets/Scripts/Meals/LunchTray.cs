using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// A script to manage whether there is still food on the tray for the first meal
/// @author Trenton Plager - tlp6760@rit.edu
/// </summary>
public class LunchTray : MonoBehaviour {

    #region Fields
    [SerializeField]
    private GameObject breakfastApple;  //the apple for breakfast object
    [SerializeField]
    private GameObject cerealBowl;      //the cereal bowl object
    [SerializeField]
    private GameObject milkCarton;      //the milk carton object
    [SerializeField]
    private GameObject egg;             //the egg object
    [SerializeField]
    private GameObject breadSlice;      //the bread slice object
    #endregion

    //proeprty
    public GameObject CerealBowl { get { return cerealBowl; } set { cerealBowl = value; } }

    // Use this for initialization
    void Start () {
		if (!SolitaryDataManager.Instance.HasBreakfastApple)
        {
            Destroy(breakfastApple);
        }
        if (!SolitaryDataManager.Instance.HasCerealBowl)
        {
            Destroy(cerealBowl);
        }
        if (!SolitaryDataManager.Instance.HasMilkCarton)
        {
            Destroy(milkCarton);
        }
        if (!SolitaryDataManager.Instance.HasEgg)
        {
            Destroy(egg);
        }
        if (!SolitaryDataManager.Instance.HasBreadSlice)
        {
            Destroy(breadSlice);
        }

        if (!SolitaryDataManager.Instance.HasBreakfastApple && 
            !SolitaryDataManager.Instance.HasCerealBowl && 
            !SolitaryDataManager.Instance.HasEgg && 
            !SolitaryDataManager.Instance.HasBreadSlice && 
            !SolitaryDataManager.Instance.HasMilkCarton)
        {
            SolitaryDataManager.Instance.HasBreakfastFood = false;
            DisablePickup();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.tag == "Preview")
        {
            if (!SolitaryDataManager.Instance.HasBreakfastApple &&
                !SolitaryDataManager.Instance.HasCerealBowl &&
                !SolitaryDataManager.Instance.HasEgg &&
                !SolitaryDataManager.Instance.HasBreadSlice &&
                !SolitaryDataManager.Instance.HasMilkCarton)
            {
                SolitaryDataManager.Instance.HasBreakfastFood = false;
                DisablePickup();
            }
        }

    }

    /// <summary>
    /// Disable the pickup of the tray once all the food is gone
    /// </summary>
    public void DisablePickup()
    {
        gameObject.GetComponentInParent<Interactable>().enabled = false;
        gameObject.GetComponentInParent<ItemPackageSpawner>().enabled = false;
        gameObject.GetComponentInParent<BoxCollider>().enabled = false; 
    }
}
