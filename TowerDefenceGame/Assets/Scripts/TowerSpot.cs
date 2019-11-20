using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    //detecting if tower spot is clicked
    private void OnMouseUp()
    {
        BuildingManager bm = GameObject.FindObjectOfType<BuildingManager>();
        //if there is a selected tower
        if (bm.selectedTower != null)
        {
            ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
            //checking if enough money
            if (sm.money < bm.selectedTower.GetComponent<Tower>().cost)
            {
                Debug.Log("Not enough money");
                return;
            }
            //minus the money
            sm.money -= bm.selectedTower.GetComponent<Tower>().cost;
            //refers to selected tower and locates it on the spot
            Instantiate(bm.selectedTower, transform.parent.position, transform.parent.rotation);
            //destroys the spot
            Destroy(transform.parent.gameObject);
        }
    }
}
