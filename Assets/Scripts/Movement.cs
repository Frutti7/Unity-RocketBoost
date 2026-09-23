using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
    [SerializeField] private InputAction thrust;
    Rigidbody rb;
    [SerializeField] float thrustStrength = 10f;

    private void Start() {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable() {
        thrust.Enable();
    }

    private void FixedUpdate() {
        if (thrust.IsPressed()) {
            rb.AddRelativeForce(Vector3.up * thrustStrength * Time.fixedDeltaTime);
        }
    }
}
