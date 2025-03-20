
using UnityEngine;

public class EnemyStatsScript : MonoBehaviour
{
    public EnemyScriptableObject enemystats;
    public int health;
    public Vector3 position;
    public int damage;
    public GameObject player;
    public Enemyscript enemyscript;
    public int goldDrop;
    void Awake()
    {
        health=enemystats.Health;
        damage=enemystats.damage;
        position=transform.position;
        goldDrop=enemystats.goldDrop;//assigns the scriptable object variables to the enemy
        player = GameObject.Find("Player");// saves time when adding multiple enemies
        enemyscript = GetComponent<Enemyscript>(); // gets the enemy ai script
    }
    void Update()
    {
       if (player.GetComponent<Inventory>().isDeath || player.GetComponent<LevelAreaScript>().isLeveling)
       {
            transform.position=position;
            enemyscript.walkPointSet=false;//telaports the enemy back to where they spawned and also makes the Ai generate a new walk point
       } 
    }
}
