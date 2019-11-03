using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

/// <summary>
/// A script to manage what food is left on the tray for the second meal
/// @author Trenton Plager - tlp6760@rit.edu
/// </summary>
public class DinnerTray : MonoBehaviour {

    #region Fields
    [SerializeField]
    private GameObject lunchApple;  //the apple for the second meal object
    [SerializeField]
    private GameObject chips;       //the chip object
    [SerializeField]
    private GameObject sandwich;    //the sandwich object
    [SerializeField]
    private GameObject juiceCup;    //the juice cup object
    [SerializeField]
    private GameObject bread;       //the bread object
    [SerializeField]
    private GameObject butterSlice; //the butter slice object
    #endregion

    //properties
    public GameObject Bread { get { return bread; } set { bread = value; } }

    // Use this for initialization
    void Start () {
        if (!SolitaryDataManager.Instance.HasLunchApple)
        {
            Destroy(lunchApple);
        }
        if (!SolitaryDataManager.Instance.HasSandwich)
        {
            Destroy(sandwich);
        }
        if (!SolitaryDataManager.Instance.HasChips)
        {
            Destroy(chips);
        }
        if (!SolitaryDataManager.Instance.HasJuiceCup)
        {
            Destroy(juiceCup);
        }
        if (!SolitaryDataManager.Instance.HasBread)
        {
            Destroy(bread);
        }
        if (!SolitaryDataManager.Instance.HasButterSlice)
        {
            Destroy(butterSlice);
        }

        if (!SolitaryDataManager.Instance.HasLunchApple &&
            !SolitaryDataManager.Instance.HasSandwich &&
            !SolitaryDataManager.Instance.HasChips &&
            !SolitaryDataManager.Instance.HasJuiceCup &&
            !SolitaryDataManager.Instance.HasBread &&
            !SolitaryDataManager.Instance.HasButterSlice)
        {
            SolitaryDataManager.Instance.HasDinnerFood = false;
            DisablePickup();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (gameObject.tag == "Preview")
        {
            if (!SolitaryDataManager.Instance.HasLunchApple &&
                !SolitaryDataManager.Instance.HasSandwich &&
                !SolitaryDataManager.Instance.HasChips &&
                !SolitaryDataManager.Instance.HasJuiceCup &&
                !SolitaryDataManager.Instance.HasBread &&
                !SolitaryDataManager.Instance.HasButterSlice)
            {
                SolitaryDataManager.Instance.HasDinnerFood = false;
                DisablePickup();
            }
        }
    }

    /// <summary>
    /// Disable pickup of the tray when there is no food left
    /// </summary>
    public void DisablePickup()
    {
        gameObject.GetComponentInParent<Interactable>().enabled = false;
        gameObject.GetComponentInParent<ItemPackageSpawner>().enabled = false;
        gameObject.GetComponentInParent<BoxCollider>().enabled = false;
    }
}
