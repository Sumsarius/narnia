using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] GameObject hitVFX;
    [SerializeField] private Transform parent;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] private int hitPoints = 10;
    
    ScoreBoard scoreBoard;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
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
        vfx.transform.parent = parent;
        hitPoints--;
        scoreBoard.IncreaseScore(scorePerHit);
    }
    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parent;
        Destroy(gameObject);
    }

    
}
