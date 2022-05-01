
namespace OCollection;

public class OArray<T>  : IEnumerable<T>
{
    private T[] Contents;
    private int Start;

    public OArray(int size)
    {
        if (size < 1) throw new ArgumentException("OArrays of size 0, are not allowed.");
        Contents = new T[size];
        Start = 0;
    }

    public T this[int index] 
    {
        get {
            return Contents[CalculateIndex(index)];
        }

        set {
            Contents[CalculateIndex(index)] = value;
        }
    }

    private int CalculateIndex(int index)
    {
        if (index >= 0) return (Start + index) % Contents.Count();
        else {
            if (Math.Abs(index) % Contents.Count() == 0) return Start;
            else return Contents.Count() - ((Math.Abs(index) % Contents.Count()) - Start);
        }
    }

    public void Rotate(int amount)
    {
        Start = CalculateIndex(amount);
    }

    public int Size
    {
        get { 
            return Contents.Count(); 
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
        return Contents.AsEnumerable().GetEnumerator();
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