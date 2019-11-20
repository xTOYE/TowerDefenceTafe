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
        if (pathNodeIndex < pathGo.transform.childCount)
        {
            targetPathNode = pathGo.transform.GetChild(pathNodeIndex);
            pathNodeIndex++;//moving to next node
        }
        else
        {
            targetPathNode = null;
            ReachedGoal();
        }
    }

    void Update()
    {
        if (targetPathNode == null)
        {
            //finding where to go next
            GetNextPathNode();
            if (targetPathNode == null)
            {//no more path
                ReachedGoal();
                return;
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
            //face node
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            //not so snappy smoother turn
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime * 5);
        }
    }

    void ReachedGoal()
    {
        //if enemy reaches goal lose life
        GameObject.FindObjectOfType<ScoreManager>().LoseLife();
        Destroy(gameObject);
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //add money from enemy dieing
        GameObject.FindObjectOfType<ScoreManager>().money += moneyValue;
        Destroy(gameObject);
    }
}
