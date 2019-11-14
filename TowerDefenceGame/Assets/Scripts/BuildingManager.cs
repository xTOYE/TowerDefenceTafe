using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject selectedTower;

    public void SelectTowerType(GameObject prefab)//a way to identify what tower is selected
    {
        selectedTower = prefab;//the selected tower is = to the prefab selected
    }
}
