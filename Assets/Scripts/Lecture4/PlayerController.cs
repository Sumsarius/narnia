using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] InputAction movement;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        movement.Enable();
    }
    void OnDisable()
    {
        movement.Disable();
    }
        [SerializeField] float controlSpeed = 30f;
        [SerializeField] private float xRange = 10f;
        [SerializeField] private float yRange = 5f;
    // Update is called once per frame
    void Update()

    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessRotation()

    {
        transform.localRotation = Quaternion.Euler(-30f, 30f, 0f);
    }
    void ProcessTranslation()
    {
        float xThrow = movement.ReadValue<Vector2>().x;
        //float xThrow = Input.GetAxis("Horizontal");    
        float yThrow = movement.ReadValue<Vector2>().y;
        //float yThrow = Input.GetAxis("Vertical");
       
       float xOffset = xThrow * Time.deltaTime * controlSpeed;
       float rawXPos = transform.localPosition.x + xOffset;
       float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);
       
       float yOffset = yThrow * Time.deltaTime * controlSpeed; 
       float rawYPos = transform.localPosition.y + yOffset;
       float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);
       
       transform.localPosition = new Vector3 (clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
