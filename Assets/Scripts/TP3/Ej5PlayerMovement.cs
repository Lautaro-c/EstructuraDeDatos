using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.InputManagerEntry;
using UnityEngine.UIElements;

public class Ej5PlayerMovement : MonoBehaviour
{
    private MyStack<Vector3> posStack;
    private MyStack<Quaternion> rotStack;
    private MyStack<string> stacksStack;
    private int speed;
    private int rotation;
    private const string posString = "Pos";
    private const string rotString = "Rot";
    private void Start()
    {
        posStack = new MyStack<Vector3>();
        rotStack = new MyStack<Quaternion>();
        stacksStack = new MyStack<string>();
        speed = 1;
        rotation = -15;
    }

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
        Undo();
    }
    private void MovePlayer()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 newPos = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            posStack.Push(transform.position);
            transform.position = newPos;
            stacksStack.Push(posString);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 newPos = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            posStack.Push(transform.position);
            transform.position = newPos;
            stacksStack.Push(posString);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            posStack.Push(transform.position);
            transform.position = newPos;
            stacksStack.Push(posString);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
            posStack.Push(transform.position);
            transform.position = newPos;
            stacksStack.Push(posString);
        }
    }

    public void RotatePlayer()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            rotStack.Push(transform.rotation);
            transform.Rotate(0, 0, rotation);
            stacksStack.Push(rotString);
        }
    }
    private void Undo()
    {
        if (Input.GetKeyDown(KeyCode.Z) && stacksStack.Count > 0)
        {
            switch (stacksStack.Peek())
            {
                case posString:
                    Debug.Log(posString);
                    stacksStack.Pop();
                    transform.position = posStack.Pop();
                break;
                case rotString:
                    Debug.Log(rotString);
                    stacksStack.Pop();
                    transform.rotation = rotStack.Pop();
                break;
            }
        }
    }
}
