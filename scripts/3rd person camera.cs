using UnityEngine;

public class thirdPersonCamera : MonoBehaviour
{
    public Transform cameraLocation; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = cameraLocation.position;
        transform.rotation = cameraLocation.rotation;// moves the camera to where the code in move 3d camera calculated
    }
}
