using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This Script Manages the Damages Done to the opposingplayer 
    Author: Afridi Rahim
 */

public class DamageController : MonoBehaviour
{
    // Light and Heavy Attack Damages
    public int lightAttackDmg;
    public int heavyAttackDmg;

    // Supers One, Two, Three and Four Damages
    public int SuperOneDmg;
    public int SuperTwoDmg;
    public int SuperThreeDmg;
    public int SuperFourDmg;

    public PlayerController player;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the Light Collider hits the opposing Player
        if (gameObject.tag == "LA" && collision.tag == "Temp Player")
        {
            // If we aren't guarding, take damage
            if (!player.isGuarding)
                player.Health -= lightAttackDmg;
        }
        // Or Else if the Heavy Collider hits the opposing Player
        else if (gameObject.tag == "HA" && collision.tag == "Temp Player")
        {
            // If we aren't guarding, take damage
            if (!player.isGuarding)
                player.Health -= heavyAttackDmg;
        }
        // Or Else if the Super Attack one hits the opposing Player
        else if (gameObject.tag == "S1" && collision.tag == "Temp Player")
        {
            // Sets true to transition to the animation
            if (anim != null)
                anim.SetBool("playerHit", true);

            // Reduces Damage if guarding 
            if (player.isGuarding)
                player.Guard(SuperOneDmg, 25, player);
            else
                player.Health -= SuperOneDmg;
        }
    }
}