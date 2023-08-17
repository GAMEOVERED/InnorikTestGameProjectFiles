using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vaulting : MonoBehaviour
{
    public string targetTag; // The tag of the objects you want to detect
    public float raySize = 0.5f; // The length of the ray
    public Animator anim;
    public CharacterController characterController;
    public float movementSpeed = 5f;
    public GameObject player;

    void Update()
    {
        if (Input.GetButtonDown("Vault"))
        {
            Ray ray = new Ray(player.transform.position, player.transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, raySize))
            {
                if (hit.collider.CompareTag(targetTag))
                {
                    StartCoroutine(MoveCharacterController());
                    // Debug the detected object
                    Debug.Log("Detected object: " + hit.collider.gameObject.name);

                    anim.SetTrigger("Vault");

                    // Move the character controller along with the animation
                   
                }
            }
            Debug.DrawRay(ray.origin, ray.direction * raySize, Color.red, 0.5f);
        }
    }

    IEnumerator MoveCharacterController()
    {
        // Wait for the next frame to ensure the animation has started
        yield return new WaitForEndOfFrame();

        // Disable the character controller to prevent physics-based movement during the animation
        characterController.enabled = true;

        // Get the initial position and rotation of the character controller
        Vector3 initialPosition = characterController.transform.position;
        Quaternion initialRotation = characterController.transform.rotation;

        // Wait for the animation to finish
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            // Move the character controller based on the animation's root motion
            characterController.Move(anim.deltaPosition);
            characterController.transform.rotation *= anim.deltaRotation;

            yield return null;
        }

        // Restore the initial position and rotation of the character controller
        characterController.transform.position = initialPosition;
        characterController.transform.rotation = initialRotation;

        // Re-enable the character controller
        characterController.enabled = true;
    }

    
}
