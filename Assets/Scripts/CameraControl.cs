using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Designed for 4 camera cell only
public class CameraControl : MonoBehaviour
{
    public vCell cameras;
    public bool wrap = false;
    private GameObject currentCam;
    private int x,y;

    void Awake(){
        currentCam = cameras.stack[1].cam[1];
        y = 1;
        x = 1;
        currentCam.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow)){
            currentCam.SetActive(false);

            if(y == 0){
                if(wrap) y = cameras.stack.Length - 1;
            }else{
                y--;
            }
            currentCam = cameras.stack[y].cam[x];

            currentCam.SetActive(true);

        }

        if(Input.GetKeyDown(KeyCode.DownArrow)){
            currentCam.SetActive(false);

            if(y == cameras.stack.Length - 1){
                if(wrap) y = 0;
            }else{
                y++;
            }
            currentCam = cameras.stack[y].cam[x];

            currentCam.SetActive(true);

        }

        if(Input.GetKeyDown(KeyCode.RightArrow)){
            currentCam.SetActive(false);

            if(x == cameras.stack[y].cam.Length - 1){
                if(wrap) x = 0;
            }else{
                x++;
            }
            currentCam = cameras.stack[y].cam[x];
            

            currentCam.SetActive(true);

        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)){
            currentCam.SetActive(false);

            if(x == 0){
                if(wrap) x = cameras.stack[y].cam.Length - 1;
            }else{
                x--;
            }
            currentCam = cameras.stack[y].cam[x];
            

            currentCam.SetActive(true);

        }
    }
}
