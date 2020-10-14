using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This Script handles everything the Player Can do such as, attacking, walking, jumping etc
    Author: Afridi Rahim
 */

public class PlayerController : MonoBehaviour
{
    private Animator animator;
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private CombatManager manager;

    // Used to edit player animations, physics and sprite
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float jumpHeight;
    [SerializeField]
    private float buttonInterval;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxKi;
    [SerializeField]
    private float subAmount;

    // An Object used to determine which side the player appearsn when subtituting
    public GameObject obj;

    // Checks used for specific calls
    private bool isCharging;
    private bool isGrounded;
    private bool isPressed;
    private bool isGuarding;
    private bool isSubbing;
    private bool isDashing;
    private bool isShooting;
    private bool isLookingLeft;
    private bool isInvincible;
    private bool hasEntered;
    private bool moveLocked;

    private float firstPress;
    private float secondPress;
    private float currentHealth;
    private float currentKi;
    private float time;

    // The location at which any Projectile called spawns
    public Transform spawnPoint;
    
    // The Projectile
    public GameObject prefab;

    // Health and Ki Property
    public float Health
    {
        get { return currentHealth; }
        set { currentHealth = value; }
    }
    public float Ki
    {
        get { return currentKi; }
        set { currentKi = value; }
    }

    void Start()
    {   
        // Intialization
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        manager = GetComponent<CombatManager>();
        sprite = GetComponent<SpriteRenderer>();
        isGrounded = true;
        hasEntered = false;
        isCharging = false;
        moveLocked = false;
        isPressed = false;
        isSubbing = false;
        isGuarding = false;
        isShooting = false;
        isInvincible = false;
        firstPress = 0;
        secondPress = 0;
        Health = maxHealth;
        Ki = maxKi;

        //Determines Player Side (Facing Right if P1, Left if P2)
        if (transform.position.x > 0)
        {
            sprite.flipX = true;
            if (obj != null)
                obj.transform.localPosition = new Vector3(0.4f, 0.0f, transform.localPosition.z);
        }
        else
        {
            sprite.flipX = false;
            if (obj != null)
                obj.transform.localPosition = new Vector3(-0.4f, 0.0f, transform.localPosition.z);
        }
    }

    void FixedUpdate()
    {
        // If We have a player and Entry Animation has Played
        if (gameObject != null && hasEntered)
        {
            // Checks if we are on Ground
            JumpCheck(transform);

            // Manages Movement Facing Right
            if (Input.GetKey(KeyCode.D) && !moveLocked && gameObject.tag != "Temp Player")
            {
                isLookingLeft = false;

                // if we have started moving right
                if (isPressed == true)
                {                  
                    firstPress = Time.time;
                    isPressed = false;

                    // If We Press D twice and the time between that is greater than the interval
                    if (firstPress - secondPress < buttonInterval)
                    {
                        Dash(KeyCode.D);
                    }
                }
                else
                {
                    // Reset 
                    animator.SetBool("IsDashing", false);
                    isDashing = false; 
                    rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
                    animator.Play("Run");
                    sprite.flipX = false;

                    // Flips The Object depending on where we face
                    if (obj != null)
                        obj.transform.localPosition = new Vector3(-0.4f, 0.0f, transform.localPosition.z);
                }
            }
            // Manages Movement Facing Left
            else if (Input.GetKey(KeyCode.A) && !moveLocked && gameObject.tag != "Temp Player")
            {
                isLookingLeft = true;

                // If we have started moving left
                if (isPressed == true)
                {
                    firstPress = Time.time;
                    isPressed = false;

                    // If We Press A twice and the time between that is greater than the interval
                    if (firstPress - secondPress < buttonInterval)
                    {
                        Dash(KeyCode.A);
                    }
                }
                else
                {
                    // Reset
                    animator.SetBool("IsDashing", false);
                    isDashing = false;
                    rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
                    animator.Play("Run");
                    sprite.flipX = true;

                    // Flips The Object depending on where we face
                    if (obj != null)
                        obj.transform.localPosition = new Vector3(0.4f, 0.0f, transform.localPosition.z);
                }

            }
            // Else if we aren't moving
            else
            {
                // Reset
                if (isPressed == false)
                {
                    secondPress = Time.time;
                    isPressed = true;
                }

                isCharging = false;
                animator.SetBool("BeginCharge", false);
                animator.SetBool("IsCharging", false);

                // If all these checks are met
                if (isGrounded && !isCharging &&
                    gameObject.tag != "Temp Player" &&
                    !manager.isHeavyAttacking && !isGuarding &&
                    !isSubbing && !manager.isLightAttacking &&
                    !isShooting && !manager.superOneActive)
                {
                    animator.Play("Idle");
                    moveLocked = false;
                    rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                }

            }

            // Manages Jumping
            if (Input.GetKey(KeyCode.Space) && isGrounded && !moveLocked && gameObject.tag != "Temp Player")
            {
                // Player is launched at the air at a maximum height
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
            }

            // Manages the Ki Blast: Shoots a small projectile
            if (Input.GetKey(KeyCode.X) && isGrounded)
            {
                // If we are attempting to shoot, don't interrupt
                if (isShooting) return;

                animator.Play("BeginBlast");
                isShooting = true;

                // Creates an instance of the projectile, sets it position and determines what side its facing
                GameObject blast = Instantiate(prefab);
                blast.GetComponent<Projectiles>().StartBlasting(isLookingLeft);
                blast.transform.position = spawnPoint.transform.position;

                // Reset
                Invoke("ResetBlast", 1f);
            }
        }
    }

    void Update()
    {
        // Manges Ki Charge: Charges the Energy Bar up
        if (Input.GetKey(KeyCode.Z) && isGrounded && gameObject.tag != "Temp Player")
        {
            // Movement is restricted
            moveLocked = true;

            // If we aren't Charging
            if (isCharging == false)
            {
                animator.SetBool("BeginCharge", true);
            }

            // If we have started Charging
            if (animator.GetBool("BeginCharge") == true)
            {
                // Begin Looped Animation
                animator.SetBool("IsCharging", true);
                isCharging = true;
            }
        }

        // Manages Substituting: For Paying One Energy, Player teleports behind the opposing player
        if (Input.GetKey(KeyCode.Y) && !moveLocked && gameObject.tag != "Temp Player")
        {
            // If we already subbed don't do it again
            if (isSubbing) return;

            // Starts the animation
            isSubbing = true;
            animator.Play("Substitute");
            StartCoroutine(Substituting());
        }


        // Manages Guarding: While guard is up player cannot take any damage from Light/Heavy combos. Damage is reduced from supers.
        if (Input.GetKey(KeyCode.G) && !moveLocked && gameObject.tag != "Temp Player")
        {
            isGuarding = true;
            animator.Play("Guard");
        }
        else
        {
            isGuarding = false;
        }
    }

    // Dashes the Player forward depending on direction
    void Dash(KeyCode key)
    {
        isDashing = true;
        
        if (key == KeyCode.D)
        {
            sprite.flipX = false;
            animator.SetBool("IsDashing", true);
            
            // Propels Player Forward on Direction
            rb2d.AddForce(new Vector2(50f, rb2d.velocity.y), ForceMode2D.Impulse);
        }

        if (key == KeyCode.A)
        {
            sprite.flipX = true;
            animator.SetBool("IsDashing", true);

            // Propels Player Forward on Direction
            rb2d.AddForce(new Vector2(-50f, rb2d.velocity.y), ForceMode2D.Impulse);
        }
    }


    // Plays the Entry Animation
    void EnablePlayer()
    {
        hasEntered = true;
        animator.SetTrigger("HasEntered");
    }

    // Checks if the player is on the ground or not
    void JumpCheck(Transform player)
    {
        // Shoots an invisble Line from the player to the ground and if we hit the floor 
        if (Physics2D.Linecast(player.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) && gameObject.tag != "Temp Player")
        {
            // On the Ground
            isGrounded = true;
            animator.SetBool("IsGrounded", true);
            animator.SetFloat("Y", 0);
            player.transform.position = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z);
        }
        else
        {
            // Not on The Ground
            if (rb2d.velocity.y > 0)
            {
                animator.SetBool("IsGrounded", false);
                animator.SetFloat("Y", rb2d.velocity.y);
            }

            isGrounded = false;
        }
    }


    // Swaps back to the Idle State when Shooting Is Finished
    void ResetBlast()
    {
        isShooting = false;
        animator.Play("Idle");
    }

    IEnumerator Substituting()
    {
        // Waits for the Substitute animations to finish playing
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Once finished teleport behind the opposing player and reset
        if (isSubbing)
            gameObject.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -0.01f);
        isSubbing = false;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];

        // If We Dash into the opposing player, force is not applied to the Opposing player
        if (contact.collider.tag == "Temp Player" && animator.GetBool("IsDashing") == true)
        {
            contact.collider.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else if (contact.collider.tag == "Temp Player" && animator.GetBool("IsDashing") == false)
        {

            contact.collider.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

}