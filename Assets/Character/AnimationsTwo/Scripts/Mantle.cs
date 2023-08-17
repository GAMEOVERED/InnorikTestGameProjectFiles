using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Animations.Rigging;
using StarterAssets;


public class Mantle : MonoBehaviour
{
    
    public float rayDistance = 1f;
    public LayerMask mantlingLayerMask;
    public float mantlingHeight = 1f;
    public float mantlingDepth;
    public Animator animator;
    public float mantlingDuration = 1.5f; // Duration of the mantling animation
    public GameObject rayOrigin;
    public GameObject rayOriginM;
    public CharacterController characterController;
    public GameObject playerScript;
    public float charcontrollerHeight;
    public float capsulecolliderHeight;
    
    public Rig IKRIGMantle;

    private bool isMantling = false;
    private Vector3 mantlingStartPosition;
    private Vector3 mantlingEndPosition;
    private Quaternion mantlingStartRotation;
    private Quaternion mantlingEndRotation;
    private float mantlingTimer = 0f;




     void Update()
    {
       



            if (isMantling)
        {
            // Update the mantling animation
            mantlingTimer += Time.deltaTime;

            if (mantlingTimer >= mantlingDuration)
            {
                StartCoroutine(ExampleCoroutine());
                // Finish the mantling animation
                animator.SetBool("Vault", false);
                //IKRIGMantle.weight = 0.0f;
                animator.SetBool("Grounded", true);
                animator.SetBool("FreeFall", false);
                playerScript.GetComponent<ThirdPersonController>().enabled = true;
                playerScript.GetComponent<CapsuleCollider>().enabled = true;
                playerScript.GetComponent<CharacterController>().height = charcontrollerHeight;
                playerScript.GetComponent<CapsuleCollider>().height = capsulecolliderHeight;
                animator.applyRootMotion = false;
                isMantling = false;
                characterController.enabled = true;
            }

            // Stop movement while mantling
            return;
        }


        // Check for mantling
        if (Input.GetButtonDown("Vault"))
        {
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin.transform.position, rayOrigin.transform.forward, out hit, rayDistance, mantlingLayerMask))
            {
                StartCoroutine(ExampleCoroutine());
                this.transform.position = Vector3.MoveTowards(transform.position, transform.position, transform.position.z);
                // Mantle over the obstacle
                animator.SetBool("Vault", true);
                animator.SetBool("Grounded", true);
                animator.SetBool("FreeFall", false);
                //playerScript.GetComponent<PlayerInput>().enabled = false;
                playerScript.GetComponent<ThirdPersonController>().enabled = false;
                playerScript.GetComponent<CapsuleCollider>().enabled = false;
                playerScript.GetComponent<CharacterController>().height = 0.6f;
                playerScript.GetComponent<CapsuleCollider>().height = 0.6f;
                //IKRIGMantle.weight = 1.0f;
                characterController.enabled = false;
                isMantling = true;
                animator.applyRootMotion = true;
                mantlingStartPosition = transform.position;
                mantlingEndPosition = hit.point + hit.normal * mantlingHeight + characterController.transform.forward * mantlingDepth;
                mantlingStartRotation = transform.rotation;
                mantlingEndRotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
                mantlingTimer = 0f;
            }
        }
        

    }

    public void MantleOver()
    {
        
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin.transform.position, rayOrigin.transform.forward, out hit, rayDistance, mantlingLayerMask))
            {
                // Mantle over the obstacle
                animator.SetBool("Vault", true);
                animator.SetBool("FreeFall", false);
                animator.SetBool("Grounded", true);
                StartCoroutine(ExampleCoroutine());
                //playerScript.GetComponent<PlayerInput>().enabled = false;
                playerScript.GetComponent<ThirdPersonController>().enabled = false;
                playerScript.GetComponent<CapsuleCollider>().enabled = false;
                playerScript.GetComponent<CharacterController>().height = 0.6f;
                playerScript.GetComponent<CapsuleCollider>().height = 0.6f;
                //IKRIGMantle.weight = 1.0f;
                characterController.enabled = false;
                isMantling = true;
                animator.applyRootMotion = true;
                mantlingStartPosition = transform.position;
                mantlingEndPosition = hit.point + hit.normal * mantlingHeight + characterController.transform.forward * mantlingDepth;
                mantlingStartRotation = transform.rotation;
                mantlingEndRotation = Quaternion.LookRotation(-hit.normal, Vector3.up);
                mantlingTimer = 0f;
            }
        

    }

    IEnumerator ExampleCoroutine()
    {
     

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(5.8f);
     //  animator.SetBool("FreeFall", false);
       // animator.SetBool("Grounded", true);
       
        
    }

    private void OnDrawGizmos()
    {
        // Visualize the raycast in the Scene view
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * rayDistance);
    }
}
