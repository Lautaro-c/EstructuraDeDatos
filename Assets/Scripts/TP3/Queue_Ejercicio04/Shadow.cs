using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour 
{
    [SerializeField] private GameObject shadow;

    private MyQueue<Vector2> queue;

    private PlayerMovement playerMovement;

    private void Start()
    {
        queue = new MyQueue<Vector2>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }
    private void Update()
    {
        if (playerMovement.Collided)
        {
            EnqueuePlayer();
        }
    }
    private void EnqueuePlayer()
    {
        queue.Enqueue(playerMovement.transform.position);
        StartCoroutine(ShadowSpawner());
    }
    IEnumerator ShadowSpawner()
    {
        Vector2 trans = queue.Dequeue();
        GameObject newShadow = Instantiate(shadow, trans, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(newShadow );
    }
    
}
