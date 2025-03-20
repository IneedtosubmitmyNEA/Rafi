using TMPro;
using UnityEngine;

public class UseItemScript : MonoBehaviour
{
    public InventoryItemData item;
    public PlayerStats playerStats;
    public Inventory Inv;
    public TMP_Text text;
    public TMP_Text HealthItemRemainingText;

    public void OnClick()
    {   //use health item code here
        if (item.isHealthItem && item.amount>0 && playerStats.health<playerStats.healthMAX)
        {
            item.amount-=1;
            playerStats.health+=item.health;
            text.text = $"{item.displayName}\nHealth restored: {item.health}\nAmount: {item.amount}";//changes the text in both inventory and HUD UI
            HealthItemRemainingText.text = $"{item.amount}";
            if (playerStats.health>playerStats.healthMAX)//makes sure that the players maximum health does not get exceeded
            {playerStats.health=playerStats.healthMAX;}
        }
        //Use death Item code here
        if (!item.isHealthItem && item.amount>0)
        {
            item.amount-=1;
            playerStats.healthMAX+=10;
            playerStats.staminaMax+=10;
            playerStats.strength+=10;//increases all stats by 10
            playerStats.deathItemsUsed+=1;
            text.text = $"{item.displayName}\nHealth restored: {item.health}\nAmount: {item.amount}";//changes the text in both inventory and HUD UI
        }
    }


}
