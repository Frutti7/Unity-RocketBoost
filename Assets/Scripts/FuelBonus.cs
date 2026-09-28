using System;
using UnityEngine;

public class FuelBonus : MonoBehaviour
{
    [SerializeField] private float bonus = 10f;   
    public event EventHandler<BonusEventArgs> OnFuelBonus;
    
    private void Start() {
        CollisionHandler.Instance.onFuelCollected += HandlerOnFuelCollected;
    }

    private void HandlerOnFuelCollected(object sender, CollisionHandler.CollisionObjectEventArgs e) {
        if (e.gameObject != gameObject) {
            return;
        }
        gameObject.SetActive(false);
        OnFuelBonus?.Invoke(this, new BonusEventArgs{Bonus = bonus});
    }

    public class BonusEventArgs : EventArgs {
        public float Bonus { get; set; }
    }
}