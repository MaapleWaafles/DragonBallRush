using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public List<Collider2D> lightColliders;
    public List<Collider2D> heavyColliders;
    public List<Collider2D> S1Colliders;
    public PlayerController player;


    #region Light Combo Colliders

    public void LS_Enable(int count)
    {
        if (lightColliders[count].gameObject != null)
        {
            lightColliders[count].gameObject.SetActive(true);
        }
    }

    public void LS_Disable(int count)
    {
        if (lightColliders[count].gameObject != null)
        {
            lightColliders[count].gameObject.SetActive(false);
        }
    }

    #endregion
   

    #region Heavy Combo Colliders

    public void HS_Enable(int count)
    {
        if (heavyColliders[count].gameObject != null)
        {
            heavyColliders[count].gameObject.SetActive(true);
        }
    }

    public void HS_Disable(int count)
    {
        if (heavyColliders[count].gameObject != null)
        {
            heavyColliders[count].gameObject.SetActive(false);
        }
    }

    #endregion


    #region Supers

    void S1_Enable(int count)
    {
        if (S1Colliders[count].gameObject != null)
        {
            S1Colliders[count].gameObject.SetActive(true);
        }
    }

    void S1_Disable(int count)
    {
        if (S1Colliders[count].gameObject != null)
        {
            S1Colliders[count].gameObject.SetActive(false);
        }
    }

    #endregion


    #region Character Specials

    void GogetaS1_Check()
    {
        if (player != null)
        {
            CombatManager cmbt = player.GetComponent<CombatManager>();
            if (!cmbt.animator.GetBool("playerHit"))
            {
                cmbt.superOneActive = false;
                cmbt.animator.SetBool("s1Active", false);
                cmbt.animator.SetBool("playerHit", false);
            }
        }
    }

    #endregion
}
