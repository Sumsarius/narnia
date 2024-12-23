using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    MeshRenderer visible;
    Rigidbody rb;
    [SerializeField] float timeToWait = 5f;

    void Start()
    {
        visible = GetComponent<MeshRenderer>();
        rb = GetComponent<Rigidbody>();

        visible.enabled = false;
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > timeToWait)
        {
            visible.enabled = true;
            rb.useGravity = true;
        }
    }
}