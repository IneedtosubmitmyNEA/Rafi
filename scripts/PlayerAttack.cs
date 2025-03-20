using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject WeaponHolder;
    Animator Animator;
    public KeyCode AttackBind = KeyCode.Mouse0;
    public bool isAttacking;
    public PlayerStats playerStats;
    public Inventory inventory;
    Collider enemy;

    void Start()
    {
        Animator= WeaponHolder.GetComponent<Animator>();
    }
    void AttackAnimaton()
    {
        if (Input.GetKeyDown(AttackBind) && !inventory.isDeath && playerStats.stamina>20)//plays the animation and then reduces stamina by 20
        {
            Animator.SetTrigger("Play Attack");
            playerStats.stamina-=20;
            isAttacking=true;// also makes isAttacking true
        }
    }

  
    void Update()
    {
        AttackAnimaton();
        if(isAttacking)
        {attack();}// makes the attack only triger once
    }

    void OnTriggerEnter(Collider other) // entering the enemies hitbox
    {
        if(other.gameObject.tag=="Enemy")
        {
            enemy=other;
        }
    }

    void OnTriggerExit(Collider other)//leaving the enemies hitbox
    {
        if(other == enemy)
        {
            enemy = null;
        }
    }

    void attack()
    {   
        if(enemy!=null)
        {
            enemy.gameObject.GetComponent<EnemyStatsScript>().health-=Mathf.RoundToInt(inventory.inventory[inventory.whereIsMain].damage + playerStats.strength*0.5f);
            isAttacking=false;//damages the enemy and then sets isAttacking to false.
        

        if(enemy.gameObject.GetComponent<EnemyStatsScript>().health<0)
        {
            Destroy(enemy.GameObject());
            playerStats.gold+=enemy.gameObject.GetComponent<EnemyStatsScript>().goldDrop;// removes the gameObject from the scene and adds to the players gold
        }
        }
    }

    
}
