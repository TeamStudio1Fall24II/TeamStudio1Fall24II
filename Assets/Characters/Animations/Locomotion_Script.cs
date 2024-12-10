using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion_Script : MonoBehaviour
{
    Animator animator;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2.0f;
    public float deceleration = 2.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // search the gameobject this script is attached to and get the animator component
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // get key input from player
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey("left shift");

        // set current maxVelocity
        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;

        // if player presses forward, increase velocity in z dirwction
        if (forwardPressed && velocityZ < currentMaxVelocity)
        {
            velocityZ += Time.deltaTime * acceleration;
        }

        // increase velocity in left direction
        if (leftPressed && velocityX > -currentMaxVelocity)
        {
            velocityX -= Time.deltaTime * acceleration;
        }

        // increase valocity in left direction
        if (rightPressed && velocityX < currentMaxVelocity)
        {
            velocityX += Time.deltaTime * acceleration;
        }

        // decrease velocityZ
        if (!forwardPressed && velocityZ > 0.0f)
        {
            velocityZ -= Time.deltaTime * deceleration;
        }

        // reset velocityZ
        if (!forwardPressed && velocityZ < 0.0f)
        {
            velocityZ = 0.0f;
        }

        // increase velocityX if left is not pressed and velocityX < 0
        if (!leftPressed && velocityX < 0.0f)
        {
            velocityX += Time.deltaTime * deceleration;
        }

        // increase velocityX if right is not pressed and velocityX > 0
        if (!rightPressed && velocityX > 0.0f)
        {
            velocityX -= Time.deltaTime * deceleration;
        }

        // reset velocityX
        if (!leftPressed && !rightPressed && velocityX != 0.0f && (velocityX > -0.05f && velocityX < 0.05f))
        {
            velocityX = 0.0f;
        }

        // lock forward
        if (forwardPressed && runPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ = currentMaxVelocity;
        }
        // decelerate to the maximum walk velocity
        else if (forwardPressed && velocityZ > currentMaxVelocity)
        {
            velocityZ -= Time.deltaTime * deceleration;
            if (velocityZ > currentMaxVelocity && velocityZ < (currentMaxVelocity + 0.05));
            {
                velocityZ = currentMaxVelocity;
            }
        }
        // round to the currentMaxVelocity if within offset
        else if (forwardPressed && velocityZ < currentMaxVelocity && velocityZ > (currentMaxVelocity - 0.05f))
        {
            velocityZ = currentMaxVelocity;
        }

        // set the parameters to our local variable values
        animator.SetFloat("Velocity Z", velocityZ);
        animator.SetFloat("Velocity X", velocityX);

    }
}
