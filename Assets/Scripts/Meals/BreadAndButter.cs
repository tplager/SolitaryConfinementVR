using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A class that deals with switching the bread to the buttered bread model
/// if the butter has been used with it
/// @author Trenton Plager -tlp6760@rit.edu
/// </summary>
public class BreadAndButter : MonoBehaviour {

    //fields
    [SerializeField]
    private GameObject butteredBreadPrefab;

    // Use this for initialization
    void Start()
    {
        if (!SolitaryDataManager.Instance.HasButterSlice)
        {
            Transform currentTransform = gameObject.transform;
            GameObject butteredBread = Instantiate(butteredBreadPrefab);
            butteredBread.transform.parent = gameObject.transform.parent;
            butteredBread.transform.position = currentTransform.position;
            butteredBread.transform.rotation = currentTransform.rotation;
            butteredBread.transform.localScale = currentTransform.localScale;

            gameObject.GetComponentInParent<DinnerTray>().Bread = butteredBread;

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// If the collider on the bread has been intersected
    /// with the collider of the butter
    /// Change the bread model to the buttered bread model 
    /// </summary>
    /// <param name="other">The collider that intersects the bread's collider</param>
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("BreadTriggered");
        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "ButterSlice")
        {
            SolitaryDataManager.Instance.HasButterSlice = false;            //change the solitary data manager's field to false for future reference
            Destroy(other.gameObject);                                      //destroy the butter slice

            Transform currentTransform = gameObject.transform;              //save the transform of the bread
            GameObject butteredBread = Instantiate(butteredBreadPrefab);    //instantiate a buttered bread prefab
            butteredBread.transform.parent = gameObject.transform.parent;   //make the parent of the buttered bread the same as the un-buttered bread
            //butteredBread.transform.position = currentTransform.position;   //move the buttered bread
            butteredBread.transform.rotation = currentTransform.rotation;
            butteredBread.transform.Rotate(new Vector3(180, 0, 0));
            Vector3 newBreadPos = new Vector3(-2.524f, 2.357f, -1.447f);
            butteredBread.transform.localPosition = newBreadPos; 
            butteredBread.transform.localScale = currentTransform.localScale;

            gameObject.GetComponentInParent<DinnerTray>().Bread = butteredBread;   //setting the DinnerTray script's bread field to the buttered bread prefab

            Destroy(gameObject);    //destroying the un-buttered bread
        }
    }
}
