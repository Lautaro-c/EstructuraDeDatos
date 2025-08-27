using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour 
{
    [SerializeField] private GameObject shadow;
    private MyQueue<float> queue;

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

   
    //IEnumerator ShadowSpawner()
    //{
    //    Vector2 trans = queue.Dequeue();
    //    GameObject newShadow = Instantiate(shadow, trans, Quaternion.identity);
    //    yield return new WaitForSeconds(1f);
    //}
}
