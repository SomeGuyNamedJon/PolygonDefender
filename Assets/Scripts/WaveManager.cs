using UnityEngine;
using UnityEngine.SceneManagement;

public class WaveManager : MonoBehaviour
{
    //Wave Control
    public static float currentTargetHealth;
    public static int waveNum;
    public bool lastWave = false;
    public bool waveStarted = false;

    //Enemy Spawn Tower Control
    public GameObject[] spawnTowers;
    public bool[] activeAtStart;
    public static bool[] activeCurrent;
    public int activatePerWave;
    public GameObject spawnHUB;

    //Wave Preferences
    public int maxEnemySpawn;
    public int increasePerWave;
    public int levelWaves;

    //Target
    public TargetHealth target;

    public void Awake(){
        Debug.Log("Wave #" + waveNum);

        if(waveNum == 0){
            currentTargetHealth = target.health;
            activeCurrent = activeAtStart;
        }else{

            target.health = currentTargetHealth;

            if(waveNum >= levelWaves){
                lastWave = true;
            }

            for (int i = 0; i < activeCurrent.Length; i++){
                if(activeAtStart[i])
                    continue;
                if(activeCurrent[i])
                    spawnTowers[i].SetActive(true);
            }

            int set = 0;
            for (int i = 0; i < activeCurrent.Length; i++)
            {
                if(set == activatePerWave)
                    break;

                if(!activeCurrent[i]){
                    spawnTowers[i].SetActive(true);
                    activeCurrent[i] = true;
                    set++;
                    
                    //PsudeoRandomness
                    if(activeCurrent.Length > i+1){
                        i += 1;
                    }
                }
            }

            maxEnemySpawn += increasePerWave*waveNum;
        }
        waveNum++;

    }

    public void BeginWave(){
        GameObject spawner;
        for (int i = 0; i < spawnHUB.transform.childCount; i++)
        {
            spawner = spawnHUB.transform.GetChild(i).gameObject;
            if(spawner.activeSelf){
                spawner.GetComponent<EnemySpawn>().ActivateSpawner();
            }else{
                continue;
            }
        }
        waveStarted = true;
    }

    public void resetWaves(){
        currentTargetHealth = 500f;
        waveNum = 0;
        activeCurrent = activeAtStart;
    }

    public void Next(){
        currentTargetHealth = target.health;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
