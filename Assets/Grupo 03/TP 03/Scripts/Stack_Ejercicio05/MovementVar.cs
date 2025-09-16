using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementVar
{
    private Vector3 position;
    private Quaternion rotation;
    public Vector3 Position => position;
    public Quaternion Rotation => rotation;

    public MovementVar(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
