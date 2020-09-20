using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float damage;

    public void StartBlasting(bool facingLeft)
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

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

        Destroy(gameObject, 5f);
    }

   void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Temp Player")
        {
            Destroy(gameObject);
        }
    }
}
