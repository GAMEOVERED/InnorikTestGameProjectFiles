using UnityEngine;

public class CharacterVault : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;
    public float rayDistance = 1f;
    public LayerMask mantlingLayerMask;
    public float mantlingHeight = 1f;
    public float mantlingDepth = 1f;
    public Animator animator;
    public float mantlingDuration = 1.5f; // Duration of the mantling animation

    private bool isMantling = false;
    private Vector3 mantlingStartPosition;
    private Vector3 mantlingEndPosition;
    private Quaternion mantlingStartRotation;
    private Quaternion mantlingEndRotation;
    private float mantlingTimer = 0f;

    private void Update()
    {
        if (isMantling)
        {
            // Update the mantling animation
            mantlingTimer += Time.deltaTime;

            if (mantlingTimer >= mantlingDuration)
            {
                // Finish the mantling animation
                animator.SetBool("Vault", false);
                isMantling = false;
            }

            // Stop movement while mantling
            return;
        }

        // Get input for movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        movement.Normalize();

        if (movement != Vector3.zero)
        {
            // Rotate player towards the movement direction
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }

        // Move the player
        transform.Translate(movement * movementSpeed * Time.deltaTime, Space.World);

        // Check for mantling
        if (Input.GetButtonDown("Vault"))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, rayDistance, mantlingLayerMask))
            {
                // Mantle over the obstacle
                animator.SetBool("Vault", true);
                isMantling = true;
                mantlingStartPosition = transform.position;
                mantlingEndPosition = hit.point + hit.normal * mantlingHeight + transform.forward * mantlingDepth;
                mantlingStartRotation = transform.rotation;
                mantlingEndRotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
                mantlingTimer = 0f;
            }
        }
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (isMantling)
        {
            // Set IK targets and weights during mantling
            float t = mantlingTimer / mantlingDuration;

            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, t);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, t);

            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, t);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, t);

            animator.SetIKPosition(AvatarIKGoal.LeftHand, Vector3.Lerp(mantlingStartPosition, mantlingEndPosition, t));
            animator.SetIKPosition(AvatarIKGoal.RightHand, Vector3.Lerp(mantlingStartPosition, mantlingEndPosition, t));

            animator.SetIKRotation(AvatarIKGoal.LeftHand, Quaternion.Lerp(mantlingStartRotation, mantlingEndRotation, t));
            animator.SetIKRotation(AvatarIKGoal.RightHand, Quaternion.Lerp(mantlingStartRotation, mantlingEndRotation, t));
        }
    }

    private void OnDrawGizmos()
    {
        // Visualize the raycast in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
