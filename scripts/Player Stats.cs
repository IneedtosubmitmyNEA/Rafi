using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int level=1;// change both in editor
    public int gold=1000;// for testing purposes
    public int levelCost=100;//change in editor
    public int strength=50;// chnge this also in the editor
    public float healthMAX;
    public float health;
    public float staminaMax;
    public float stamina;
    public Vector3 respawn = new Vector3 (0,1,0);//makes the player respawn here if a respawn pos is not set
    public int deathItemsUsed;
    void Awake()
    {
        stamina=staminaMax;
        health=healthMAX;// makes the players start with max stats
    }
}
