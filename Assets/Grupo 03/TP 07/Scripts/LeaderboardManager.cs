using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject scorePrefab;
    public TMP_InputField inputName;
    public TMP_InputField inputScore;
    public AVLTree<ScoreEntry> scoreTree = new AVLTree<ScoreEntry>();

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            string name = "Player" + i;
            int score = UnityEngine.Random.Range(0, 1000);
            scoreTree.Insert(new ScoreEntry(name, score));
        }
        UpdateUI(scoreTree.InOrderTraversal());

    }

    public void AddNewScore(string name, int score)
    {
        scoreTree.Insert(new ScoreEntry(name, score));
        UpdateUI(scoreTree.InOrderTraversal());
    }

    void UpdateUI(List<ScoreEntry> scores)
    {
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

        foreach (var entry in scores)
        {
            GameObject item = Instantiate(scorePrefab, contentPanel);
            item.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = entry.ToString();
        }
    }

    public void AddScoreInput()
    {
        string playerName = inputName.text;
        if (int.TryParse(inputScore.text, out int score))
        {
            AddNewScore(playerName, score);
            inputName.text = "";
            inputScore.text = "";
        }
        else
        {
            Debug.LogWarning("Puntaje inválido");
        }
    }
    public void LevelOrder()
    {
        var nodes = scoreTree.LevelOrder();  // SimpleList<Node<ScoreEntry>>
        List<ScoreEntry> scores = ConvertToList(nodes);
        UpdateUI(scores);
    }

    public void PreOrder()
    {
        var nodes = scoreTree.PreOrder();
        List<ScoreEntry> scores = ConvertToList(nodes);
        UpdateUI(scores);
    }

    public void PostOrder()
    {
        var nodes = scoreTree.PostOrder();
        List<ScoreEntry> scores = ConvertToList(nodes);
        UpdateUI(scores);
    }

    public void InOrder()
    {
        var nodes = scoreTree.InOrder();
        List<ScoreEntry> scores = ConvertToList(nodes);
        UpdateUI(scores);
    }
    public List<ScoreEntry> ConvertToList(SimpleList<Node<ScoreEntry>> nodeList)
    {
        List<ScoreEntry> list = new List<ScoreEntry>();
        for (int i = 0; i < nodeList.Count; i++)
        {
            list.Add(nodeList[i].Data);
        }
        return list;
    }



}