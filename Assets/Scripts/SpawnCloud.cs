using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCloud : MonoBehaviour
{   
    private int index = 0;
    private int oldIndex = 0;
    private float randOffset = 0f;
    private float oldOffset = 0f;
    private Vector3 spawnLocation;
    public GameObject[] prefabCloud;
    public float spawnFrequency = 5f;
    public GameManager manager;
    
    void Awake(){
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn(){
        while(!manager.gamePaused){
            spawnLocation = transform.position;
            
            while(oldOffset - 10f < randOffset && randOffset < oldOffset + 10f){
                randOffset = Random.Range(-50, 150);
            }
            spawnLocation = new Vector3(spawnLocation.x + randOffset, spawnLocation.y, spawnLocation.z + randOffset);
            oldOffset = randOffset;

            while(index == oldIndex){
                index = Random.Range(0,prefabCloud.Length - 1);
            }
            oldIndex = index;
            
            Instantiate(prefabCloud[index], spawnLocation, Quaternion.identity);
            yield return new WaitForSeconds(spawnFrequency + Random.Range(-(spawnFrequency-1), 0));
        }
    }
}
