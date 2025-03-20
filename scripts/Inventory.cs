using System;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Processors;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public KeyCode OpenInventory = KeyCode.I;
    public KeyCode UseHealthItemKey = KeyCode.R;
    public List<InventoryItemData> inventory = new List<InventoryItemData>();
    //public GameObject health_item;
    public InventorySlot[] inventorySlots;
    public PlayerStats playerStats;
    public HotbarSlots[] mainSlots;// on HUD
    public GameObject inventoryItemPrefab;
    public GameObject PlayerHud;// Overlay UI/canvas
    public GameObject playerInventory;//inventory canvas/UI
    public GameObject SwapToMainButton;
    public GameObject UseItemButton;
    public GameObject deathscreen;
    public bool isDeath;
    public TMP_Text text; //item text box
    public TMP_Text HealthItemRemainingText; //Hud Text Box
    public bool canOpen;
    public LevelAreaScript levelAreaScript;// get the isLeveling bool 
    bool shouldchange;
    public int whereIsMain;



    void Update()
    {
        canOpenInventory();
        if(canOpen)
        {
            RevealInventory();
        }                           ///checks if the player can open inventory UIs or not
        else
            HideInventory();
        changeEquippedOnHUD(inventory[whereIsMain]);// code to make add the main item to the hotbar
        useHealthItem();
        death();

    }
    void canOpenInventory()// check if the player can open their inventory
    {
        if(canOpen && Input.GetKeyUp(OpenInventory) && !levelAreaScript.isLeveling && !isDeath)
        {
            canOpen=false;
        }
        else if (!canOpen && Input.GetKeyUp(OpenInventory) && !levelAreaScript.isLeveling && !isDeath)
        {
            canOpen=true;
        }
    }

    void RevealInventory()
    {
        playerInventory.SetActive(true);//shows invetory UI
        PlayerHud.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideInventory()//shows playerHUD UI
    {
        playerInventory.SetActive(false);
        PlayerHud.SetActive(true);
        if (!levelAreaScript.isLeveling)// if the player is leveling they have the HUD as the background so this allows them to use their mouse
        {Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;}
    }
    //to get the weapon that the player has as their main weapon
    
    public void AddToInventory(InventoryItemData item, TMP_Text text, GameObject SwapToMainButton, GameObject UseItemButton, Inventory inv)//this code makes it so that the item in the inventory list is shown in the UI
    {
        if(inventory != null)
        {
            for (int i = 0; i < inventorySlots.Length; i++)//code to look for children, if theres a child goes to the next item slot, until an item slot with no child is found
            {
                InventorySlot slot = inventorySlots[i];
                InventoryItem itemInSlot = slot.GetComponentInChildren<InventoryItem>();
                if (itemInSlot == null)
                {
                    SpawnNewItem(item, slot, text, SwapToMainButton, UseItemButton, inv);
                    return;
                }

            }
        }
    }

    void SpawnNewItem(InventoryItemData item, InventorySlot slot, TMP_Text text, GameObject SwapToMainButton, GameObject UseItemButton, Inventory inv)// places the item as a child of the free slot and them and then changes the variables /assigns variables
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        newItemGo.GetComponent<InventoryItem>().text=text;
        newItemGo.GetComponent<InventoryItem>().SwapToMain=SwapToMainButton;
        newItemGo.GetComponent<InventoryItem>().UseItem=UseItemButton;
        newItemGo.GetComponent<InventoryItem>().Inv=inv;
    }

    void EquippedOnHud(InventoryItemData item)// similar to the method above except its for a specific index of a different list
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, mainSlots[0].transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
    }

    void changeEquippedOnHUD(InventoryItemData item)
    {
        if(shouldchange)
        {
            UnityEngine.Debug.Log("should change has been triggered");
            EquippedOnHud(item);
            shouldchange=false;
        }
        if(inventory != null && mainSlots[0].GetComponentInChildren<InventoryItem>() == null && item.isMain)//if the first is the main weapon then this triggers
        {
            UnityEngine.Debug.Log("long if statement has been triggered triggered");
            EquippedOnHud(item);
        }
        else if(inventory !=null && !item.isMain)//looks for the main item
        {//searches the list to find what has become the main weapon
            for (int i = 0; i < inventory.Count; i++)
            {
                if(inventory[i].isMain)
                {   
                    if(mainSlots[0].GetComponentInChildren<InventoryItem>() != null)
                    {Destroy(mainSlots[0].transform.GetChild(0).gameObject);}
                    whereIsMain = i;
                    shouldchange=true;
                    break;
                }
                
            }
            }
        
        
    }

    public void useHealthItem()// lets the player consume their health item with a keybind
    {
        if(Input.GetKeyDown(UseHealthItemKey) && inventory[1].amount>0 && playerStats.health<playerStats.healthMAX)
        {    
            inventory[1].amount-=1;
            playerStats.health+=inventory[1].health;
            text.text = $"{inventory[1].displayName}\nHealth restored: {inventory[1].health}\nAmount: {inventory[1].amount}";// updates the text bottom right of the inventory
            HealthItemRemainingText.text = $"{inventory[1].amount}";
            if (playerStats.health>playerStats.healthMAX)//makes sure that the players maximum health does not get exceeded
            {playerStats.health=playerStats.healthMAX;}
        }    
    }


    public void death()
    {
        if (playerStats.health<=0)
        {
            isDeath=true;//in movement code, makes the player unable to move
            deathscreen.SetActive(true);shows the death screen
            inventory[2].amount+=1;// increase the death item amount
            canOpen=false;// does not let the player open the inventory
            playerStats.health=0.001f;// makes sure that this code does not loop but also mkaes the health bar look like its fully gone
            Invoke("respawn",2.5f);// respawn the player after 2.5 seconds
        }
    }

    public void respawn()
    {
        isDeath=false;
        deathscreen.SetActive(false);//hides the death screen
        playerStats.health=playerStats.healthMAX;
        playerStats.stamina=playerStats.staminaMax;// make the player regain their maximum health and stamina
        inventory[1].amount=5;// resets their health item amount
        transform.position=playerStats.respawn;// makes the player telaport to the levelUpArea
    }

}
