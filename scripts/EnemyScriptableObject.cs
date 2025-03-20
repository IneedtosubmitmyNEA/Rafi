using UnityEngine;
[CreateAssetMenu(menuName = "Enemy Stats")]
public class EnemyScriptableObject : ScriptableObject
{
    public int Health;
    public InventoryItemData inventoryItemData;
    public int damage;
    public int goldDrop = 50;

}
