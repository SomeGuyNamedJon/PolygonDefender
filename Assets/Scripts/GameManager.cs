using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int enemyCnt = 0;
    public int totalEnemyCnt = 0;
    public float timeLeft;
    public bool gamePaused = false;
    public bool targetDead = false;
    public bool timeStart = false;
    public int resources = 100;
    public LevelManager level;
    public WaveManager wave;
    public HUD_Controller UI;
    public DropTurret turretClick;


    public void Awake(){
        timeLeft = level.prepTime;
    }

    public void Update()
    {
        if(wave.waveStarted && !targetDead){
            if(enemyCnt == 0){
                if(wave.lastWave){
                    if(level.lastLevel){
                        GameWin();
                    }else{
                        LevelWin();
                    }
                }else{
                    wave.Next();
                }
            }
        }else if(timeStart){
            timeLeft -= Time.deltaTime;
            if(timeLeft < 0){
                wave.BeginWave();
            }
        }

        if(targetDead){
            //Display Game Over message
            Invoke("GameOver", 2f);
        }
    }

    public void GameWin(){
        Debug.Log("CONGRATS YOU WON THE WHOLE GAME!!!!");
    }

    public void LevelWin(){
        UI.ShowLevelWin();
        Invoke("NextLevel", 2f);
    }

    private void NextLevel(){
        LevelManager.levelNum++;
        level.Next();
    }

    public void SelectTurret(int selection){
        turretClick.selected = selection - 1;
    }

    public void GameOver(){
        UI.ShowGM();
    }
}
