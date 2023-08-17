using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Pushing : MonoBehaviour
{
    public string targetTag; // The tag of the objects you want to detect
    public float raySize = 0.5f; // The length of the ray
    public float pushForce;
    public Animator anim;
    public Rig IKRIGMantle;

    void Update()
    {

        Push();

    }

   

    public void Push()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, raySize))
        {
           
           // Debug.Log(hit.collider.gameObject.name);
            if (hit.transform.tag == "Vaultable" && Input.GetButtonDown("Pushing"))
            {

                Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(transform.forward * pushForce);
                anim.SetBool("Push", true);
               // IKRIGMantle.weight = 1.0f;
                // You can perform additional actions with the detected object here
            }
            else if(Input.GetButtonUp("Pushing") && hit.transform.tag == "Vaultable") {
                Rigidbody rb = hit.collider.gameObject.GetComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
               // rb.AddForce(transform.forward * pushForce);
                anim.SetBool("Push", false);
                //IKRIGMantle.weight = 0.0f;
            }
            
        }
        Debug.DrawRay(ray.origin, ray.direction * raySize, Color.red, 0.5f);
    }
}
