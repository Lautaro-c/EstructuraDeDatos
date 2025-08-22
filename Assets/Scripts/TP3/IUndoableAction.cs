using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUndoableAction
{
    void Execute();  // Realiza la acción
    void Undo();     // Revierte la acción
}
