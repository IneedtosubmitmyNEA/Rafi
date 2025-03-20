using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UIElements;

public class InventoryItem : MonoBehaviour
{
    //public Inventory inventory;
    public InventoryItemData item;
    public UnityEngine.UI.Image image;
    public Inventory Inv;
    public TMP_Text text;
    public GameObject SwapToMain;
    public GameObject UseItem;
    public void InitialiseItem(InventoryItemData newItem)
    {
        item = newItem;
        image.sprite = newItem.image;//changes item and sprite based on parameters
    }

    public void changeText()
    {
        Debug.Log("Button is being pressed");
        if(item.isWeapon)
        {text.text = $"{item.displayName}\nWeapon damage: {item.damage}";}// updates the text box bottom left of the inventory
        else if (item.isConsumable)
        {text.text = $"{item.displayName}\nHealth restored: {item.health}\nAmount: {item.amount}";}// updates the text box bottom left of the inventory
    }
    public void buttonchoice()
    {
        if (item.isWeapon)// shows equip button
        {
            SwapToMain.SetActive(true);
            SwapToMain.GetComponent<SwapToMainScript>().Inv=Inv;
            SwapToMain.GetComponent<SwapToMainScript>().item=item;
            UseItem.SetActive(false);
        }
        else if (item.isConsumable)//shows use item button
        {
            UseItem.SetActive(true);
            SwapToMain.SetActive(false);
            UseItem.GetComponent<UseItemScript>().Inv=Inv;
            UseItem.GetComponent<UseItemScript>().item=item;
        }
    }
}
