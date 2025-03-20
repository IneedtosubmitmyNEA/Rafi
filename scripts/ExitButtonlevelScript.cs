using UnityEngine;

public class ExitButtonlevelScript : MonoBehaviour
{
    public GameObject Player;
    public GameObject LevelUpUI;
    public void OnClick()// code for the x on the top right of the upgrade menu
    {
        Player.GetComponent<LevelAreaScript>().isLeveling=false;
        LevelUpUI.SetActive(false);
    }
}
