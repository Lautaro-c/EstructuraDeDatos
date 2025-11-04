using System;

//Marca la clase como serializable para que pueda ser guardada o mostrada en el Inspector de Unity
[System.Serializable]
public class ScoreEntry : IComparable<ScoreEntry>
{
    //Nombre del jugador
    public string playerName;
    //Puntaje del jugador
    public int score;

    //Constructor que inicializa una entrada de puntaje con nombre y puntaje
    public ScoreEntry(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }

    //Implementa la interfaz IComparable para poder ordenar las entradas
    public int CompareTo(ScoreEntry other)
    {
        //Compara los puntajes en orden descendente (mayor puntaje primero)
        return other.score.CompareTo(this.score);
    }

    //Devuelve una representación en texto de la entrada (nombre: puntaje)
    public override string ToString()
    {
        return $"{playerName}: {score}";
    }
}