using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetsManager : MonoBehaviour
{
    MyALGraph<GameObject> myALGraph;
    SimpleList<GameObject> planetsList;
    [SerializeField] private TextMeshProUGUI lenghtText;
    [SerializeField] private GameObject Mercurio;
    [SerializeField] private GameObject Venus;
    [SerializeField] private GameObject Tierra;
    [SerializeField] private GameObject Marte;
    [SerializeField] private GameObject Jupiter;
    [SerializeField] private GameObject Saturno;
    [SerializeField] private GameObject Urano;
    [SerializeField] private GameObject Neptuno;
    void Start()
    {
        myALGraph = new MyALGraph<GameObject>();
        planetsList = new SimpleList<GameObject>();
        myALGraph.AddVertex(Mercurio);
        myALGraph.AddVertex(Venus);
        myALGraph.AddVertex(Tierra);
        myALGraph.AddVertex(Marte);
        myALGraph.AddVertex(Jupiter);
        myALGraph.AddVertex(Saturno);
        myALGraph.AddVertex(Urano);
        myALGraph.AddVertex(Neptuno);
        AddWeightByDistance(Tierra, Venus);
        AddWeightByDistance(Tierra, Mercurio);
        AddWeightByDistance(Tierra, Marte);
        AddWeightByDistance(Marte, Jupiter);
        AddWeightByDistance(Jupiter, Urano);
        AddWeightByDistance(Marte, Saturno);
        AddWeightByDistance(Saturno, Neptuno);
    }

    private void AddWeightByDistance(GameObject planet1, GameObject planet2)
    {
        float distance = Vector2.Distance(planet1.transform.position, planet2.transform.position);
        myALGraph.AddEdge(planet1, (planet2, distance));
        myALGraph.AddEdge(planet2, (planet1, distance));
    }

    public void PlanetClicked (GameObject planet)
    {
        planetsList.Add(planet);
    }

    public void CheckTrayect()
    {
        float totalDistance = 0f;
        for (int i = 0; i < planetsList.Count - 1; i++)
        {
            if(myALGraph.ContainsEdge(planetsList[i], planetsList[i +1]))
            {
                totalDistance += myALGraph.GetWeight(planetsList[i], planetsList[i + 1]);
            }
            else
            {
                lenghtText.text = "The trayectory isn't valid";
                planetsList.Clear();
                return;
            }
        }
        planetsList.Clear();
        lenghtText.text = "The trayectory is valid, total energy cost: " + totalDistance;
    }
}
