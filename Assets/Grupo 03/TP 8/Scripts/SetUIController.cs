using UnityEngine;
using UnityEngine.UI;

public class SetUIController : MonoBehaviour
{
    public InputField inputA, inputB;
    public Text showA, showB, infoA, infoB, resultado;

    private MySetList<string> setA = new MySetList<string>();
    private MySetList<string> setB = new MySetList<string>();

    public void AddToA()
    {
        setA.Add(inputA.text);
        UpdateUI();
    }

    public void RemoveFromA()
    {
        setA.Remove(inputA.text);
        UpdateUI();
    }

    public void ClearA()
    {
        setA.Clear();
        UpdateUI();
    }

    public void AddToB()
    {
        setB.Add(inputB.text);
        UpdateUI();
    }

    public void RemoveFromB()
    {
        setB.Remove(inputB.text);
        UpdateUI();
    }

    public void ClearB()
    {
        setB.Clear();
        UpdateUI();
    }

    public void UnionSets()
    {
        var union = setA.UnionWith(setB);
        resultado.text = "Unión: " + union.ToString();
    }

    public void IntersectSets()
    {
        var inter = setA.IntersectWith(setB);
        resultado.text = "Intersección: " + inter.ToString();
    }

    public void DifferenceSets()
    {
        var diff = setA.DifferenceWith(setB);
        resultado.text = "Diferencia A - B: " + diff.ToString();
    }

    private void UpdateUI()
    {
        showA.text = "Set A: " + setA.ToString();
        infoA.text = $"Count: {setA.Count()} | Empty: {setA.IsEmpty()}";

        showB.text = "Set B: " + setB.ToString();
        infoB.text = $"Count: {setB.Count()} | Empty: {setB.IsEmpty()}";
    }
}
