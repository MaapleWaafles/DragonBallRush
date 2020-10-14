using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This Script Controls the Animations of the players Combo/Super attacks and the controls assigned to it
    Author: Afridi Rahim
 */

public class CombatManager : MonoBehaviour
{
    [HideInInspector]
    public Animator animator;
    public ColliderController controller;

    // Checks if we are currently using a Light Attack
    public bool isLightAttacking;

    // Checks if we are currently using a Heavy Attack
    public bool isHeavyAttacking;

    // Checks if we are Currently using The First Super Attack
    public bool superOneActive;

    // Prevents the Animations from being interrupted when pressed again
    private bool PressOnce;

    // This is used to pass the current frame into the next frame
    private int lightCounter = 0;
    private int heavyCounter = 0;
    
    // The amount of animations that consist of the light/heavy string
    public int lightAnimations = 0;
    public int heavyAnimations = 0;   
    
    // The maximum amount of time given to the light string before its reset
    private float lightFrame = 90.0f;
    private float maxLightFrame;

    // The maximum amount of time given to the heavy string before its reset
    private float heavyFrame = 90.0f;
    private float maxHeavyFrame;

    // Time used to Decrement the Animations play time
    private const float frameTime = 1f;

    void Awake()
    {
        // Intialize
        animator = GetComponent<Animator>();
        controller = GetComponent<ColliderController>();
        maxLightFrame = lightFrame;
        maxHeavyFrame = heavyFrame;
        PressOnce = false;
        isLightAttacking = false;
        isHeavyAttacking = false;
    }

    void Update()
    {
        // If we have started the light string
        if (isLightAttacking == true)
        {
            // Begin the countdown
            lightFrame -= frameTime;
        }
        LightStringEnd();
        LightString();

        // If we have started the heavy string
        if (isHeavyAttacking == true)
        {
            // Begin the countdown
            heavyFrame -= frameTime;
        }
        HeavyStringEnd();
        HeavyString();

        SuperOne();
    }

    // Controls the start of The Light Combo 
    public void LightString()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isLightAttacking = true;

            // If we have started the Light String
            if (lightFrame > 0)
            {
                if (isLightAttacking && !PressOnce)
                {
                    // Increments the Counter and adds more time 
                    lightCounter += 1;
                    lightFrame += 20;
                    animator.SetInteger("lightCounter", lightCounter);
                    PressOnce = true;
                }
            }

            // If we reached the max amount of animations
            if (lightCounter == lightAnimations + 1)
            {
                // Reset
                lightCounter = 0;
                animator.SetInteger("lightCounter", 0);
                isLightAttacking = false;
            }
        }
        else
        {
            PressOnce = false;
        }
    }

    // Controls the end of the Light Combo
    public void LightStringEnd()
    {
        // If our Light string has collapsed
        if (lightFrame <= 0)
        {
            // Reset
            animator.SetInteger("lightCounter", 0);
            lightCounter = 0;
            lightFrame = maxLightFrame;
            isLightAttacking = false;
            PressOnce = false;

            // Disables any Colliders left on
            foreach (Collider2D coll in controller.lightColliders)
            {
                coll.gameObject.SetActive(false);
            }
        }

        // Resets the frame
        if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            lightFrame = maxLightFrame;
        }
    }


    // Controls the start of the Heavy Combo
    public void HeavyString()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isHeavyAttacking = true;

            // If we have started the Heavy String
            if (heavyFrame > 0)
            {
                if (isHeavyAttacking && !PressOnce)
                {
                    // Increments the Counter and Adds more time
                    heavyCounter += 1;
                    heavyFrame += 20;
                    animator.SetInteger("heavyCounter", heavyCounter);
                    PressOnce = true;
                }
            }
            
            // If we Reach the max amout of animations
            if (lightCounter == lightAnimations + 1)
            {
                // Reset 
                lightCounter = 0;
                animator.SetInteger("heavyCounter", 0);
                isHeavyAttacking = false;
            }
        }
        else
        {
            PressOnce = false;
        }

    }

    // Controls the end of the Heavy Combo
    public void HeavyStringEnd()
    {
        // If our Heavy String has collapsed
        if (heavyFrame <= 0)
        {

            // Reset
            animator.SetInteger("heavyCounter", 0);
            heavyCounter = 0;
            heavyFrame = maxHeavyFrame;
            isHeavyAttacking = false;
           
            // Disables any Colliders left on
            foreach (Collider2D coll in controller.heavyColliders)
            {
                coll.gameObject.SetActive(false);
            }
        }

        // Resets the frame time
        if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            heavyFrame = maxHeavyFrame;
        }
    }


    // Controls the start of the First Super Attack
    public void SuperOne()
    {
        if (Input.GetKey(KeyCode.O) && Input.GetKey(KeyCode.P))
        {
            // if we have already started dont go again
            if (superOneActive) return;
            superOneActive = true;

            // Sets the animation to play
            if (superOneActive)
            {
                animator.SetBool("s1Active", true);
            }
        }
    }

    // Controls the end of the First Super Attack
    public void SuperOneEnd()
    {
        StartCoroutine(WaitForS1());
    }

    IEnumerator WaitForS1()
    {
        // Waits Until the Animation finishes
        yield return new WaitForSeconds(0.2f);
        
        // Reset once finished
        superOneActive = false;
        animator.SetBool("s1Active", false);
        animator.SetBool("playerHit", false);
    }
}
