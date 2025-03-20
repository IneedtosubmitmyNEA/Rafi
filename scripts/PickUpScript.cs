using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PickUpScript : MonoBehaviour
{
    public KeyCode Interact = KeyCode.E;
    public Inventory Inv;
    public TMP_Text text;
    public GameObject SwapToMain;
    public GameObject UseItem;
    public GameObject weaponHolder;// this is where the item is held
    public InventoryItemData Starting_weapon;
    public InventoryItemData Health_Item;
    public InventoryItemData deathItem;
    private Collider PickupableItem;

    void Start()//addes teh starting weapon then health item then death item to inventory
    {
        Inv.AddToInventory(Starting_weapon, text, SwapToMain, UseItem, Inv);
        Inv.inventory.Add(Starting_weapon);
        Inv.AddToInventory(Health_Item, text, SwapToMain, UseItem, Inv);
        Inv.inventory.Add(Health_Item);
        Inv.AddToInventory(deathItem, text, SwapToMain, UseItem, Inv);
        Inv.inventory.Add(deathItem);
        Health_Item.amount=5; // makes sure that the health item amount resets
        deathItem.amount=0; // makes sure that at the start of the game the player is at 0 of these items
        GameObject newItemGo = Instantiate(Inv.inventoryItemPrefab, Inv.mainSlots[1].transform);
        InventoryItem inventoryItem = newItemGo.GetComponent<InventoryItem>();
        inventoryItem.InitialiseItem(Inv.inventory[1]); //places the healing item sprite on the bottom right square on the HUD
        
        // makes the player equip the weapon
        GameObject Inhand = Instantiate(Inv.inventory[Inv.whereIsMain].prefab,weaponHolder.transform.position,quaternion.identity);
        Inhand.transform.SetParent(weaponHolder.transform);
        Inhand.transform.rotation=weaponHolder.transform.rotation;// places the sword in the weapon holder object
    }
    void OnTriggerEnter(Collider other) // entering the item hitbox
    {
        if(other.gameObject.tag=="PickUpable")
        {
            PickupableItem=other;
        }
    }

    void OnTriggerExit(Collider other)//leaving the items hitbox
    {
        if(other == PickupableItem)
        {
            PickupableItem = null;
        }
    }

    void Update()
    {
        if (PickupableItem != null && Input.GetKeyUp(Interact) && !Inv.isDeath)
        {
            //get the gameobject it collided with then gets the items script and then the scriptable object
            Inv.inventory.Add(PickupableItem.gameObject.GetComponent<ItemScript>().item); 
            Inv.AddToInventory(PickupableItem.gameObject.GetComponent<ItemScript>().item, text, SwapToMain, UseItem, Inv);
            Destroy(PickupableItem.gameObject);// then removes it from the scene
            print("item is gone");//for debug purposes
            PickupableItem = null;//so no errors happen
        }
    }

} 

