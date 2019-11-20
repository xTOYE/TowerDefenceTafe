using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Transform turretTransform;

    public float range = 10f;
    public GameObject bulletPrefab;

    public int cost = 5;

    public float fireCooldown = 0.5f;
    float fireCooldownLeft = 0;

    public float damage = 1;
    public float radius = 0;

    void Start()
    {
        //finding what part to move
        turretTransform = transform.Find("Turret");
    }

    void Update()
    {
        //looking for enemies
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();

        Enemy nearestEnemy = null;
        //can see all
        float dist = Mathf.Infinity;
        //finding nearest enemy
        foreach (Enemy e in enemies)
        {
            float d = Vector3.Distance(this.transform.position, e.transform.position);
            //if = to 0 or closest
            if (nearestEnemy == null || d < dist)
            {
                //make closest enemy e
                nearestEnemy = e;
                //make there distance d
                dist = d;
            }
        }

        if(nearestEnemy == null)
        {
            Debug.Log("No enemies");
            return;
        }
        //telling that it should look at 'nearest enemy'
        Vector3 dir = nearestEnemy.transform.position - this.transform.position;

        Quaternion lookRot = Quaternion.LookRotation(dir);
        //telling where to look
        turretTransform.rotation = Quaternion.Euler(0, lookRot.eulerAngles.y, 0);
        //calculating firecooldown
        fireCooldownLeft -= Time.deltaTime;
        //finding when firecooldown is over then checking if in range
        if (fireCooldownLeft <= 0 && dir.magnitude <= range)
        {
            //fire if cooldown is over
            fireCooldownLeft = fireCooldown;
            ShootAt(nearestEnemy);
        }
    }

    void ShootAt (Enemy e)
    {
        //get bulletPrefab
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);
        //setting b to bullets prefab
        Bullet b = bulletGO.GetComponent<Bullet>();
        b.target = e.transform;//bullets go to ememies
        b.damage = damage;//bullet damage = to tower damage
        b.radius = radius;//bullet radius = to tower range
    }
}
