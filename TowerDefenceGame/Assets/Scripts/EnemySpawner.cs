using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float spawnCD = 0.5f;
    float spawnCDremaining = 0;
    float timeToNextWave = 5.0f;

    //allows the order in which the enemies are spawned to be controlled
    [System.Serializable]
    public class WaveComponent
    {
        public GameObject enemyPrefab;
        public int num;
        [System.NonSerialized]
        public int spawned = 0;
    }

    public WaveComponent[] waveComps;

    void Update()
    {


        //time between waves that saves and gives time for building towers
        //each wave saves towers money and lives ++ to current if higest < current ++ highest 
        //pause allows resume load and new game


        bool didSpawn = false;
        spawnCDremaining -= Time.deltaTime;
        if(spawnCDremaining < 0)
        {
            spawnCDremaining = spawnCD;
            //cycles through remaining enemies to spawn
            foreach (WaveComponent wc in waveComps)
            {
                //now spawn
                if (wc.spawned < wc.num)
                {//increase spawn count
                    wc.spawned++;
                    //spaw the enemy at span point
                    Instantiate(wc.enemyPrefab, this.transform.position, this.transform.rotation);

                    didSpawn = true;
                    break;
                }
            }
            //wave complete
            if (didSpawn == false)
            {
                //insurance so no error when out of rounds
                StartCoroutine(NextWave(timeToNextWave));
            }
        }
    }

    public IEnumerator NextWave(float delay)
    {
        // Before the delay
        Debug.Log("Next Wave Incoming");
        yield return new WaitForSeconds(delay);

        // After the delay

        if (transform.parent.childCount > 1)
        {
            ScoreManager.Instance.current++;

            //spawning next wave
            transform.parent.GetChild(1).gameObject.SetActive(true);
        }
        Destroy(gameObject);
    }
}
