using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    [SerializeField] InputAction fire;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

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
    
        [SerializeField] float controlSpeed = 30f;
        [SerializeField] float xRange = 10f;
        [SerializeField] float yRange = 5f;
        
        [SerializeField] float positionPitchFactor = -2f;
        [SerializeField] private float positionYawFactor = 2f;
        [SerializeField] private float controlPitchFactor = -10f;
        [SerializeField] private float controlRollFactor = -20f;
        
        float yThrow;
        float xThrow;
        
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
            Debug.Log("Firing...");
        }
        else
        {
            Debug.Log("Easy...");
        }
        // if pushing fire button
        // then print "shooting"
        // or else
    }
}
