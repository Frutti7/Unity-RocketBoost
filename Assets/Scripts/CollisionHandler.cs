using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
    public static CollisionHandler Instance { get; private set; }

    public event EventHandler<CollisionObjectEventArgs> onCollisionDestroy;
    public event EventHandler<CollisionObjectEventArgs> onCollisionFriendly;
    public event EventHandler<CollisionObjectEventArgs> onCollisionFinish;
    public event EventHandler<CollisionObjectEventArgs> onFuelCollected;

    private void Awake() {
        Instance = this;
    }

    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Friendly":
                break;
            case "Finish":
                LoadNextLevel();
                break;
            default:
                ReloadLevel();
                break;
        }
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