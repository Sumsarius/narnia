using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] private int hitPoints = 10;
    
    ScoreBoard scoreBoard;
    GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        Rigidbody  rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1) ;
        {
            KillEnemy();
        }
    }

    private void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitPoints--;
    }
    private void KillEnemy()
    {
        scoreBoard.IncreaseScore(scorePerHit);
        GameObject vfx = Instantiate(deathFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }

    
}
