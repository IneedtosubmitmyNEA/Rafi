using System;
using System.Numerics;
using JetBrains.Annotations;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.Rendering;

public class move3dcamera : MonoBehaviour
{
    public LayerMask whatIsWall;
    [Header("Sensitivity")]
    public float Xsensitivity;
    public float Ysensitivity;
    public Transform HitBox;
    public Transform Center;
    public Rigidbody rb;
    public float Radius;
    
    float mouseX;
    float mouseY;
    float YLoca;
    float XLoca;
    float ZLoca;

    float alpha=0; //  this is the angle on the x,z grid
    float beta=0; // this is the angle between the y axis and the x,z plane 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HitBox.rotation=UnityEngine.Quaternion.Euler(0,270,0);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }
    

    void Update()
    {
        MouseInput();
        MoveCamera();
        RotateCamera();
        HitBox.position= new UnityEngine.Vector3(Convert.ToSingle(XLoca)+Center.position.x,Convert.ToSingle(YLoca)+Center.position.y,Convert.ToSingle(ZLoca)+Center.position.z);
        // the position of where the camera should go
    }


    private void MouseInput()// gets mouse inputs
    {
        mouseX = Input.GetAxisRaw("Mouse X") * Xsensitivity*Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * Ysensitivity*Time.deltaTime ; 
    }

    private void MoveCamera()
    {
        alpha+=mouseX;
        if(alpha>360 || alpha<-360)//makes it so that alpha doesn't become really big also || means logical or
        {alpha=0;}
        float alphaRad = alpha*Convert.ToSingle(Math.PI)/180;
        
        beta+=mouseY;
        beta=Mathf.Clamp(beta,-90f,90f);//need to make sure that the most the player can look up is 90 degrees (makes the sensitivity also higher cause trig is in radians)
        float betaRad = beta*Convert.ToSingle(Math.PI)/180;

        YLoca = Radius * Convert.ToSingle(Math.Sin(betaRad));
        ZLoca = Radius * Convert.ToSingle(Math.Cos(betaRad) * Math.Sin(alphaRad));
        XLoca = Radius * Convert.ToSingle(Math.Cos(betaRad) * Math.Cos(alphaRad));
        
       
    }

    private void RotateCamera()// uses previously caculated values to change the players orientation and the rotaion of the camera
    {

        // rotate cam and orientation
        transform.rotation = UnityEngine.Quaternion.Euler(beta, -alpha+270, 0);
        Center.rotation = UnityEngine.Quaternion.Euler(0, -alpha+270, 0);
    }
    
}