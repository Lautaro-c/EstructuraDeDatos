using Unity.VisualScripting;

public interface IIndexableList<T>
{
    public int Count { get; }
    public T this[int index] { get; set; }
    public void Add(T item) { }
    public void Clear() { }
    public void Remove(T item) { }

    public void RemoveAt(int index) { }
}