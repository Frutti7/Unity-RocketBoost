using System;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
   private AudioSource audioSource;
   
   private void Awake() {
      audioSource = GetComponent<AudioSource>();
   }

   private void Start() {
      PlayerStates.Instance.OnThrustStateChanged += PlayerStates_OnThrustStateChanged;
   }

   private void PlayerStates_OnThrustStateChanged(object sender, PlayerStates.ThrustStateChangedEventArs e) {
      if (e.IsThrusting) {
         audioSource.Play();
      } else if (e.IsIdle) {
         audioSource.Stop();
      }
   }
}
