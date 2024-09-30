using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class BeeController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Joystick fixedJoystick;
    private Rigidbody rigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        fixedJoystick = FindObjectOfType<Joystick>();
        rigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        fixedJoystick.
    }
}
