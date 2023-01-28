using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudWind : MonoBehaviour
{
    private float windSpeed = 15f;
    public GameManager manager;

    void Awake(){
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void FixedUpdate()
    {
        if(!manager.gamePaused)
            transform.position = transform.position + new Vector3(-windSpeed * Time.deltaTime, 0, windSpeed * Time.deltaTime);
    }
}
