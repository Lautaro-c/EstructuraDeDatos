using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Clase que gestiona el leaderboard usando un árbol AVL para mantener los puntajes ordenados
public class LeaderboardManager : MonoBehaviour
{
    //Panel donde se instanciarán los elementos visuales del leaderboard
    public Transform contentPanel;
    //Prefab que representa visualmente una entrada de puntaje
    public GameObject scorePrefab;
    //Campo de entrada para el nombre del jugador
    public TMP_InputField inputName;
    //Campo de entrada para el puntaje del jugador
    public TMP_InputField inputScore;
    //Árbol AVL que almacena las entradas de puntaje ordenadas
    public AVLTree<ScoreEntry> scoreTree = new AVLTree<ScoreEntry>();

    //Se ejecuta al iniciar el juego
    void Start()
    {
        //Genera 100 entradas de puntaje aleatorias y las inserta en el árbol
        for (int i = 0; i < 100; i++)
        {
            string name = "Player" + i;
            int score = Random.Range(0, 1000);
            scoreTree.Insert(new ScoreEntry(name, score));
        }
        //Actualiza la UI con los puntajes ordenados usando InOrder
        UpdateUI(scoreTree.InOrderTraversal());
    }

    //Agrega una nueva entrada de puntaje al árbol y actualiza la UI
    public void AddNewScore(string name, int score)
    {
        scoreTree.Insert(new ScoreEntry(name, score));
        UpdateUI(scoreTree.InOrderTraversal());
    }

    //Actualiza la UI instanciando los elementos visuales del leaderboard
    void UpdateUI(SimpleList<ScoreEntry> scores)
    {
        //Elimina todos los elementos visuales anteriores
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }
        //Instancia un nuevo elemento por cada entrada de puntaje
        for (int i = 0; i < scores.Count; i++)
        {
            GameObject item = Instantiate(scorePrefab, contentPanel);
            item.GetComponentInChildren<TextMeshProUGUI>().text = scores[i].ToString();
        }
    }

    //Obtiene los datos desde los campos de entrada y los agrega al árbol
    public void AddScoreInput()
    {
        string playerName = inputName.text;
        //Valida que el puntaje ingresado sea un número entero y el nombre no este vacio
        if (int.TryParse(inputScore.text, out int score) && ! string.IsNullOrWhiteSpace(inputName.text))
        {
            AddNewScore(playerName, score);
            //Limpia los campos de entrada
            inputName.text = "";
            inputScore.text = "";
        }
        else
        {
            Debug.LogWarning("Puntaje o nombre inválido");
        }
    }

    //Actualiza la UI usando recorrido LevelOrder
    public void LevelOrder()
    {
        SimpleList<Node<ScoreEntry>> nodes = scoreTree.LevelOrder();
        SimpleList<ScoreEntry> scores = ConvertToList(nodes);
        UpdateUI(scores);
    }

    //Actualiza la UI usando recorrido PreOrder
    public void PreOrder()
    {
        SimpleList<Node<ScoreEntry>> nodes = scoreTree.PreOrder();
        SimpleList<ScoreEntry> scores = ConvertToList(nodes);
        UpdateUI(scores);
    }

    //Actualiza la UI usando recorrido PostOrder
    public void PostOrder()
    {
        SimpleList<Node<ScoreEntry>> nodes = scoreTree.PostOrder();
        SimpleList<ScoreEntry> scores = ConvertToList(nodes);
        UpdateUI(scores);
    }

    //Actualiza la UI usando recorrido InOrder
    public void InOrder()
    {
        SimpleList<Node<ScoreEntry>> nodes = scoreTree.InOrder();
        SimpleList<ScoreEntry> scores = ConvertToList(nodes);
        UpdateUI(scores);
    }

    //Convierte una lista de nodos en una lista de ScoreEntry
    public SimpleList<ScoreEntry> ConvertToList(SimpleList<Node<ScoreEntry>> nodeList)
    {
        SimpleList<ScoreEntry> list = new SimpleList<ScoreEntry>();
        for (int i = 0; i < nodeList.Count; i++)
        {
            list.Add(nodeList[i].Data);
        }
        return list;
    }
}