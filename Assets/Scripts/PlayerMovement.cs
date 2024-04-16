
using System;
using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public bool isSpeedingUp; // variable pour savoir si le joueur est en train de courir
    public bool isJumpActionTriggered; // variable pour savoir si le joueur a appuyé sur le bouton de saut
    public bool isJumping; // variable pour savoir si le joueur est en train de sauter (et son animation de saut est en cours)
    public bool isOnAir; // variable pour savoir si le joueur est en l'air
    public bool isGrounded;
    private float horizontalMovement;
    public Transform groundCheck;
    public float groundChecksRadius;
    public LayerMask collisionLayer;
    public SpriteRenderer spriteRenderer;


    public Rigidbody2D rb;
    public Animator animator;

    private Vector3 velocity = Vector3.zero;


    // Update est appelé à chaque frame
    void Update()
    {
        // Speed up detection
        if (Input.GetButtonDown("Fire2"))
        {
            Debug.Log("SpeedUp");
            //animator.SetTrigger("isSpeedUp");
            isSpeedingUp = true;
        } else
        {
            isSpeedingUp = false;
        }
        

        // Jump detection
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Debug.Log("Jump");
            isJumpActionTriggered = true;
            isJumping = true;
        }
        if (isJumping && !isGrounded)
        {
            isOnAir = true;
        }
        
        if (isGrounded && isOnAir)
        {
            isOnAir = false;
            isJumping = false;
            animator.SetTrigger("isLanding");
        }

        Flip(rb.velocity.x);
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);

    }

    // FixedUpdate est appelé à chaque frame pour les calculs de physique
    void FixedUpdate()
    {
        int speedUpFactor = 1;
        if (isSpeedingUp)
        {
            Debug.Log("SpeedUp factor");
            speedUpFactor = 2;
        } 

        horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime * speedUpFactor;

        
        // On vérifie si le joueur est au sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundChecksRadius, collisionLayer);

        MovePlayer(horizontalMovement);
    }

    // Fonction pour déplacer le joueur
    void MovePlayer(float _horizontalMovement)
    {
        // on ajoute la vélocité du joueur initiale sur y pour la conserver à l'identique
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        // on lisse la vélocité du joueur pour éviter les accélérations brutales
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if (isJumpActionTriggered)
        {
            Jump();
            isJumpActionTriggered = false;
        }
    }

    void Jump()
    {
        rb.AddForce(new Vector2(0f, jumpForce));

        //-----------Animmation-------------------------
        //With Triggers
        animator.SetTrigger("isJumpActionTriggered");
    }




    void Flip(float _velocity)
    {
        if (_velocity > 0.1f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -0.1f)
        {
            spriteRenderer.flipX = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundChecksRadius);
    }
}
