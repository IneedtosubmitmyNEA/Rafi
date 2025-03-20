using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LevelAreaScript : MonoBehaviour
{
    public bool isLeveling;
    public Collider LevelArea;
    public Inventory Inv;// get the canOpen bool 
    public PickUpScript pickUpScript;
    public GameObject LevelUpUI;
    public PlayerStats playerStats;
    public TMP_Text GoldCostAndGoldText;
    void OnTriggerEnter(Collider other) // entering the flag/levelUpArea hitbox
    {
        if(other.gameObject.tag=="Level Up Area")
        {
            LevelArea=other;
        }
    }
    void OnTriggerExit(Collider other)//leaving the flag/levelUpArea hitbox
    {
        if(other == LevelArea)
        {
            LevelArea = null;
        }
    }

    void Update()
    {

        if (LevelArea != null && Input.GetKeyUp(pickUpScript.Interact) && !Inv.isDeath)
        {
            playerStats.respawn=LevelArea.gameObject.transform.GetChild(8).position;
            isLeveling=true;
            Inv.canOpen=false;//makes sure the player cannot open their inventory
            LevelUpUI.SetActive(true);//shows the levelUp UI
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;//shows the mouse and lets it move around
            GoldCostAndGoldText.text=$"Gold amount:{playerStats.gold}  Level Cost:{playerStats.levelCost}";// shows how much gold and the cost at the top left
            playerStats.health=playerStats.healthMAX;// lets the player regain their health 
            Inv.inventory[1].amount=5;// so the game still feels fair
        }//stamina not included as the player auto regens that
    }

}
