using System;

[System.Serializable]
public class ScoreEntry : IComparable<ScoreEntry>
{
    public string playerName;
    public int score;

    public ScoreEntry(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }

    public int CompareTo(ScoreEntry other)
    {
        return other.score.CompareTo(this.score); // Orden descendente
    }

    public override string ToString()
    {
        return $"{playerName}: {score}";
    }
}