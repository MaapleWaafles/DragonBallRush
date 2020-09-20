using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
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

    private Animator animator;
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private CombatManager manager;
    public PlayerController player;
    public GameObject obj;

    private bool isCharging;
    private bool isGrounded;
    private bool isPressed;
    private bool isGuarding;
    private bool isSubbing;
    private bool isDashing;
    private bool isShooting;
    private bool hasEntered;
    private bool moveLocked;
    private bool isLookingLeft;
    private bool isInvincible;

    private float firstPress;
    private float secondPress;
    private float currentHealth;
    private float currentKi;
    private float time;

    public Transform spawnPoint;
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

    // Init
    void Start()
    {
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

        //Determines Player Side
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
        Debug.Log(isGrounded);
        if (gameObject != null && hasEntered)
        {
            // Floor Check
            JumpCheck(transform);
            // Movement + Dash
            if (Input.GetKey(KeyCode.D) && !moveLocked && gameObject.tag != "Temp Player")
            {
                isLookingLeft = false;
                if (isPressed == true)
                {
                    firstPress = Time.time;
                    isPressed = false;

                    if (firstPress - secondPress < buttonInterval)
                    {
                        Dash(KeyCode.D);
                    }
                }
                else
                {
                    animator.SetBool("IsDashing", false);
                    isDashing = false; 
                    rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
                    animator.Play("Run");
                    sprite.flipX = false;
                    if (obj != null)
                        obj.transform.localPosition = new Vector3(-0.4f, 0.0f, transform.localPosition.z);
                }
            }
            else if (Input.GetKey(KeyCode.A) && !moveLocked && gameObject.tag != "Temp Player")
            {
                isLookingLeft = true;
                if (isPressed == true)
                {
                    firstPress = Time.time;
                    isPressed = false;

                    if (firstPress - secondPress < buttonInterval)
                    {
                        Dash(KeyCode.A);
                    }
                }
                else
                {
                    animator.SetBool("IsDashing", false);
                    isDashing = false;
                    rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
                    animator.Play("Run");
                    sprite.flipX = true;
                    if (obj != null)
                        obj.transform.localPosition = new Vector3(0.4f, 0.0f, transform.localPosition.z);
                }

            }
            else if (Input.GetKey(KeyCode.Y) && !moveLocked && gameObject.tag != "Temp Player")
            {
                if (isSubbing) return;

                isSubbing = true;
                animator.Play("Substitute");
                StartCoroutine(Substituting());
            }
            else
            {
                if (isPressed == false)
                {
                    secondPress = Time.time;
                    isPressed = true;
                }

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

                isCharging = false;
                animator.SetBool("BeginCharge", false);
                animator.SetBool("IsCharging", false);
            }

            // Jump
            if (Input.GetKey(KeyCode.Space) && isGrounded && !moveLocked && gameObject.tag != "Temp Player")
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
            }

            // Guard
            if (Input.GetKey(KeyCode.G) && !moveLocked && gameObject.tag != "Temp Player")
            {
                isGuarding = true;
                animator.Play("Guard");
            }
            else
            {
                isGuarding = false;
            }

            //Ki Blast
            if (Input.GetKey(KeyCode.X) && isGrounded)
            {
                if (isShooting) return;

                animator.Play("BeginBlast");
                isShooting = true;

                GameObject blast = Instantiate(prefab);
                blast.GetComponent<Projectiles>().StartBlasting(isLookingLeft);
                blast.transform.position = spawnPoint.transform.position;

                Invoke("ResetBlast", 1f);
            }
        }
    }

    void Update()
    {
        // Ki Charge
        if (Input.GetKey(KeyCode.Z) && isGrounded && gameObject.tag != "Temp Player")
        {
            moveLocked = true;
            if (isCharging == false)
            {
                animator.SetBool("BeginCharge", true);
            }

            if (animator.GetBool("BeginCharge") == true)
            {
                animator.SetBool("IsCharging", true);
                isCharging = true;
            }
        }
    }



    // Dash Handler
    void Dash(KeyCode key)
    {
        isDashing = true;
        if (key == KeyCode.D)
        {
            animator.SetBool("IsDashing", true);
            sprite.flipX = false;
            rb2d.AddForce(new Vector2(50f, rb2d.velocity.y), ForceMode2D.Impulse);
        }

        if (key == KeyCode.A)
        {
            animator.SetBool("IsDashing", true);
            sprite.flipX = true;
            rb2d.AddForce(new Vector2(-50f, rb2d.velocity.y), ForceMode2D.Impulse);
        }
    }

    // Used to activate "Has Entered" Trigger on Animator
    void EnablePlayer()
    {
        hasEntered = true;
        animator.SetTrigger("HasEntered");
    }

    // Checks if the player is on the ground or not
    void JumpCheck(Transform player)
    {
        if (Physics2D.Linecast(player.transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) && gameObject.tag != "Temp Player")
        {
            isGrounded = true;
            animator.SetBool("IsGrounded", true);
            animator.SetFloat("Y", 0);
            player.transform.position = new Vector3(player.transform.position.x, 0.0f, player.transform.position.z);
        }
        else
        {
            if (rb2d.velocity.y > 0)
            {
                animator.SetBool("IsGrounded", false);
                animator.SetFloat("Y", rb2d.velocity.y);
            }
            isGrounded = false;
        }


    }

    //unsure if this halts his speed entirely
    void OnCollisionEnter2D(Collision2D collision)
    {
        ContactPoint2D contact = collision.contacts[0];
        if (contact.collider.tag == "Temp Player" && animator.GetBool("IsDashing") == true)
        {
            contact.collider.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        }
        else if (contact.collider.tag == "Temp Player" && animator.GetBool("IsDashing") == false)
        {

            contact.collider.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    void ResetBlast()
    {
        isShooting = false;
        animator.Play("Idle");
    }

    IEnumerator Substituting()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        if (isSubbing)
            gameObject.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, -0.01f);
        isSubbing = false;
    }

}