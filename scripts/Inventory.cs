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
        }                           ///checks if the player can open inventory or not
        else
            HideInventory();
        changeEquippedOnHUD(inventory[whereIsMain]);// code to make add the main item to the hotbar
        useHealthItem();
        takeDamage();
        death();

    }
    void canOpenInventory()
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
        playerInventory.SetActive(true);
        PlayerHud.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideInventory()
    {
        playerInventory.SetActive(false);
        PlayerHud.SetActive(true);
        if (!levelAreaScript.isLeveling)
        {Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;}
    }
    //to get the weapon that the player has as their main weapon
    
    public void AddToInventory(InventoryItemData item, TMP_Text text, GameObject SwapToMainButton, GameObject UseItemButton, Inventory inv)
    {
        if(inventory != null)
        {
            for (int i = 0; i < inventorySlots.Length; i++)
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

    void SpawnNewItem(InventoryItemData item, InventorySlot slot, TMP_Text text, GameObject SwapToMainButton, GameObject UseItemButton, Inventory inv)
    {
        GameObject newItemGo = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(item);
        newItemGo.GetComponent<InventoryItem>().text=text;
        newItemGo.GetComponent<InventoryItem>().SwapToMain=SwapToMainButton;
        newItemGo.GetComponent<InventoryItem>().UseItem=UseItemButton;
        newItemGo.GetComponent<InventoryItem>().Inv=inv;
    }

    void EquippedOnHud(InventoryItemData item)
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
        if(inventory != null && mainSlots[0].GetComponentInChildren<InventoryItem>() == null && item.isMain)
        {
            UnityEngine.Debug.Log("long if statement has been triggered triggered");
            EquippedOnHud(item);
        }
        else if(inventory !=null && !item.isMain)
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

    public void useHealthItem()
    {
        if(Input.GetKeyDown(UseHealthItemKey) && inventory[1].amount>0 && playerStats.health<playerStats.healthMAX)
        {    
            inventory[1].amount-=1;
            playerStats.health+=inventory[1].health;
            text.text = $"{inventory[1].displayName}\nHealth restored: {inventory[1].health}\nAmount: {inventory[1].amount}";
            HealthItemRemainingText.text = $"{inventory[1].amount}";
            if (playerStats.health>playerStats.healthMAX)//makes sure that the players maximum health does not get exceeded
            {playerStats.health=playerStats.healthMAX;}
        }    
    }

    public void takeDamage()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {playerStats.health-=10;}
    }

    public void death()
    {
        if (playerStats.health<=0)
        {
            isDeath=true;
            deathscreen.SetActive(true);
            inventory[2].amount+=1;
            canOpen=false;
            playerStats.health=0.001f;
            Invoke("respawn",2.5f);
        }
    }

    public void respawn()
    {
        isDeath=false;
        deathscreen.SetActive(false);
        playerStats.health=playerStats.healthMAX;
        playerStats.stamina=playerStats.staminaMax;
        inventory[1].amount=5;
        transform.position=playerStats.respawn;
    }

}
