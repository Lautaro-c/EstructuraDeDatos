using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyALGraph<T>
{
    Dictionary<T, List<(T, int)>> adjList = new Dictionary<T, List<(T, int)>>();
    public IEnumerable<T> Vertices()
    {
        return adjList.Keys;
    }
    public void AddVertex (T vertex)
    {
        if (!adjList.ContainsKey(vertex))
        {
            adjList.Add(vertex, new List<(T, int)>()); // key: vertex, value: neighbours list

        }
    }
    public void RemoveVertex (T vertex)
    {
        if (adjList.ContainsKey(vertex))
        {
            adjList.Remove(vertex);
            foreach (T vtx in adjList.Keys)
            {
                adjList[vtx].RemoveAll(e => EqualityComparer<T>.Default.Equals(e.Item1, vertex)); // removes edge from each neighbour list (reference)
            }
        }
    }
    public void AddEdge (T from, (T to,int weight) edge) // edge is a tuple (neighbour, weight)
    {
        var (to, weight) = edge;

        if (!adjList.ContainsKey(from)) adjList[from] = new List<(T, int)>();
        if (!adjList.ContainsKey(to)) adjList[to] = new List<(T, int)>();

        var list = adjList[from];
        bool exists = false;
        for (int i=0; i<list.Count; i++)
        {
            if (Equals(list[i].Item1, to) && list[i].Item2 == weight) // si ya existe una tupla igual, no agrega
            {
                exists = true;
                break;
            }
        }
        if (!exists)
        {
            list.Add((to, weight));
        }
    }
    public void RemoveEdge (T from, T to)
    {
        if (adjList.ContainsKey(from))
        {
            adjList[from].RemoveAll(e => EqualityComparer<T>.Default.Equals(e.Item1, to)); // removes edge from neighbour list (reference)
        }
    }
}   
