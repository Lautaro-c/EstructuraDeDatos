using System.Collections;
using System.Collections.Generic;

public class MyBubleSort
{
    public IIndexableList<int> SortedLinkedList(IIndexableList<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            for(int j = 0; j < list.Count - 1 - i; j++)
            {
                if (list[j] > list[j + 1])
                {
                    int tempValue1 = list[j];
                    list[j] = list[j + 1];
                    list[j+1] = tempValue1;;
                }
            }
        }
        return list;
    }
}
