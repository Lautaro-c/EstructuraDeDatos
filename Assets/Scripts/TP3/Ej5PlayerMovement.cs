using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.InputManagerEntry;
using UnityEngine.UIElements;

public class Ej5PlayerMovement : MonoBehaviour
{
    private MyStack<Vector3> myStack;
    private int speed;
    private void Start()
    {
        myStack = new MyStack<Vector3>();
        speed = 1;
    }

    private void Update()
    {
        MovePlayer();
        Undo();
        Debug.Log(myStack.Count.ToString());
    }
    private void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 newPos = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            myStack.Push(transform.position);
            transform.position = newPos;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 newPos = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            myStack.Push(transform.position);
            transform.position = newPos;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            myStack.Push(transform.position);
            transform.position = newPos;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
            myStack.Push(transform.position);
            transform.position = newPos;
        }
    }
    private void Undo()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            transform.position = myStack.Pop();
        }
    }
}
