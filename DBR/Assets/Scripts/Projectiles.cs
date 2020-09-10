using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;
    public GameObject location;
    public float damage;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        location.transform.position = location.transform.localPosition;
    }
}
