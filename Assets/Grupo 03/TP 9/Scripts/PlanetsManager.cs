using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlanetsManager : MonoBehaviour
{
    MyALGraph<GameObject> myALGraph;
    SimpleList<GameObject> planetsList;
    [SerializeField] private TextMeshProUGUI lenghtText;
    [SerializeField] private GameObject Mercury;
    [SerializeField] private GameObject Venus;
    [SerializeField] private GameObject Earth;
    [SerializeField] private GameObject Mars;
    [SerializeField] private GameObject Jupiter;
    [SerializeField] private GameObject Saturn;
    [SerializeField] private GameObject Uranus;
    [SerializeField] private GameObject Neptune;
    [SerializeField] private TextMeshProUGUI MEConnection;
    [SerializeField] private TextMeshProUGUI EVConnection;
    [SerializeField] private TextMeshProUGUI UJConnection;
    [SerializeField] private TextMeshProUGUI JMConnection;
    [SerializeField] private TextMeshProUGUI MSConnection;
    [SerializeField] private TextMeshProUGUI SNConnection;
    [SerializeField] private TextMeshProUGUI EMConnection;
    void Start()
    {
        myALGraph = new MyALGraph<GameObject>();
        planetsList = new SimpleList<GameObject>();
        myALGraph.AddVertex(Mercury);
        myALGraph.AddVertex(Venus);
        myALGraph.AddVertex(Earth);
        myALGraph.AddVertex(Mars);
        myALGraph.AddVertex(Jupiter);
        myALGraph.AddVertex(Saturn);
        myALGraph.AddVertex(Uranus);
        myALGraph.AddVertex(Neptune);
        EVConnection.text = AddWeightByDistance(Earth, Venus).ToString();
        MEConnection.text = AddWeightByDistance(Earth, Mercury).ToString();
        EMConnection.text = AddWeightByDistance(Earth, Mars).ToString();
        JMConnection.text = AddWeightByDistance(Mars, Jupiter).ToString();
        UJConnection.text = AddWeightByDistance(Jupiter, Uranus).ToString();
        MSConnection.text = AddWeightByDistance(Mars, Saturn).ToString();
        SNConnection.text = AddWeightByDistance(Saturn, Neptune).ToString();
    }

    private float AddWeightByDistance(GameObject planet1, GameObject planet2)
    {
        float distance = Vector2.Distance(planet1.transform.position, planet2.transform.position);
        myALGraph.AddEdge(planet1, (planet2, distance));
        myALGraph.AddEdge(planet2, (planet1, distance));
        return distance;
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
