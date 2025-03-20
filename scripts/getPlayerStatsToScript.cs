using TMPro;
using UnityEngine;

public class getPlayerStatsToScript : MonoBehaviour
{
    public PlayerStats playerstats;
    public TMP_Text text;

    void Update()// inventory menu which shows the players stats
    {
        text.text = $"Player level: {playerstats.level}\nPlayer's Health: {playerstats.health}\nPlayer's stamina: {playerstats.stamina}\nPlayer's Maximum Health: {playerstats.healthMAX}\nPlayer's Maximum Stamina: {playerstats.staminaMax}\nPlayers strength: {playerstats.strength}\nPlayer's Gold: {playerstats.gold}\nDeath items used:{playerstats.deathItemsUsed}";
    }
}
