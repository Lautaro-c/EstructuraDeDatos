
public class ItemSOSorter
{
    //     Colección similar a una                                       Func es un delegado generico
    //     lista con elementos tipo                                      (permite tratar a una función como una variable)
    //     ItemSO                                                        <ItemSO, T> es la firma
    //                                                                   Es una promesa de que te voy a pedir algo despues 
    //                                                                   Y func es un ticket especial que te dice que hacer y que me 
    //                                                                   devolves
    //                                                                   Es una funcion con un return
    public IIndexableList<ItemSO> SortBy<T>(IIndexableList<ItemSO> list, System.Func<ItemSO, T> selector) where T : System.IComparable<T>
    {
        for (int i = 1; i < list.Count; i++)
        {
            ItemSO key = list[i];
            T keyValue = selector(key);
            int j = i - 1;

            while (j >= 0 && selector(list[j]).CompareTo(keyValue) > 0)
            {
                list[j + 1] = list[j];
                j--;
            }
            list[j + 1] = key;
        }
        return list;
    }
}
