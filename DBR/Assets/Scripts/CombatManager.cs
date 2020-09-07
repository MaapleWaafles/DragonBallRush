using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private const float frameTime = 1f;
    
    private Animator animator;
    public ColliderController controller;

    public bool isLightAttacking;
    public bool isHeavyAttacking;

    private bool PressOnce;

    private int lightCounter = 0;
    public int lightAnimations = 0;

    private int heavyCounter = 0;
    public int heavyAnimations = 0;

    private float lightFrame = 90.0f;
    private float maxLightFrame;

    public float heavyFrame = 90.0f;
    private float maxHeavyFrame;

    void Awake()
    {
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
        if (isLightAttacking == true)
        {
            lightFrame -= frameTime;
        }
        LightStringEnd();
        LightString();

        if (isHeavyAttacking == true)
        {
            heavyFrame -= frameTime;
        }
        HeavyStringEnd();
        HeavyString();
    }

    public void LightString()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isLightAttacking = true;

            if (lightFrame > 0)
            {
                if (isLightAttacking && !PressOnce)
                {
                    lightCounter += 1;
                    lightFrame += 20;
                    animator.SetInteger("lightCounter", lightCounter);
                    PressOnce = true;
                }
            }
            if (lightCounter == lightAnimations + 1)
            {
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

    public void LightStringEnd()
    {
        if (lightFrame <= 0)
        {
            animator.SetInteger("lightCounter", 0);
            lightCounter = 0;
            lightFrame = maxLightFrame;
            isLightAttacking = false;
            PressOnce = false;
            foreach (Collider2D coll in controller.lightColliders)
            {
                coll.gameObject.SetActive(false);
            }
            foreach (Collider2D coll in controller.heavyColliders)
            {
                coll.gameObject.SetActive(false);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            lightFrame = maxLightFrame;
        }
    }

    public void HeavyString()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isHeavyAttacking = true;

            if (heavyFrame > 0)
            {
                if (isHeavyAttacking && !PressOnce)
                {
                    heavyCounter += 1;
                    heavyFrame += 20;
                    animator.SetInteger("heavyCounter", heavyCounter);
                    PressOnce = true;
                }
            }
            if (lightCounter == lightAnimations + 1)
            {
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

    public void HeavyStringEnd()
    {
        if (heavyFrame <= 0)
        {
            animator.SetInteger("heavyCounter", 0);
            heavyCounter = 0;
            heavyFrame = maxHeavyFrame;
            isHeavyAttacking = false;
            foreach (Collider2D coll in controller.lightColliders)
            {
                coll.gameObject.SetActive(false);
            }
            foreach (Collider2D coll in controller.heavyColliders)
            {
                coll.gameObject.SetActive(false);
            }
        }

        if (animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime)
        {
            heavyFrame = maxHeavyFrame;
        }
    }
}
