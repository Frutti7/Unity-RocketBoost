using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {

    [SerializeField] private float collisionCrashDelay = 2f;
    [SerializeField] private float finishDelay = 1f;
    public static CollisionHandler Instance { get; private set; }

    public event EventHandler onCollisionCrash;
    public event EventHandler<CollisionObjectEventArgs> onCollisionFriendly;
    public event EventHandler onCollisionFinish;
    public event EventHandler<CollisionObjectEventArgs> onFuelCollected;

    private void Awake() {
        Instance = this;
    }

    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartSuccessSequence() {
        // TODO: Play success animation
        onCollisionFinish?.Invoke(this, EventArgs.Empty);
        Invoke("LoadNextLevel", finishDelay);
    }

    private void StartCrashSequence() {
        // TODO: Play crash animation
        onCollisionCrash?.Invoke(this, EventArgs.Empty);
        Invoke("ReloadLevel", collisionCrashDelay);
    }

    private void OnTriggerEnter(Collider other) {
        switch (other.gameObject.tag) {
            case "Fuel":
                onFuelCollected?.Invoke(this, new CollisionObjectEventArgs { gameObject = other.gameObject });
                break;
        }
    }

    public class CollisionObjectEventArgs : EventArgs {
        public GameObject gameObject;
    }

    private void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void LoadNextLevel() {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        int nextScene = currentSceneIndex + 1;
        if (nextScene == SceneManager.sceneCountInBuildSettings) {
            nextScene = 0;
        }
        
        SceneManager.LoadScene(nextScene);
    }
}