using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ShadowManager : MonoBehaviour
{
    [SerializeField] private GameObject shadow;
    private MyQueue<float> queue;
    public MyQueue<float> Queue => queue;
    public float counterAmount;
    private PlayerMovement playerMovement;
    private bool MovementStarted = false;
    private Stopwatch stopwatch;
    private void Start()
    {
        queue = new MyQueue<float>();
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        stopwatch = new Stopwatch();
        stopwatch.Start();
    }

    private void FixedUpdate()
    {
        //if (stopwatch.Elapsed.TotalSeconds > timeToStart)
        //{
        //    ShadowDequeue();
        //}
    }
    public void MovementEnqueue()
    {
        queue.Enqueue(playerMovement.HorizontalInput);
    }
    public void ShadowDequeue()
    {
        if (playerMovement.specificShadow != null)
        {
            if (MovementStarted == false && playerMovement.DoorStarted)
            {
                print("debug");
                playerMovement.specificShadow.SetActive(true);
                MovementStarted = true;
            }
            playerMovement.specificShadow.GetComponent<ShadowMovement>().Movement(queue.Dequeue());
        }
    }

}