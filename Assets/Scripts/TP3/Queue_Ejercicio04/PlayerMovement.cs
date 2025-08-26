using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    private float horizontalInput;
    public float HorizontalInput => horizontalInput;

    private bool collided;
    public bool Collided => collided;
    void Start()
    {
        
    }
    void Update()
    {
        Walk();
    }
    private void Walk()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        transform.Translate(Vector2.right * horizontalInput * speed * Time.deltaTime);
        //Debug.Log(horizontalInput);
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
