using UnityEngine;

public class TargetHealth : MonoBehaviour
{
    public float health = 500f;
    private float maxHP;
    public Color nextColor;
    public Color fullHP;
    public Color medHP;
    public Color lowHP;
    private Material targetMat;
    public GameManager manager;

    public void Awake(){
        targetMat = gameObject.GetComponent<Renderer>().material;
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        nextColor = fullHP;
        maxHP = health;
    }

    public void TakeDamage(float damage){
        health -= damage;

        if(health > 300)
            nextColor = Color.Lerp(nextColor, medHP, damage / (maxHP - 200));
        if(health <= 325 && health >= 300)
            nextColor = medHP;
        
        if(health < 300)
            nextColor = Color.Lerp(nextColor, lowHP, damage / 200);

        targetMat.SetColor("_EmissionColor",nextColor);
        if(health <= 0){
            Die();
        }
    }

    public void Die(){
        //TODO: Make Target Explode
        targetMat.SetColor("_EmissionColor",lowHP);
        manager.targetDead = true;
    }
    
}
