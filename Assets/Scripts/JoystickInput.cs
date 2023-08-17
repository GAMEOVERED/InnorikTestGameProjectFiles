using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickInput : MonoBehaviour
{
    public Joystick joystick;
 //   public FixedTouchField touchField;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float vertical = joystick.Vertical;
        //Debug.Log(vertical);
        // Calculate movement direction
        Vector3 movementDirection = new Vector3(horizontalInput, 0.0f, verticalInput);
        movementDirection.Normalize();
    }
}
