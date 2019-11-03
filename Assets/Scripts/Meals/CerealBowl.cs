using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that deals with switching the cereal bowl to the full cereal bowl model
/// if the milk has been used with it
/// @author Trenton Plager -tlp6760@rit.edu
/// </summary>
public class CerealBowl : MonoBehaviour {

    //fields
    [SerializeField]
    private GameObject fullCerealBowlPrefab; 

	// Use this for initialization
	void Start () {
        if (!SolitaryDataManager.Instance.HasMilkCarton)// && gameObject.transform.parent.name == "BrunchTrayPreview")
        {
            Transform currentTransform = gameObject.transform;
            GameObject fullCerealBowl = Instantiate(fullCerealBowlPrefab);
            fullCerealBowl.transform.parent = gameObject.transform.parent;
            fullCerealBowl.transform.position = currentTransform.position;
            fullCerealBowl.transform.rotation = currentTransform.rotation;
            fullCerealBowl.transform.localScale = currentTransform.localScale;

            gameObject.GetComponentInParent<LunchTray>().CerealBowl = fullCerealBowl;

            Destroy(gameObject);
        }
	}
	
	// Update is called once per frame
	void Update () {

    }

    /// <summary>
    /// If the collider on the cereal bowl has been intersected
    /// with the collider of the milk carton
    /// Change the cereal bowl model to the full cereal bowl model 
    /// </summary>
    /// <param name="other">The collider that intersects the cereal bowl's collider</param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "MilkCarton")
        {
            SolitaryDataManager.Instance.HasMilkCarton = false;             //change the solitary data manager's field to false for future reference
            Destroy(other.gameObject);                                      //destroy the milk carton

            Transform currentTransform = gameObject.transform;              //save the transform of the cereal bowl
            GameObject fullCerealBowl = Instantiate(fullCerealBowlPrefab);  //instantiate a full cereal bowl
            fullCerealBowl.transform.parent = gameObject.transform.parent;  //make the parent of the full cereal bowl the same as the old cereal bowl
            fullCerealBowl.transform.position = currentTransform.position;  //move the full cereal bowl
            fullCerealBowl.transform.rotation = currentTransform.rotation;
            fullCerealBowl.transform.localScale = currentTransform.localScale;

            gameObject.GetComponentInParent<LunchTray>().CerealBowl = fullCerealBowl;   //setting the LunchTray script's cereal bowl field to the new full cereal bowl

            Destroy(gameObject);    //destroying the empty cereal bowl
        }
    }
}
