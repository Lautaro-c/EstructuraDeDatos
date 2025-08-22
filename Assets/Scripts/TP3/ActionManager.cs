using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager
{
    private MyStack<IUndoableAction> undoStack = new MyStack<IUndoableAction>();

    public void PerformAction(IUndoableAction action)
    {
        action.Execute();
        undoStack.Push(action);
    }

    public void UndoLast()
    {
        if (undoStack.TryPop(out IUndoableAction last))
        {
            last.Undo();
        }
    }
}

