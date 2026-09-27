using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    private void OnCollisionEnter(Collision collision) {
        switch (collision.gameObject.tag) {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                Debug.Log("You win!");
                break;
            case "Fuel":
                Debug.Log("You gained fuel!");
                break;
            default:
                Debug.Log("You lose!");
                break;
        }
    }
}