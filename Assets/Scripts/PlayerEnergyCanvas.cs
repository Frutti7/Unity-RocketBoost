using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEnergyCanvas : MonoBehaviour
{
    [SerializeField] private Image barImage;
    
    private void Start() {
        PlayerStates.Instance.OnEnergyChanged += PlayerStates_OnEnergyChanged;
    }

    private void PlayerStates_OnEnergyChanged(object sender, PlayerStates.EnergyConsumptionEventArgs e) {
        barImage.fillAmount = e.EnergyPercentage;
    }
}
