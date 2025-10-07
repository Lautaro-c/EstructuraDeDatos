using System.Collections.Generic;
using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public Transform contentPanel;
    public GameObject scorePrefab;
    public AVLTree<ScoreEntry> scoreTree = new AVLTree<ScoreEntry>();

    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            string name = "Player" + i;
            int score = UnityEngine.Random.Range(0, 1000);
            scoreTree.Insert(new ScoreEntry(name, score));
        }
        UpdateUI();
    }

    public void AddNewScore(string name, int score)
    {
        scoreTree.Insert(new ScoreEntry(name, score));
        UpdateUI();
    }

    void UpdateUI()
    {
        foreach (Transform child in contentPanel)
            Destroy(child.gameObject);

        List<ScoreEntry> scores = scoreTree.InOrderTraversal(); // Implementar en AVL
        foreach (var entry in scores)
        {
            GameObject item = Instantiate(scorePrefab, contentPanel);
            item.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = entry.ToString();
        }
    }
}