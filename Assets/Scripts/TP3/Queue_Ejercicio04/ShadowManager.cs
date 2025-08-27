using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowManager : MonoBehaviour 
{
    [SerializeField] private GameObject shadow;
    private MyQueue<float> queue;
    public float counterAmount;
    private PlayerMovement playerMovement;
    private bool isCollided = false;
    private void Start()
    {
        queue = new MyQueue<float>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
    }
    private void Update()
    {
    }

    public void MovementEnqueue()
    {
        queue.Enqueue(playerMovement.HorizontalInput);
        counterAmount = queue.Count;
    }
    public void ShadowDequeue()
    {
        if(isCollided == false)
        {
            playerMovement.specificShadow.SetActive(true);
        }
        else
        {
            queue.Clear();
        }
        
        playerMovement.specificShadow.GetComponent<ShadowMovement>().Movement(queue.Dequeue());
    }

}
