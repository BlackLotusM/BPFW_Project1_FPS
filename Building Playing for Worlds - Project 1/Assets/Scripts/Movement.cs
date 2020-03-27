    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float jumpHeight = 2f;
    public bool jetpack = false;

    Vector3 velocity;
    bool isGrounder;

    public float jetpacktimer;
    public int jetpackmaxfuel;

    public float staminaTimer;
    public int maxStamina;

    public Image ManaChange;


    public bool used = false;
    // Update is called once per frame
    void Update()
    {
        isGrounder = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounder)
        {
            jetpack = false;
            jetpacktimer = 0;
        }
        else 
        if(isGrounder == false && jetpacktimer <= jetpackmaxfuel)
        {
            jetpack = true;
        }
        else
        {
            jetpack = false;
        }

        if(isGrounder && velocity.y < 0)
        {
            velocity.y = -2f;
        }


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButton("Jump") && isGrounder == false)
        {
            if (jetpack)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                jetpacktimer += Time.deltaTime;
                
            }
            
        }
        ManaChange.fillAmount = jetpacktimer / 4;
        if (Input.GetButtonDown("Jump") && isGrounder == true)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
