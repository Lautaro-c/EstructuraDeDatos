public abstract class MySet<T>
{
    public T[] elements;
    public abstract bool Add(T item);
    public abstract void Remove(T item);
    public abstract void Clear();
    public abstract bool Contains(T item);
    public abstract void Show();
    public abstract override string ToString();
    public abstract int Count();
    public abstract bool IsEmpty();
    public abstract MySet<T> UnionWith(MySet<T> other);
    public abstract MySet<T> IntersectWith(MySet<T> other);
    public abstract MySet<T> DifferenceWith(MySet<T> other);
}