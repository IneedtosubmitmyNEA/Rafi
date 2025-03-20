using System.Collections;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class DodgeScript : MonoBehaviour
{
    public GameObject playerObj;
    Animator Animator;
    public KeyCode DodgeBind = KeyCode.Q;
    public bool isdodging;
    bool onCooldown;
    public Inventory inventory;
    public PlayerStats playerStats;

    void Start()
    {
        Animator= playerObj.GetComponent<Animator>();
    }


    IEnumerator PlayDodgeAnimationAndWait()
    {
        if (!isdodging && !onCooldown)
        // Play animation
        {Animator.SetTrigger("Dodge");
        playerStats.stamina-=20;
        isdodging=true;
        onCooldown=true;
        }

        // Wait for 4 seconds
        yield return new WaitForSeconds(4f);

        isdodging=false;// make sure that the player is vulnerable
        //waits for another 4 seconds
        yield return new WaitForSeconds(4f);
        onCooldown=false;// lets the player dodge again

    }
  
    void Update()
    {   if(Input.GetKeyDown(DodgeBind))
        {StartCoroutine(PlayDodgeAnimationAndWait());}// runs the method above
    }


}
