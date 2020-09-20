using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour
{
    public int lightAttackDmg;
    public int heavyAttackDmg;

    public int SuperOneDmg;
    public int SuperTwoDmg;
    public int SuperThreeDmg;
    public int SuperFourDmg;

    public PlayerController player;
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "LA" && collision.tag == "Temp Player")
        {
            player.Health -= lightAttackDmg;
        }
        else if (gameObject.tag == "HA" && collision.tag == "Temp Player")
        {
            player.Health -= heavyAttackDmg;
        }
        else if (gameObject.tag == "S1" && collision.tag == "Temp Player")
        {
            if (anim != null)
                anim.SetBool("playerHit", true);

            player.Health -= SuperOneDmg;
        }
    }
}