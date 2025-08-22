using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAction : IUndoableAction
{
    private Entity entity;
    private int fromX, fromY;
    private int toX, toY;

    public MoveAction(Entity entity, int toX, int toY)
    {
        this.entity = entity;
        this.fromX = entity.X;
        this.fromY = entity.Y;
        this.toX = toX;
        this.toY = toY;
    }

    public void Execute()
    {
        entity.X = toX;
        entity.Y = toY;
    }

    public void Undo()
    {
        entity.X = fromX;
        entity.Y = fromY;
    }
}
