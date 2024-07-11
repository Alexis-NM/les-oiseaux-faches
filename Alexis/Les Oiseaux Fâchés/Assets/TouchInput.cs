using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private GameObject ball;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Out the method if not touching the screen
        if (!Touchscreen.current.primaryTouch.press.isPressed)
        {
            return;
        }

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 screenPosition = new Vector3(touchPosition.x, touchPosition.y, mainCamera.WorldToScreenPoint(ball.transform.position).z);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

        ball.transform.position = new Vector3(worldPosition.x, worldPosition.y, ball.transform.position.z);
        Debug.Log("TOUCH POSITION: " + touchPosition);
        Debug.Log("WORLD POSITION: " + worldPosition);
    }
}