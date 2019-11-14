using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 15;
    public Transform target;
    public float damage = 1f;
    public float radius = 0;

    void Start()
    {
        
    }

    void Update()
    {
        if(target == null)//if no target
        {
            Destroy(gameObject);//destroy bullet
            return;
        }

        Vector3 dir = target.position - this.transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)//tells when it has reched target
        {
            DoBulletHit();
        }
        else
        {
            //move towards target
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);//face target
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);//not so snappy
        }
    }

    void DoBulletHit()
    {
        if (radius == 0)
        {
            target.GetComponent<Enemy>().takeDamage(damage);//bullet hit bullet do  damage
        }
        else
        {
            Physics.OverlapSphere(transform.position, radius);//tells what we have colided with

            foreach(Collider c in cols)
            {
                Enemy e = c.GetComponent<Enemy>();//checking if what it colided with was an enemy
                if (e != null)//if it colides
                {
                    e.GetComponent<Enemy>().takeDamage(damage);//take damage
                }
            }
        }

        Destroy(gameObject);//when reached target destroy
    }
}
