using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] private Transform parent;
    private void OnParticleCollision(GameObject other)
    {
        Instantiate(deathVFX, transform.position, Quaternion.identity);
        deathVFX.transform.parent = parent;
        Destroy(gameObject);
    }
}
