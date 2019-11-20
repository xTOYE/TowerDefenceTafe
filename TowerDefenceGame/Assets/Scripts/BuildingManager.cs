using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject selectedTower;

    //a way to identify what tower is selected
    public void SelectTowerType(GameObject prefab)
    {
        //the selected tower is = to the prefab selected
        selectedTower = prefab;
    }
}
