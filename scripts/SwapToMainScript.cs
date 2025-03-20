using Unity.Mathematics;
using UnityEngine;

public class SwapToMainScript : MonoBehaviour
{
    public InventoryItemData item;
    public Inventory Inv;
    public GameObject weaponHolder;
    public void OnClick()//places the new weapon in the weapon holder object
    {
        Inv.inventory[Inv.whereIsMain].isMain=false;
        item.isMain=true;
        Destroy(weaponHolder.transform.GetChild(0).gameObject);//gets the first child's gameobject
        GameObject Inhand = Instantiate(item.prefab,weaponHolder.transform.position,quaternion.identity);
        Inhand.transform.SetParent(weaponHolder.transform);
        Inhand.transform.rotation=weaponHolder.transform.rotation;
    }
}
