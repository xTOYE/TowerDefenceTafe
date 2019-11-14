using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpot : MonoBehaviour
{
    private void OnMouseUp()//detecting if tower spot is clicked
    {
        BuildingManager bm = GameObject.FindObjectOfType<BuildingManager>();
        if(bm.selectedTower != null)//if there is a selected tower
        {
            ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
            if(sm.money < bm.selectedTower.GetComponent<Tower>().cost)//checking if enough money
            {
                Debug.Log("Not enough money");
                return;
            }

            sm.money -= bm.selectedTower.GetComponent<Tower>().cost;//minus the money

            Instantiate(bm.selectedTower, transform.parent.position, transform.parent.rotation);//refers to selected tower and locates it on the spot
            Destroy(transform.parent.gameObject);//destroys the spot
        }
    }
}
