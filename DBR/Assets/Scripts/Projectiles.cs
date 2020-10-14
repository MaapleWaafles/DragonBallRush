using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This Script Handles Projectiles that the player produces, including its damage and speed
    Author: Afridi Rahim
 */

public class Projectiles : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float damage;
    
    // Time until projectile is destroyed
    [SerializeField]
    private float destroy;

    // Controls the Direction of the Projectile
    public void StartBlasting(bool facingLeft)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        // Flips according to directionf faced
        if (facingLeft)
        {
            sprite.flipX = true;
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
        }
        else
        { 
            sprite.flipX = false;
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
        }

        // Destroyed after a while
        Destroy(gameObject, destroy);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // If the opposing player is hit
        if (collision.tag == "Temp Player")
        {
            // Terminates projectile
            Destroy(gameObject);
        }
    }
}
