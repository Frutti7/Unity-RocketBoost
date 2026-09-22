using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour {
    [SerializeField] private InputAction thrust;

    private void OnEnable() {
        thrust.Enable();
    }

    private void Update() {
        if (thrust.WasPressedThisFrame()) {
            Debug.Log("Thrust");
        }
    }
}
