using UnityEngine;
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
                Debug.Log("BOOOM!");
                break;
        }
    }
}
