using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour 
{
    [SerializeField] private GameObject shadow;
    private MyQueue<float> queue;
    public MyQueue<float> Queue => queue;
    public float counterAmount;
    private PlayerMovement playerMovement;
    private bool isCollided = false;
    private void Start()
    {
        queue = new MyQueue<float>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }

    public void MovementEnqueue()
    {
        queue.Enqueue(playerMovement.HorizontalInput);
    }
    public void ShadowDequeue()
    {
        if (playerMovement.specificShadow != null)
        {
            if (isCollided == false)
            {
                playerMovement.specificShadow.SetActive(true);
                isCollided = true;
            }
            playerMovement.specificShadow.GetComponent<ShadowMovement>().Movement(queue.Dequeue());
        }
    }

}
