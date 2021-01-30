
using UnityEngine;

public class MouseHandler : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;
    public bool cursorLock = false;

    float xRotation = 0f;

    void Start()
    {
        
    }

    void Update()
    {
        if (cursorLock)
         {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }


    }
    public void LockMouse(bool val)
    {
        if (val == true)
        {
            cursorLock = true;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            cursorLock = false;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}