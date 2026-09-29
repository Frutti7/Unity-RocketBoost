using System;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
   private AudioSource audioSource;
   [SerializeField] private AudioSource mainEngineAudioSource;
   //[SerializeField] AudioClip mainEngine;
   [SerializeField] AudioClip crash;
   [SerializeField] AudioClip levelSuccess;
   
   
   private void Awake() {
      audioSource = GetComponent<AudioSource>();
      //mainEngineAudioSource = GetComponent<AudioSource>();
   }

   private void Start() {
      PlayerStates.Instance.OnThrustStateChanged += PlayerStates_OnThrustStateChanged;
      CollisionHandler.Instance.onCollisionCrash += CollisionHandler_onCollisionCrash;
      CollisionHandler.Instance.onCollisionFinish += CollisionHandler_onCollisionFinish;
   }

   private void CollisionHandler_onCollisionFinish(object sender, EventArgs e) {
      audioSource.PlayOneShot(levelSuccess);
   }

   private void CollisionHandler_onCollisionCrash(object sender, EventArgs e) {
      audioSource.PlayOneShot(crash);
   }

   private void PlayerStates_OnThrustStateChanged(object sender, PlayerStates.ThrustStateChangedEventArs e) {
      if (e.IsThrusting) {
         mainEngineAudioSource.Play();
      } else if (e.IsIdle) {
         mainEngineAudioSource.Stop();
      }
   }
}
