
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Update is called once per frame
    void Update()   
    {
        if (Input.GetKey(KeyCode.Escape))
        { 
            Application.Quit(); 
            Debug.Log("Quitting...");
        }
    }
}