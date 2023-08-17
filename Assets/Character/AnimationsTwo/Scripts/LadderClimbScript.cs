using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using StarterAssets;

public class LadderClimbScript : MonoBehaviour
{
   
    public GameObject playerObject;
    public Transform startObject;
    public Button climbButton;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }





    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            other.transform.position = Vector3.MoveTowards(transform.position, startObject.position, startObject.position.z);
            // other.transform.Rotate(0.0f, other.transform.localRotation.eulerAngles.y - other.transform.localRotation.eulerAngles.y, 0.0f, Space.Self);
            // climbButton.gameObject.SetActive(true);
            if (other.tag == "Player"  )
            {
               // other.GetComponent<CharacterController>().enabled = false;
                other.GetComponent<Animator>().SetBool("ClimbUp", true);
                other.GetComponent<Animator>().applyRootMotion = true;
                // other.transform.Rotate(0.0f,0.0f,0.0f,Space.Self);
                other.GetComponent<ThirdPersonController>().enabled = false;
                // StartCoroutine(StartClimb());
                // anim.SetTrigger("Up");
                Debug.Log("Climbing");
                climbButton.gameObject.SetActive(false);
            }
        }
       
       
    
    }

   
   
}