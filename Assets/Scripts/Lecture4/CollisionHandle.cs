using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandle : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip impact;
    [SerializeField] ParticleSystem impactParticles;
    [SerializeField] MeshRenderer shipRenderer;
    
    AudioSource audioSource;
    
    bool isTransitioning = false;
    bool collisionDisabled = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (isTransitioning || collisionDisabled) { return; }
        StartCrashSequence();
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<PlayerController>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        shipRenderer.enabled = false;
        impactParticles.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(impact);
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
