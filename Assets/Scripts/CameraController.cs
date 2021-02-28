using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    GameplayUI gameplayUI;

    public float sensitivity = 1000.0f;
    public Transform playerBody;

    private float XAxisRotation = 0.0f;
    private Vector2 mouseDelta;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (!gameplayUI.isPaused)
        { // Look up and down
            XAxisRotation -= mouseDelta.y;
            XAxisRotation = Mathf.Clamp(XAxisRotation, -90, 90.0f);
            transform.localRotation = Quaternion.Euler(XAxisRotation, 0.0f, 0.0f);

            // Look left and right and rotate around the Y Axis
            playerBody.Rotate(Vector3.up * mouseDelta.x);
        }
    }

    public void OnMouseMove(InputAction.CallbackContext value)
    {
        mouseDelta = value.ReadValue<Vector2>() * sensitivity;
    }
}
