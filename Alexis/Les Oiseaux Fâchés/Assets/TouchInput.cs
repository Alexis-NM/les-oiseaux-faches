using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TouchInput : MonoBehaviour
{
    private Camera mainCamera;
    [SerializeField] private GameObject ball;
    private Rigidbody2D ballRigidbody2D;
    private SpringJoint2D ballSpringJoint2D;

    private bool isDragging = false;
    private Vector3 lastPosition;
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        ballRigidbody2D = ball.GetComponent<Rigidbody2D>();
        ballSpringJoint2D = ball.GetComponent<SpringJoint2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sortir de la méthode si on ne touche pas l'écran
        if (Touchscreen.current == null || !Touchscreen.current.primaryTouch.press.isPressed)
        {
            if (isDragging)
            {
                // Lancer la balle en appliquant la vitesse obtenue
                Vector3 throwVelocity = (currentPosition - lastPosition) / Time.deltaTime;
                ballRigidbody2D.velocity = new Vector2(throwVelocity.x, throwVelocity.y);

                // Changer le type du corps en Dynamic
                ballRigidbody2D.bodyType = RigidbodyType2D.Dynamic;

                // Supprimer le SpringJoint2D après un délai de 0.5 secondes
                Invoke("RemoveSpringJoint", 0.5f);

                isDragging = false;
            }
            return;
        }

        Vector2 touchPosition = Touchscreen.current.primaryTouch.position.ReadValue();
        Vector3 screenPosition = new Vector3(touchPosition.x, touchPosition.y, mainCamera.WorldToScreenPoint(ball.transform.position).z);
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);

        if (isDragging)
        {
            lastPosition = currentPosition;
            currentPosition = worldPosition;
            // ball.transform.position = new Vector3(worldPosition.x, worldPosition.y, ball.transform.position.z);
            ballRigidbody2D.position = new Vector3(worldPosition.x, worldPosition.y, ball.transform.position.z);
        }
        else
        {
            // Vérifier si le toucher est proche de la balle pour commencer à la déplacer
            Vector3 ballScreenPosition = mainCamera.WorldToScreenPoint(ball.transform.position);
            if (Vector3.Distance(screenPosition, ballScreenPosition) < 50f) // Ajustez le seuil selon vos besoins
            {
                isDragging = true;
                currentPosition = worldPosition;
                lastPosition = worldPosition;

                // Changer le type du corps en Kinematic
                ballRigidbody2D.bodyType = RigidbodyType2D.Kinematic;
            }
        }

        Debug.Log("TOUCH POSITION: " + touchPosition);
        Debug.Log("WORLD POSITION: " + worldPosition);
    }

    // Méthode pour supprimer le SpringJoint2D
    void RemoveSpringJoint()
    {
        Destroy(ballSpringJoint2D);
    }
}
