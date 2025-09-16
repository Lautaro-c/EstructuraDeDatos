using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.InputManagerEntry;
using UnityEngine.UIElements;

public class Ej5PlayerMovement : MonoBehaviour
{
    private MyStack<MovementVar> movementStack;
    private int speed;
    private int rotation;
    private void Start()
    {
        movementStack = new MyStack<MovementVar>();
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
            MovementVar movement = new MovementVar(transform.position, transform.rotation);
            movementStack.Push(movement);
            transform.position = newPos;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Vector3 newPos = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            MovementVar movement = new MovementVar(transform.position, transform.rotation);
            movementStack.Push(movement);
            transform.position = newPos;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
            MovementVar movement = new MovementVar(transform.position, transform.rotation);
            movementStack.Push(movement);
            transform.position = newPos;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
            MovementVar movement = new MovementVar(transform.position, transform.rotation);
            movementStack.Push(movement);
            transform.position = newPos;
        }
    }

    public void RotatePlayer()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            MovementVar movement = new MovementVar(transform.position, transform.rotation);
            movementStack.Push(movement);
            transform.Rotate(0, 0, rotation);
        }
    }
    private void Undo()
    {
        if (Input.GetKeyDown(KeyCode.Z) && movementStack.Count > 0)
        {
            MovementVar movement = movementStack.Pop();
            transform.position = movement.Position;
            transform.rotation = movement.Rotation;
        }
    }
}
