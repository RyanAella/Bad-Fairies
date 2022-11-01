using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float xSensitivity;
    public float ySensitivity;

    public Transform playerRotation;

    private float _xCamRotation;
    private float _yCamRotation;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        // mouse input
        var mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * xSensitivity;
        var mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * ySensitivity;

        _xCamRotation += mouseX;
        _xCamRotation -= mouseY;
        
        // if (_animator != null)
        // {
        //     if (Input.GetKeyDown(KeyCode.L))
        //     {
        //         _animator.SetTrigger("isWalking");
        //     }
        //     else
        //     {
        //         isWalking = false;
        //     }
        //
        //     if (Input.GetKeyDown(KeyCode.R) && isRunning == false)
        //     {
        //         _animator.SetTrigger("isRunning");
        //     }
        //     else if (Input.GetKeyDown(KeyCode.R) && isRunning == true)
        //     {
        //         _animator.SetTrigger(isRunning);
        //     }
        // }
    }
}
