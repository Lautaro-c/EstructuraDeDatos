using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    private float horizontalInput;
    public float HorizontalInput => horizontalInput;

    private ShadowManager shadow;
    private bool collided;
    public bool Collided => collided;

    public GameObject specificShadow;

    public bool DoorStarted = false;

    private float secondCounter = 0f;
    private float maxTime = 3f;
    private bool startTimer = false;
    void Start()
    {
        shadow = FindAnyObjectByType<ShadowManager>();
    }
    void FixedUpdate()
    {
        Walk();
    }
    private void Walk()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);

        if (horizontalInput != 0 && collided == false && DoorStarted)
        {
            shadow.MovementEnqueue();
            //Debug.Log("Enqueue");
        }
        if (startTimer)
        {
            secondCounter += Time.deltaTime;
            if (secondCounter >= maxTime && DoorStarted)
            {
                shadow.ShadowDequeue();
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Door"))
            {
                collided = true;
                Debug.Log("collided true");
            }
            if (collision.CompareTag("DoorStart"))
            {
                startTimer = true;
                DoorStarted = true;

            }
        }
    }
}


