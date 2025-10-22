using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyALGraph<T>
{
    Dictionary<T, List<(T to, float weight)>> adjList = new Dictionary<T, List<(T to, float weight)>>();
    public IEnumerable<T> Vertices => adjList.Keys;
    public void AddVertex (T vertex)
    {
        if (!adjList.ContainsKey(vertex))
        {
            adjList.Add(vertex, new List<(T to, float weight)>()); // key: vertex, value: neighbours list
        }
    }
    public void RemoveVertex (T vertex)
    {
        if (adjList.ContainsKey(vertex))
        {
            adjList.Remove(vertex);
            foreach (T vtx in adjList.Keys)
            {
                adjList[vtx].RemoveAll(e => Equals(e.to, vertex));
            }
        }
    }
    public void AddEdge (T from, (T to, float weight) edge) // edge is a tuple (neighbour, weight)
    {
        var (to, weight) = edge;

        if (!adjList.ContainsKey(from)) adjList[from] = new List<(T, float)>();
        if (!adjList.ContainsKey(to)) adjList[to] = new List<(T, float)>();

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
            adjList[from].RemoveAll(e => Equals(e.to, to));
        }
    }
    public bool ContainsVertex(T vertex)
    {
        if (adjList.ContainsKey(vertex)) return true;
        else return false;
    }
    public bool ContainsEdge(T from, T to)
    {
        if (adjList.ContainsKey(from))
        {
            foreach (var edge in adjList[from])
            {
                if (Equals(edge.to, to)) return true;
            }
        }
        return false;
    }
    public float GetWeight(T from, T to)
    {
        if (adjList.ContainsKey(from))
        {
            foreach (var edge in adjList[from])
            {
                if (Equals(edge.to, to)) return edge.weight;
            }
        }
        return -1; // indica que no existe la arista
    }
}
