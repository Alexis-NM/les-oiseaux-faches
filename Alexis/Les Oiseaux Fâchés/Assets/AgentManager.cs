using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] private GameObject agent1;
    [SerializeField] private GameObject agent2;
    [SerializeField] private GameObject agent3;

    public float changeSpeed = 10;
    private static float timer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        agent1.transform.localScale = new Vector3(2.0f, 0.5f, 0.5f);
        agent2.transform.localScale = new Vector3(1f, 1f, 1f);
        agent3.transform.localScale = new Vector3(2.0f, 0.5f, 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate(agent1, changeSpeed / 5);
        agent2.GetComponent<SpriteRenderer>().material.color = GetSinCosColor(changeSpeed / 2);
        PushUp(agent3.GetComponent<Rigidbody2D>(), 1);
        
    }

    // Rotate on Z axis by a given speed
    void Rotate(GameObject go, float speed)
    {
        go.transform.localRotation = Quaternion.Euler(0, 0, agent1.transform.localEulerAngles.z + speed * Time.deltaTime);
    }

    // Return a new color interpolating R(sin) and B(cos) channels by a given speed
    Color GetSinCosColor(float speed)
    {
        timer += (speed) * Time.deltaTime;
        return new Color(Mathf.Sin(timer), Mathf.Cos(timer), 1, 1);
    }

    // Add a vertical force to the Rigidbody
    void PushUp(Rigidbody2D rb, float force)
    {
        if (rb == null)
        {
            throw new Exception("No Rigidbody detected");
        }
        rb.AddForce(new Vector2(0, force));
    }
}