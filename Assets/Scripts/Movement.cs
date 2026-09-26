using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotation;
    Rigidbody rb;
    [SerializeField] float thrustStrength = 10f;
    [SerializeField] Vector3 vRotationStrength;
    [SerializeField] float rotationStrength = 100f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
        vRotationStrength = new Vector3(0, 0, 100);
    }

    private void OnEnable() {
        thrust.Enable();
        rotation.Enable();
    }

    private void FixedUpdate() {
        //rb.transform.position = new Vector3(rb.transform.position.x, rb.transform.position.y, 0);
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust() {
        if (thrust.IsPressed()) {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        }
    }
    
    private void ProcessRotation() {
        
        float rotationInput = rotation.ReadValue<float>();
        
        if (rotationInput > 0) {
            ApplyRotation(-rotationStrength);
        } else if (rotationInput < 0) {
            ApplyRotation(rotationStrength);
        }
        
        /* Using Quaternion for rotation
        Quaternion deltaRotation = Quaternion.Euler(vRotationStrength * -rotationInput * Time.fixedDeltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
        */
        Debug.Log(rotationInput);
    }

    private void ApplyRotation(float rotationStrength) {
        transform.Rotate(0, 0, 1f * rotationStrength * Time.fixedDeltaTime);
    }
}
