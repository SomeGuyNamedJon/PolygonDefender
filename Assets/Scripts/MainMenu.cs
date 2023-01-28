using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene("Level 0");
    }

    public void Quit(){
        Application.Quit();
    }

    public void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){
            Quit();
        }
        if(Input.GetKeyDown(KeyCode.Return)){
            Play();
        }
    }
}
