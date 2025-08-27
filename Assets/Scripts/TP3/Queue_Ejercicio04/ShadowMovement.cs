using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    public void Movement(float horizontalInput)
    {
        transform.Translate(Vector2.right * horizontalInput * 5f * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("Door"))
            {
                gameObject.SetActive(false);
            }
        }
    }
}
