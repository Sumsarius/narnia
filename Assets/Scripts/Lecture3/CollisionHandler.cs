using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("no problem");
                break;
            case "Fuel":
                Debug.Log("boost");
                break;
            case "Finish":
                Debug.Log("Game Over, you win");
                break;
            default:
                SceneManager.LoadScene("Lecture3 - Balloon");
                break;
        }
    }
}
