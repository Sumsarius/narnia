using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Controller Input")]
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    
    [Header("Movement")]
    [Tooltip("Movement speed up and down")] 
    [SerializeField] float controlSpeed = 30f;
    [Tooltip("Movement cap horizontal")] 
    [SerializeField] float xRange = 10f;
    [Tooltip("Movement cap vertical")]
    [SerializeField] float yRange = 5f;
    
    [Header("Weapons Array")]
    [Tooltip("Add spaceship weapons")]
    [SerializeField] private GameObject[] lasers;
   
    [Header("Screen position based tuning")]
    [SerializeField] private float positionYawFactor = 2f;
    [SerializeField] private float controlPitchFactor = -10f;
    
    [Header("Player input based tuning")]
    [SerializeField] float positionPitchFactor = -2f;
    [SerializeField] private float controlRollFactor = -20f;
        
    float yThrow;
    float xThrow;
    
    void OnEnable()
    {
        movement.Enable();
        fire.Enable();
    }
    void OnDisable()
    {
        movement.Disable();
        fire.Disable();
    }
    
    // Update is called once per frame
    void Update()

    {
        ProcessTranslation();
        ProcessRotation();
        ProcessFiring();
    }
    void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToRotation = yThrow * controlPitchFactor;
        float rollDueToRotation = xThrow * controlRollFactor;
        
        float pitch =  pitchDueToPosition + pitchDueToRotation;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * rollDueToRotation;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
    
    void ProcessTranslation()
    {
        xThrow = movement.ReadValue<Vector2>().x;
        //float xThrow = Input.GetAxis("Horizontal");    
        yThrow = movement.ReadValue<Vector2>().y;
        //float yThrow = Input.GetAxis("Vertical");
       
       float xOffset = xThrow * Time.deltaTime * controlSpeed;
       float rawXPos = transform.localPosition.x + xOffset;
       float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
       
       float yOffset = yThrow * Time.deltaTime * controlSpeed; 
       float rawYPos = transform.localPosition.y + yOffset;
       float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
       
       transform.localPosition = new Vector3 (clampedXPos, clampedYPos, transform.localPosition.z);
    }

    void ProcessFiring()
    {
        if (fire.ReadValue<float>() > .5f)
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    void SetLasersActive(bool isActive)
    {
        foreach (GameObject laser in lasers)
        {
            var emissionModule = laser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
