using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;

public class PlayerMovement : MonoBehaviour
{
    public LevelAreaScript levelAreaScript;
    public Inventory Inv;
    public PlayerAttack playerAttack;
    public DodgeScript dodgeScript;
    [Header("Movement")]
    public float Speed;
    public float SpSpeed;
    public float groundDrag;
    public float airDrag;
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool canJump = true;

    [Header("keybinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;
    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    [Header("Player Stats")]
    public PlayerStats playerStats;
    //stamina
    public float staminaGain;
    bool canGainStamina;
    float timer = 0;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        Time.fixedDeltaTime = 1f / 60f;
        rb.linearDamping = groundDrag;
    }
    
    private void Update()
    {
        //ground check
        grounded= Physics.Raycast(transform.position,Vector3.down,playerHeight*0.5f+0.2f,whatIsGround);
        if (!levelAreaScript.isLeveling && !Inv.isDeath)
        {    
        MyInput();
        //when to jump
        if(Input.GetKey(jumpKey) && canJump && grounded && playerStats.stamina>=20)
        {
            playerStats.stamina-=20;
            canJump = false;
            canGainStamina=false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }
        }    
        timerTillStamReset();
        regainStamina();
        
    }

    private void FixedUpdate()
    {
       if (grounded)
            MovePlayer();
    }
    private void MyInput()// get the players inputs
    {
        horizontalInput = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        verticalInput = Input.GetAxisRaw("Vertical") * Time.deltaTime;
        
    }

    private void MovePlayer()
    {
        //calculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //if the sprint key is pressed
        if(Input.GetKey(sprintKey) && playerStats.stamina>0)
            {
            rb.AddForce(moveDirection.normalized * SpSpeed * 600f, ForceMode.Force);
            playerStats.stamina -= 10f*Time.deltaTime;
            canGainStamina=false;// makes the player unable to gain stamina if they sprint
            }
        //if it is not pressed
        else
            rb.AddForce(moveDirection.normalized * Speed * 600f , ForceMode.Force);
    }


    private void Jump()// makes the player jump
    {
        //reset y velocity
        rb.linearVelocity = new Vector3(rb.linearVelocity.x,0f,rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce * 60f, ForceMode.Impulse);
    }
    private void ResetJump()// make sure the player can jump again
    {
        canJump = true;
    }
    private void timerTillStamReset()
    {   //code should check if the player has pressed the jump, sprint, dodge and attack binds in 1.5 seconds or else it resets the timer
        if (timer < 1.5f)    
            if (Input.GetKey(sprintKey) || Input.GetKey(jumpKey) || Input.GetKey(playerAttack.AttackBind) || Input.GetKey(dodgeScript.DodgeBind))
                timer=0;
            else
                timer += Time.deltaTime;
        else
            {
            canGainStamina = true;
            timer=0;
            }
    }

    private void regainStamina()
    {   //if conditions are met then stamina will recover automatically
        if (playerStats.stamina < playerStats.staminaMax && grounded && canGainStamina)
            playerStats.stamina += staminaGain * Time.deltaTime;
        // if the players current stamina is greater than the max stamina they can have
        if (playerStats.stamina > playerStats.staminaMax)
            playerStats.stamina = playerStats.staminaMax;
        
    }
}
