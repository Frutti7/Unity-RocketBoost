 using System;
 using UnityEngine;

public class PlayerStates : MonoBehaviour
{
    public static PlayerStates Instance {get; private set;}
    public event EventHandler<ThrustStateChangedEventArs> OnThrustStateChanged ;
    private bool isThrustingNow = false;
    private float thrustTimer = 0f;
    private float thrustTimerMax = 0.3f;
    
    public enum ThrustState {
        Idle,
        Thrusting,
    }
    
    private ThrustState currentState = ThrustState.Idle;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        Movement.Instance.OnThrust += Movement_OnThrust;
    }

    private void Movement_OnThrust(object sender, EventArgs e) {
        isThrustingNow = true;
    }

    private void Update() {
        HandleThrustState();
    }
    
    private void HandleThrustState() {
        if (isThrustingNow) {
            if (currentState == ThrustState.Idle) {
                currentState = ThrustState.Thrusting;
                OnThrustStateChanged?.Invoke(this, new ThrustStateChangedEventArs {IsThrusting = true});
            } else if (currentState == ThrustState.Thrusting) { 
                thrustTimer = 0;
            }
        }

        if (!isThrustingNow) {
            if (thrustTimer >= thrustTimerMax) {
                currentState = ThrustState.Idle;
                OnThrustStateChanged?.Invoke(this, new ThrustStateChangedEventArs {IsIdle = true});
                thrustTimer = 0;
            } else if (thrustTimer < thrustTimerMax) {
                thrustTimer += Time.deltaTime;
            }
        }
        isThrustingNow = false;
    }

    public class ThrustStateChangedEventArs : EventArgs {
        public bool IsIdle { get; set;}
        public bool IsThrusting { get; set;}
    }
    
}
