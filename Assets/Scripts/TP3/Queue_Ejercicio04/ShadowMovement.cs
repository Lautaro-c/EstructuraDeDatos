using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class ShadowMovement : MonoBehaviour
{
    private ShadowManager shadowManager;
    private void Start()
    {
        shadowManager = FindAnyObjectByType<ShadowManager>();
    }
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
                this.gameObject.SetActive(false);
                shadowManager.Queue.Clear();
                Debug.Log("SHADOW COLISION PUERTA");
            }
        }
    }
}
