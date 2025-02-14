using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip impact;
    [SerializeField] AudioClip success;
    [SerializeField] ParticleSystem impactParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] MeshRenderer balloonRenderer;
    
    AudioSource audioSource;
    
    bool isTransitioning = false;
    bool collisionDisabled = false;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled; // toggle collision
            Debug.Log("Collision Switched");
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled) { return; }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Let's get to our destination");
                break;
            case "Fuel":
                // todo add fuel to the game
                Debug.Log("boost");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isTransitioning || collisionDisabled) { return; }
        StartCrashSequence();
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(impact);
        balloonRenderer.enabled = false;
        GetComponent<SphereCollider>().enabled = false;
        impactParticles.gameObject.transform.SetParent(null);
        impactParticles.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        GetComponent<Rigidbody>().isKinematic = true;
        successParticles.transform.rotation = Quaternion.Euler(0, 0, 0);
        successParticles.Play();
        GetComponent<Movement>().enabled=false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadNextLevel()
         {
             int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
             int nextSceneIndex = currentSceneIndex + 1;
             if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
             {
                 nextSceneIndex = 0;
             }
             SceneManager.LoadScene(nextSceneIndex);
         }
}
