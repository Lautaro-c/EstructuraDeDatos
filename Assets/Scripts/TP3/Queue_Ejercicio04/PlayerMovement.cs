using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    private float horizontalInput;
    public float HorizontalInput => horizontalInput;

    private Shadow shadow;
    private bool collided;
    public bool Collided => collided;

    public GameObject specificShadow;
    void Start()
    {
        shadow = FindAnyObjectByType<Shadow>();
    }
    void Update()
    {
        Walk();
    }
    private void Walk()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);

        if (horizontalInput != 0 && collided == false) 
        {
            shadow.MovementEnqueue();
            Debug.Log("Enqueue");
        }
        if(collided)
        {
            shadow.ShadowDequeue();
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
        }
    }
}
