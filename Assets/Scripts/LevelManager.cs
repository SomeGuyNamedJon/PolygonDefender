using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //make some level preferences
    public static int levelNum = 0;
    public static int lastLevelNum = 1;

    public float prepTime = 120;

    public bool lastLevel = false;

    public void Start(){
        if(levelNum == lastLevelNum){
            lastLevel = true;
        }
    }

    public void Next(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
