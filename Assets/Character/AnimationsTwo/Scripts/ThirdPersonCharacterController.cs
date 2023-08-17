using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ThirdPersonCharacterController : MonoBehaviour
{
    public Transform cameraTransform;
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpForce = 5f;
    public float attackDamage = 10f;
    public float attackRange = 1f;
    public float blockDuration = 1f;
    public float maxStamina = 100f;
    public float staminaRegenRate = 10f;
    public float staminaDepletionRate = 20f;
    public LayerMask attackLayerMask;
    private CapsuleCollider _capsule;
    private float _newSpeed;
  //  public string targetTag;
    private Rigidbody rb;
    private Animator animator;
    private bool isGrounded;
    private bool isAttacking;
    private bool isBlocking;
    private bool _climbing;
    private float currentStamina;

    

    //  public float climbForce = 5f;
    // public float climbDuration = 2f;
    // private bool isClimbing = false;
    // private float climbTimer = 0f;

   


   




    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        currentStamina = maxStamina;
        _capsule = GetComponent<CapsuleCollider>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Movement
        Vector3 movement = cameraTransform.forward * verticalInput + cameraTransform.right * horizontalInput;
        movement.y = 0f;
        movement.Normalize();
        movement *= moveSpeed;
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z);

        // Rotation
        if (movement.magnitude > 0.1f)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            Quaternion rotation = Quaternion.Lerp(rb.rotation, toRotation, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(rotation);
        }

        // Update animator parameters
        animator.SetFloat("Speed", movement.magnitude);

        // Jumping
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        // Blocking
        if (Input.GetButton("Fire2") && !isAttacking && currentStamina > 0f)
        {
            isBlocking = true;
            // Call blocking animation or behavior
            currentStamina -= Time.deltaTime * staminaDepletionRate;
        }
        else
        {
            isBlocking = false;
            currentStamina += Time.deltaTime * staminaRegenRate;
            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        }
        /*
        // Attacking
        if (Input.GetButtonDown("Fire1") && !isAttacking && !isBlocking && currentStamina > 0f)
        {
            if (ConsumeStamina(attackStaminaCost))
            {
                isAttacking = true;
                Attack();
            }
        }
        */
        

       

      
       
       
    }

    private bool ConsumeStamina(float amount)
    {
        if (currentStamina >= amount)
        {
            currentStamina -= amount;
            return true;
        }
        else
        {
            // Not enough stamina to perform the action
            return false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the character is touching the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    /*
    private void Attack()
    {
        // Play attack animation here

        // Perform raycast to detect enemies in range
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, attackRange, transform.forward, 0f, attackLayerMask);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyScript enemy = hit.collider.GetComponent<EnemyScript>();
                enemy.TakeDamage(attackDamage);
            }
        }

        // Delay the next attack based on animation length or cooldown
        float attackDuration = 1f; // Placeholder value, replace with actual attack animation length
        Invoke("ResetAttack", attackDuration);
    }
    */
    private void ResetAttack()
    {
        isAttacking = false;
    }
    


  

    
    

   

   
}

