using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    Transform turretTransform;

    float range = 10f;
    public GameObject bulletPrefab;

    public int cost = 5;

    float fireCooldown = 0.5f;
    float fireCooldownLeft = 0;

    public float Damage = 1;
    public float radius = 0;

    void Start()
    {
        turretTransform = transform.Find("Turret");//finding what part to move
    }

    void Update()
    {
        Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();//looking for enemies

        Enemy nearestEnemy = null;
        float dist = Mathf.Infinity;//can see all

        foreach(Enemy e in enemies)//finding nearest enemy
        {
            float d = Vector3.Distance(this.transform.position, e.transform.position);
            if(nearestEnemy == null || d < dist)//if = to 0 or closest
            {
                nearestEnemy = e;//make closest enemy e
                dist = d;//make there distance d
            }
        }

        if(nearestEnemy == null)
        {
            Debug.Log("No enemies");
            return;
        }

        Vector3 dir = nearestEnemy.transform.position - this.transform.position;//telling that it should look at 'nearest enemy'

        Quaternion lookRot = Quaternion.LookRotation(dir);

        turretTransform.rotation = Quaternion.Euler( 0, lookRot.eulerAngles.y, 0);//telling where to look

        fireCooldownLeft -= Time.deltaTime;//calculating firecooldown
        if(fireCooldownLeft <= 0 && dir.magnitude <= range)//finding when firecooldown is over then checking if in range
        {
            fireCooldownLeft = fireCooldown;//fire if cooldown is over
            ShootAt(nearestEnemy);
        }
    }

    void ShootAt (Enemy e)
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);//get bulletPrefab

        Bullet b = bulletGO.GetComponent<Bullet>();//setting b to bullets prefab
        b.target = e.transform;//bullets go to ememies
        b.damage = Damage;//bullet damage = to tower damage
        b.radius = radius;//bullet radius = to tower range
    }
}
