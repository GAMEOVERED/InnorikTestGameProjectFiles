using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class LadderClimbScript1 : MonoBehaviour
{
   
    public GameObject playerObject;
    public GameObject endPosition;
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
        if (other.tag == "Player" )
        {
            other.GetComponent<CharacterController>().enabled = true;
            other.GetComponent<Animator>().SetBool("ClimbUp",false);
            other.GetComponent<Animator>().SetFloat("Speed",0.0f);
            other.GetComponent<Animator>().applyRootMotion = false;
            StartCoroutine(ExampleCoroutine());
            // other.transform.localRotation = endPosition.transform.rotation;
            other.transform.localPosition = Vector3.Lerp(endPosition.transform.position,endPosition.transform.position, 10);
            // StartCoroutine(StartClimb());
            // anim.SetTrigger("Up");
            Debug.Log("DoneClimbing");

        }


        IEnumerator ExampleCoroutine()
        {


            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(0.8f);
            other.GetComponent<ThirdPersonController>().enabled = true;
            //  animator.SetBool("FreeFall", false);
            // animator.SetBool("Grounded", true);


        }


    }

    


}