using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] float destroyTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}
