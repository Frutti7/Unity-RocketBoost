using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}

    private void Awake() {
        Instance = this;
    }

    public void PlaySound(AudioClip audioClip, Vector3 position, float volume) {
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
    
}
