using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUDScript : MonoBehaviour
{
    public PlayerStats playerStats;
    public RawImage StaminaBar;
    public RawImage MaxStaminaBar;
    public RawImage HealthBar;
    public RawImage MaxHealthBar;
    public TMP_Text goldAmount;
    public TMP_Text HealthItemRemainingText;
    public LevelAreaScript levelAreaScript;
    public Inventory Inv;
    float Swidth;
    float Hwidth;
    public float MaxSWidth = 150;// this will be changed by the level up UI and code is added
    public float MaxHWidth = 200;// this will also be changed when the level up system is added

    void Start()
    {

    }

    void Update()
    {
       MaxStaminaBar.rectTransform.sizeDelta=new Vector2(MaxSWidth,25); //changes the maximum stamina bar
       Swidth=playerStats.stamina * MaxSWidth / playerStats.staminaMax;
       StaminaBar.rectTransform.sizeDelta=new Vector2(Swidth,25); //changes the stamina bar
       MaxHealthBar.rectTransform.sizeDelta=new Vector2(MaxHWidth,25); //changes the maximum health bar
       Hwidth=playerStats.health * MaxHWidth / playerStats.healthMAX;
       HealthBar.rectTransform.sizeDelta=new Vector2(Hwidth,25); //changes the health bar
       HealthItemRemainingText.text = $"{Inv.inventory[1].amount}";
       if(levelAreaScript.isLeveling)
       {
            goldAmount.gameObject.SetActive(false);//makes the gold amount on the HUD disappear if in the level up menu
       }
       else
       {
            goldAmount.gameObject.SetActive(true);//makes the gold amount on the HUD appear if not in the level up menu
       }
       goldAmount.text=$"{playerStats.gold}";//shows the gold amount
    }
}
