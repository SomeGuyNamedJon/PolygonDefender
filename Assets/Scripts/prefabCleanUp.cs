using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prefabCleanUp : MonoBehaviour
{
    void OnCollisionEnter(Collision collisionInfo){
        //Debug.Log("Collision detected");
        if(collisionInfo.collider.tag == "Destroy"){
            Destroy(this.gameObject);
        }
    }
}
