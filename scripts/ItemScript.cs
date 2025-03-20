using UnityEngine;

public class ItemScript : MonoBehaviour
{
   public InventoryItemData item;

    void Start()
    {
        if (item!=null)// used to check if the item has been generated correclty
        {
            Debug.Log($"Item name:{item.displayName}, Item damage:{item.damage}, Item isWeapon:{item.isWeapon}, Item isHealthItem:{item.isHealthItem}");
        }
    }
}
