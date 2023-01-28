using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTurret : MonoBehaviour
{
    public GameObject[] turrets;
    public Camera mainCam;

    public int selected = 0;

    void Update()
    {
        RaycastHit hit;
        RaycastHit[] sphereCheck;
        if(Input.GetMouseButtonDown(0)){
            Vector3 clickPosition = mainCam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            Physics.Raycast(clickPosition, mainCam.transform.forward, out hit);
            if(hit.collider.gameObject.tag == "Ground"){
                sphereCheck = Physics.SphereCastAll(hit.point, 2f, mainCam.transform.forward);
                
                bool canSpawn = true;
                foreach (RaycastHit sphereHit in sphereCheck)
                {
                    if(sphereHit.collider.gameObject.tag != "Ground"){
                        canSpawn = false;
                    }
                }

                if(canSpawn)
                    Instantiate(turrets[selected], hit.point, Quaternion.identity);
            }

        }
    }
}
