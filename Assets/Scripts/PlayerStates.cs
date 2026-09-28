using System;
using UnityEngine;

public class PlayerStates : MonoBehaviour {
    public static PlayerStates Instance { get; private set; }
    public event EventHandler<ThrustStateChangedEventArs> OnThrustStateChanged;
    public event EventHandler<EnergyConsumptionEventArgs> OnEnergyChanged;
    private bool isThrustingNow = false;
    private float thrustTimer = 0f;
    private float thrustTimerMax = 0.3f;

    public static float energy;
    private float maxEnergy = 100f;

    FuelBonus[] fuelBonuses;

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
        FindFuelBonus();
        //FuelBonus.Instance.OnFuelBonus += FuelBonus_OnFuelBonus;
        energy = 50;
        OnEnergyChanged?.Invoke(this, new EnergyConsumptionEventArgs { EnergyPercentage = energy / maxEnergy });
    }

    private void FuelBonus_OnFuelBonus(object sender, FuelBonus.BonusEventArgs e) {
        // energy = Mathf.Min(energy + e.Bonus, maxEnergy); <- this would be the correct way to do it
        if (energy < maxEnergy) {
            if (energy + e.Bonus > maxEnergy) {
                energy = maxEnergy;
            } else {
                energy += e.Bonus;
            }

            OnEnergyChanged?.Invoke(this, new EnergyConsumptionEventArgs { EnergyPercentage = energy / maxEnergy });
        }
    }

    private void Movement_OnThrust(object sender, EventArgs e) {
        isThrustingNow = true;
        HandleEnergyConsumption();
    }

    private void Update() {
        HandleThrustState();
    }

    private void HandleThrustState() {
        if (isThrustingNow) {
            if (currentState == ThrustState.Idle) {
                currentState = ThrustState.Thrusting;
                OnThrustStateChanged?.Invoke(this, new ThrustStateChangedEventArs { IsThrusting = true });
            } else if (currentState == ThrustState.Thrusting) {
                thrustTimer = 0;
            }
        }

        if (!isThrustingNow) {
            if (thrustTimer >= thrustTimerMax) {
                currentState = ThrustState.Idle;
                OnThrustStateChanged?.Invoke(this, new ThrustStateChangedEventArs { IsIdle = true });
                thrustTimer = 0;
            } else if (thrustTimer < thrustTimerMax) {
                thrustTimer += Time.deltaTime;
            }
        }

        isThrustingNow = false;
    }

    public class ThrustStateChangedEventArs : EventArgs {
        public bool IsIdle { get; set; }
        public bool IsThrusting { get; set; }
    }

    public class EnergyConsumptionEventArgs : EventArgs {
        public float EnergyPercentage { get; set; }
    }


    private void HandleEnergyConsumption() {
        energy -= 20 * Time.deltaTime;
        OnEnergyChanged?.Invoke(this, new EnergyConsumptionEventArgs { EnergyPercentage = energy / maxEnergy });
    }

    public static bool CanUseEnergy() {
        return energy > 0;
    }

    private void FindFuelBonus() {
        fuelBonuses = FindObjectsOfType<FuelBonus>();
        foreach (var bonus in fuelBonuses) {
            bonus.OnFuelBonus += FuelBonus_OnFuelBonus;
        }
    }
}