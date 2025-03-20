using UnityEngine;

public class howtoplayscript : MonoBehaviour
{
    public GameObject self;
    void Start()
    {
        self.SetActive(false);// makes the howtoplay canvas disappear game starts
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            self.SetActive(false);// makes the howtoplay canvas disappear when pressing z
        }
    }
    public void onClick()
    {
        self.SetActive(true);// makes the howtoplay canvas appear when pressing the button
    }
}
