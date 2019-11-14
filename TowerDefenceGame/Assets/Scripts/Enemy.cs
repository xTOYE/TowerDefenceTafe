using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject pathGo;

    Transform targetPathNode;
    int pathNodeIndex = 0;

    public float speed = 5;

    public float health = 1f;

    public int moneyValue = 1;

    void Start()
    {
        pathGo = GameObject.Find("Path");//finding the path
    }

    void GetNextPathNode()
    {
        targetPathNode = pathGo.transform.GetChild(pathNodeIndex);
        pathNodeIndex++;//moving to next node
    }

    void Update()
    {
        if (targetPathNode == null)
        {
            GetNextPathNode();//finding where to go next
            if (targetPathNode == null)
            {
                //no more path
            }
        }

        Vector3 dir = targetPathNode.position - this.transform.localPosition;

        float distThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distThisFrame)
        {
            //if in a certain dis reached the node
            targetPathNode = null;
        }
        else
        {
            //move towards node
            transform.Translate(dir.normalized * distThisFrame, Space.World);
            Quaternion targetRotation = Quaternion.LookRotation(dir);//face node
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);//not so snappy
        }


    }

    void ReachedGoal()
    {
        GameObject.FindObjectOfType<ScoreManager>().LoseLife();//if enemy reaches goal lose life
        Destroy(gameObject);
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        GameObject.FindObjectOfType<ScoreManager>().money += moneyValue;//add money from enemy dieing
        Destroy(gameObject);
    }
}
