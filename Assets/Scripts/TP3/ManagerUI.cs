using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
public class ManagerUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown moveDropdown;
    [SerializeField] private TMP_Text positionText;
    [SerializeField] private Button moveButton;
    [SerializeField] private Button undoButton;

    private Entity player;
    private ActionManager actionManager;

    private void Start()
    {
        player = new Entity();
        actionManager = new ActionManager();

        moveButton.onClick.AddListener(HandleMove);
        undoButton.onClick.AddListener(HandleUndo);
        UpdateUI();
    }

    private void HandleMove()
    {
        int selected = moveDropdown.value;
        int newX = selected * 2; // Ejemplo: cada opción mueve 2 unidades
        int newY = selected * 3;

        var move = new MoveAction(player, newX, newY);
        actionManager.PerformAction(move);
        UpdateUI();
    }

    private void HandleUndo()
    {
        actionManager.UndoLast();
        UpdateUI();
    }

    private void UpdateUI()
    {
        positionText.text = $"Posición: ({player.X}, {player.Y})";
    }
}