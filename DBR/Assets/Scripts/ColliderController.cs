using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This Script Controls the Individual Colliders of the players Combos/Super Attacks
    Author: Afridi Rahim
 */

public class ColliderController : MonoBehaviour
{
    // Manages the Light Combo String
    public List<Collider2D> lightColliders;

    // Manages the Heavy Combo String
    public List<Collider2D> heavyColliders;
    
    // Manages the Super Attack One 
    public List<Collider2D> S1Colliders;

    public PlayerController player;


    #region Light Combo Colliders

    // Enables the Specific Light Collider 
    public void LS_Enable(int count)
    {
        if (lightColliders[count].gameObject != null)
        {
            lightColliders[count].gameObject.SetActive(true);
        }
    }

    // Disables the Specific Light Collider
    public void LS_Disable(int count)
    {
        if (lightColliders[count].gameObject != null)
        {
            lightColliders[count].gameObject.SetActive(false);
        }
    }

    #endregion
   

    #region Heavy Combo Colliders

    // Enables the Specific Heavy Collider
    public void HS_Enable(int count)
    {
        if (heavyColliders[count].gameObject != null)
        {
            heavyColliders[count].gameObject.SetActive(true);
        }
    }

    // Disables the Specific Heavy Collider
    public void HS_Disable(int count)
    {
        if (heavyColliders[count].gameObject != null)
        {
            heavyColliders[count].gameObject.SetActive(false);
        }
    }

    #endregion


    #region Supers

    // Enables The Specific Super One Collider
    void S1_Enable(int count)
    {
        if (S1Colliders[count].gameObject != null)
        {
            S1Colliders[count].gameObject.SetActive(true);
        }
    }

    // Disables The Specific Super One Collider
    void S1_Disable(int count)
    {
        if (S1Colliders[count].gameObject != null)
        {
            S1Colliders[count].gameObject.SetActive(false);
        }
    }

    #endregion


    // This Manages the Animations of Characters that need Unique Functions
    #region Character Specials

    // Checks if Character ""Gogeta" Has hit anything with his Super One
    void GogetaS1_Check()
    {
        if (player != null)
        {
            CombatManager cmbt = player.GetComponent<CombatManager>();
            if (!cmbt.animator.GetBool("playerHit"))
            {
                // If False Turn these off
                cmbt.superOneActive = false;
                cmbt.animator.SetBool("s1Active", false);
                cmbt.animator.SetBool("playerHit", false);
            }
        }
    }

    #endregion
}
