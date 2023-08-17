using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ObjectPickup : MonoBehaviour
{
    public Transform raycastOrigin; // The point from which the ray will originate
    public float rayLength = 0.6f; // The length of the ray
    private GameObject objectInHand;
    private float throwForce = 10f;
    public GameObject handPoint;
    public Animator anim;
    public Rig IKRIGMantle;
    public GameObject objHoldPos;

    void Update()
    {
        if (Input.GetButtonDown("PickUp"))
        {
            if (objectInHand == null)
            {
                // Raycast to detect objects to pick up
                Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, rayLength))
                {
                    if (hit.collider.CompareTag("Pickable"))
                    {
                        objectInHand = hit.collider.gameObject;
                        objectInHand.GetComponent<Rigidbody>().isKinematic = true;
                        objectInHand.GetComponent<Rigidbody>().useGravity = false;
                        //objectInHand.GetComponent<BoxCollider>().enabled = false;
                        objectInHand.GetComponent<SphereCollider>().enabled = false;
                        
                        anim.SetTrigger("Lift");
                       
                        objectInHand.transform.SetParent(handPoint.transform);
                       // objectInHand.transform.position = handPoint.transform.localPosition + new Vector3(0, 0, 0);
                        
                    }
                }
            }
            else
            {
                // Throw the object
                Rigidbody rb = objectInHand.GetComponent<Rigidbody>();
                rb.isKinematic = false;
                rb.useGravity = true;
               // objectInHand.GetComponent<BoxCollider>().enabled = true;
                objectInHand.GetComponent<SphereCollider>().enabled = true;
                objectInHand.transform.SetParent(null);
               // IKRIGMantle.weight = 0.0f;
                anim.SetTrigger("PutDown");
                rb.AddForce(raycastOrigin.forward * throwForce, ForceMode.Impulse);
                objectInHand = null;
            }
        }
    }

    void joinObj()
    {
        objectInHand.transform.localPosition = objHoldPos.transform.localPosition;
        IKRIGMantle.weight = 1.0f;
    }
    void setIKup()
    {
        
    }
}
