using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class HUD_Controller : MonoBehaviour
{
    public GameManager manager;

    public GameObject HUD;
    public GameObject GameOverMsg;
    public GameObject LevelCompleteMsg;
    public GameObject WaveMsg;

    public Button select1;
    public Button select2;
    public Button select3;

    public Text levelCounter;
    public Text waveCounter;
    public Text enemiesLeft;
    public Text timeLeft;
    public void Start(){
        levelCounter.text += LevelManager.levelNum;
        waveCounter.text += WaveManager.waveNum;
        timeLeft.text = TimeSpan.FromSeconds(manager.level.prepTime).ToString(@"m\:ss");
        HUD.SetActive(true);
        select1.onClick.AddListener(SelectTurret1);
        select2.onClick.AddListener(SelectTurret2);
        select3.onClick.AddListener(SelectTurret3);
        ShowWave();
    }

    public void ShowWave(){
        WaveMsg.SetActive(true);
        Invoke("closeWaveDisplay", 2f);
    } 

    private void closeWaveDisplay(){
        WaveMsg.SetActive(false);
        manager.timeStart = true;
    }

    public void ShowGM(){
        HUD.SetActive(false);
        GameOverMsg.SetActive(true);
    }

    public void ShowLevelWin(){
        HUD.SetActive(false);
        LevelCompleteMsg.SetActive(true);
    }

    public void SelectTurret1(){
        manager.SelectTurret(1);
    }
    public void SelectTurret2(){
        manager.SelectTurret(2);
    }
    public void SelectTurret3(){
        manager.SelectTurret(3);
    }

    public void Retry(){
        manager.wave.resetWaves();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit(){
        Application.Quit();
    }

    public void Update(){
        if(manager.timeLeft < 0){
            timeLeft.gameObject.SetActive(false);
            enemiesLeft.text = "Enemies left: " + (manager.wave.maxEnemySpawn - (manager.totalEnemyCnt - manager.enemyCnt));
        }else{
            timeLeft.text = TimeSpan.FromSeconds(manager.timeLeft).ToString(@"m\:ss");
        }
    }

}
