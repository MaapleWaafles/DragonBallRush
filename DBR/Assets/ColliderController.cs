using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderController : MonoBehaviour
{
    public List<Collider2D> lightColliders;
    public List<Collider2D> heavyColliders;
    public PlayerController player;

    public int lightAttackDmg;
    public int heavyAttackDmg;

    public int SuperOneDmg;
    public int SuperTwoDmg;
    public int SuperThreeDmg;
    public int SuperFourDmg;

    #region Light Combo Colliders
    public void LS_1Enable()
    {
        if (lightColliders[0].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), lightColliders[0]);
            lightColliders[0].gameObject.SetActive(true);
            if (player.tag == "Temp Player")
            {
                player.Health -= 10;
            }
        }
    }

    public void LS_1Disable()
    {
        if (lightColliders[0].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), lightColliders[0]);
            lightColliders[0].gameObject.SetActive(false);
        }
    }




    public void LS_2Enable()
    {
        if (lightColliders[1].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), lightColliders[1]);
            lightColliders[1].gameObject.SetActive(true);
            if (player.tag == "Temp Player")
            {
                player.Health -= 10;
            }
        }
    }

    public void LS_2Disable()
    {
        if (lightColliders[1].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), lightColliders[1]);
            lightColliders[1].gameObject.SetActive(false);
        }
    }




    public void LS_3Enable()
    {
        if (lightColliders[2].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), lightColliders[2]);
            lightColliders[2].gameObject.SetActive(true);
            if (player.tag == "Temp Player")
            {
                player.Health -= 10;
            }
        }
    }

    public void LS_3Disable()
    {
        if (lightColliders[2].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), lightColliders[2]);
            lightColliders[2].gameObject.SetActive(false);
        }
    }




    public void LS_4Enable()
    {
        if (lightColliders[3].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), lightColliders[3]);
            lightColliders[3].gameObject.SetActive(true);
            if (player.tag == "Temp Player")
            {
                player.Health -= 10;
            }
        }
    }

    public void LS_4Disable()
    {
        if (lightColliders[3].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), lightColliders[3]);
            lightColliders[3].gameObject.SetActive(false);
        }
    }




    public void LS_5Enable()
    {
        if (lightColliders[4].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), lightColliders[4]);
            lightColliders[4].gameObject.SetActive(true);
            if (player.tag == "Temp Player")
            {
                player.Health -= 10;
            }
        }
    }

    public void LS_5Disable()
    {
        if (lightColliders[4].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), lightColliders[4]);
            lightColliders[4].gameObject.SetActive(false);
        }
    }
    #endregion

    #region Heavy Combo Colliders
    void H1_Enable()
    {
        if (heavyColliders[0].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heavyColliders[0]);
            heavyColliders[0].gameObject.SetActive(true);
            if (player.tag == "Temp Player")
            {
                player.Health -= heavyAttackDmg;
            }
        }
    }

    void H1_Disable()
    {
        if (heavyColliders[0].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heavyColliders[0]);
            heavyColliders[0].gameObject.SetActive(false);
        }
    }




    void H2_Enable()
    {
        if (heavyColliders[1].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heavyColliders[1]);
            heavyColliders[1].gameObject.SetActive(true);
            if (player.tag == "Temp Player")
            {
                player.Health -= heavyAttackDmg;
            }
        }
    }

    void H2_Disable()
    {
        if (heavyColliders[1].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heavyColliders[1]);
            heavyColliders[1].gameObject.SetActive(false);
        }
    }




    void H3_Enable()
    {
        if (heavyColliders[2].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heavyColliders[2]);
            heavyColliders[2].gameObject.SetActive(true);
            if (player.tag == "Temp Player")
            {
                player.Health -= heavyAttackDmg;
            }
        }
    }

    void H3_Disable()
    {
        if (heavyColliders[2].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heavyColliders[2]);
            heavyColliders[2].gameObject.SetActive(false);
        }
    }




    void H4_Enable()
    {
        if (heavyColliders[3].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heavyColliders[3]);
            heavyColliders[3].gameObject.SetActive(true);
            if (player.tag == "Temp Player")
            {
                player.Health -= heavyAttackDmg;
            }
        }
    }

    void H4_Disable()
    {
        if (heavyColliders[3].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heavyColliders[3]);
            heavyColliders[3].gameObject.SetActive(false);
        }
    }




    void H5_Enable()
    {
        if (heavyColliders[4].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heavyColliders[4]);
            heavyColliders[4].gameObject.SetActive(true);
            if (player.tag == "Temp Player")
            {
                player.Health -= heavyAttackDmg;
            }
        }
    }

    void H5_Disable()
    {
        if (heavyColliders[4].gameObject != null)
        {
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), heavyColliders[4]);
            heavyColliders[4].gameObject.SetActive(false);
        }
    }


    #endregion


}
