using System.Collections;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    private int index = 0;
    public GameObject[] enemyList;
    public float spawnFrequency = 5f;
    public GameManager manager;
    public Transform spawnPoint;

    void Awake(){
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ActivateSpawner()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        while(!manager.targetDead && !manager.gamePaused && manager.wave.maxEnemySpawn > manager.totalEnemyCnt){            
            index = Random.Range(0,enemyList.Length - 1);
            Instantiate(enemyList[index], spawnPoint.position, Quaternion.identity);
            manager.enemyCnt++;
            manager.totalEnemyCnt++;
            yield return new WaitForSeconds(spawnFrequency + Random.Range(-(spawnFrequency-1), 0));
        }
    }
}
