using TMPro;
using UnityEngine;

public class LevelUpButtonsScripts : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerHUDScript playerHUD;
    public TMP_Text GoldCostAndGoldText;
    public void levelStrength()
    {
        if (playerStats.levelCost<playerStats.gold)
        {
            playerStats.strength+=20;//increase stat by coresponding amount
            playerStats.gold-=playerStats.levelCost;//takes the gold away
            playerStats.levelCost+=10*playerStats.level;//increases the cost
            playerStats.level+=1;
            GoldCostAndGoldText.text=$"Gold amount:{playerStats.gold}  Level Cost:{playerStats.levelCost}";// updates what the gold amount says in the level up UI.
        }
    }
    public void levelHealth()
    {
        if (playerStats.levelCost<playerStats.gold)
        {
            playerStats.healthMAX+=20;//increase stat by coresponding amount
            playerStats.gold-=playerStats.levelCost;//takes the gold away
            playerStats.levelCost+=10*playerStats.level;//increases the cost
            playerHUD.MaxHWidth+=10;// makes the bar longer
            playerStats.level+=1;
            playerStats.health=playerStats.healthMAX; // heals the player back to full
            GoldCostAndGoldText.text=$"Gold amount:{playerStats.gold}  Level Cost:{playerStats.levelCost}";
        }
    }

    public void levelStamina()
    {
        if (playerStats.levelCost<playerStats.gold)
        {
            playerStats.staminaMax+=20;//increase stat by coresponding amount
            playerStats.gold-=playerStats.levelCost;//takes the gold away
            playerStats.levelCost+=10*playerStats.level;//increases the cost
            playerHUD.MaxSWidth+=10; //makes the bar longer
            playerStats.level+=1;
            playerStats.stamina=playerStats.staminaMax;// takes the players stamina back to full
            GoldCostAndGoldText.text=$"Gold amount:{playerStats.gold}  Level Cost:{playerStats.levelCost}";
        }
    }
}
