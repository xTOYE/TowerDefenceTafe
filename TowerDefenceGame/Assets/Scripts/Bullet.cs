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
        //if no target
        if (target == null)
        {
            //destroy bullet
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - this.transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;
        //tells when it has reched target
        if (dir.magnitude <= distThisFrame)
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
            //bullet hit bullet do  damage
            target.GetComponent<Enemy>().takeDamage(damage);
        }
        else
        {
            //tells what we have colided with
            Collider[] cols = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider c in cols)
            {
                //checking if what it colided with was an enemy
                Enemy e = c.GetComponent<Enemy>();
                if (e != null)//if it colides
                {
                    //take damage
                    e.GetComponent<Enemy>().takeDamage(damage);
                }
            }
        }
        //when reached target destroy
        Destroy(gameObject);
    }
}
