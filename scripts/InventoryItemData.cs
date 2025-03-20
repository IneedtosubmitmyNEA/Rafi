using UnityEngine;
[CreateAssetMenu(menuName = "Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string displayName;
    public Sprite image;
    public int damage;
    public float health;
    public float amount;
    public bool isWeapon=true;
    public bool isHealthItem=false;
    public bool isConsumable=false;
    public bool isMain=false;
    public GameObject prefab;//item 3d model


}
