using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUndoableAction
{
    void Execute();  // Realiza la acci�n
    void Undo();     // Revierte la acci�n
}
