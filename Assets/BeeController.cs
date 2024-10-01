using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Add this line to include the namespace for FixedJoystick

public class BeeController : MonoBehaviour
{
    [SerializeField] private float speed ;

    private FixedJoystick fixedJoystick;
    private new Rigidbody rigidbody;

    [System.Obsolete]
    private void OnEnable()
    {
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
       float xVal = fixedJoystick.Horizontal;
       float yVal = fixedJoystick.Vertical;

       Vector3 movement = new Vector3(xVal, 0, yVal);
       rigidbody.linearVelocity = movement * speed;

       if(xVal != 0 || yVal != 0)
       {
           transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(xVal, yVal) * Mathf.Rad2Deg, transform.eulerAngles.z);
       }
    }


}