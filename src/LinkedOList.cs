
namespace OCollection;

public class LinkedOList<T> : IEnumerable<T>
{
    private LinkedOListNode<T>? Current;
    private int _Size = 0;

    public T this[int index] 
    {
        get {
            if (Current is null) throw new Exception("NO ELEMENTS");

            var downscaled = (index >= 0) ? index % _Size : -(Math.Abs(index) % _Size) ;

            LinkedOListNode<T> tmp = Current;
            while(downscaled != 0) {
                if (downscaled > 0) {
                    tmp = tmp.Next;
                    downscaled--;
                } else {
                    tmp = tmp.Prev;
                    downscaled++;
                }
            }
            return tmp.Data;
        }
        set {
            if (Current is null) throw new Exception("NO ELEMENTS");

            var downscaled = (index >= 0) ? index % _Size : -(Math.Abs(index) % _Size) ;

            LinkedOListNode<T> tmp = Current;
            while(downscaled != 0) {
                if (downscaled > 0) {
                    tmp = tmp.Next;
                    downscaled--;
                } else {
                    tmp = tmp.Prev;
                    downscaled++;
                }
            }
            tmp.Data = value;
        }
    }

    public void Insert(T element)
    {
        _Size++;
        if (Current is null) { 
            Current = new LinkedOListNode<T>(element); 
            return;
        }
        var newNode = new LinkedOListNode<T>(element, Current.Next, Current);
        Current.Next.Prev = newNode;
        Current.Next = newNode;
    }

    public void InsertBack(T element)
    {
        _Size++;
        if (Current is null) { 
            Current = new LinkedOListNode<T>(element);
            return;
        }
        var newNode = new LinkedOListNode<T>(element, Current, Current.Prev);
        Current.Prev.Next = newNode;
        Current.Prev = newNode;
    }

    public void Rotate(int amount)
    {
        if (Current is null) return;

        var downscaled = (amount >= 0) ? amount % _Size : -(Math.Abs(amount) % _Size) ;

        while(downscaled != 0) {
            if (downscaled > 0) {
                Current = Current.Next;
                downscaled--;
            } else {
                Current = Current.Prev;
                downscaled++;
            }
        }
    }

    public int Size 
    {
        get  {
            return _Size;
        }
    }

    public void ConForeach(Action<T> action)
    {
        int counter = 0;
        while(true) {
            action(this[counter]);
            counter++;
        }
    }

    public void ConForeach(Predicate<T> action)
    {
        int counter = 0;
        bool cont = true;
        while(cont) {
            cont = action(this[counter]);
            counter++;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new LinkedOListEnum<T>(Current);
    }

    private System.Collections.IEnumerator GetEnumerator1()
    {
        return this.GetEnumerator();
    }
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator1();
    }
}

public class LinkedOListNode<T>
{
    internal T Data;
    internal LinkedOListNode<T> Next;
    internal LinkedOListNode<T> Prev;

    public LinkedOListNode(T d, LinkedOListNode<T> n, LinkedOListNode<T> p)
    {
        Data = d; Next = n; Prev = p;
    }

    public LinkedOListNode(T d)
    {
        Data = d; Next = this; Prev = this;
    }
}

public class LinkedOListEnum<T> : IEnumerator<T>
{
    private LinkedOListNode<T>? start;
    private LinkedOListNode<T>? current;

    public LinkedOListEnum(LinkedOListNode<T>? s)
    {
        start = s; current = null;
    }

    object System.Collections.IEnumerator.Current
    {
        get 
        {
            if (current is null || current.Data is null) throw new Exception("MoveNext not called yet");
            return current.Data; 
        }
    }
    T IEnumerator<T>.Current
    {
        get 
        {
            if (current is null) throw new Exception("MoveNext not called yet");
            return current.Data; 
        }
    }
    public bool MoveNext() 
    { 
        if (current is null) {
            current = start;
            return start is not null;
        } else {
            current = current.Next;  
            return current != start;
        }
    }
    public void Reset() { current = null; }
    public void Dispose() { start = null; current = null; }
}