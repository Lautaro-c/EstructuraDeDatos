
public class ItemSOSorter
{
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
