
namespace OtherCollections;

public class LAList<T>
{
    private LANode<T> Data;
    private bool FixedIncrements = false;
    private int Increment = 10;
    private int Length = 0;

    public LAList() {
        Data = new LANode<T>(10);
    }

    public LAList(int size) {
        Data = new LANode<T>(size);
        FixedIncrements = true;
        Increment = size;
    }

    public void Add(T element) {
        int skips = 0;
        int idx = 0;
        if (FixedIncrements) {
            skips = Length / Increment;
            idx = Length - (skips * Increment);
        }
        else {
            while(10 * Math.Pow(2,skips) <= Length) {
                idx += 10 * (int)Math.Pow(2,skips);
                skips++;
            }
            idx = Length - idx;
        };

        var node = Data;
        while(skips > 0) { 
            if (node.Next is null) {
                node.Next = new LANode<T>(Increment);
                Increment = (FixedIncrements) ? Increment : Increment*2;
            }
            node = node.Next; 
            skips--; 
        }

        node.Data[idx] = element;
        Length++;
    }

    public T this[int index] 
    {
        get {
            if (index >= Length) throw new IndexOutOfRangeException();

            int skips = 0;
            int idx = 0;
            if (FixedIncrements) {
                skips = index / Increment;
                idx = index - (skips * Increment);
            }
            else {
                while(10 * Math.Pow(2,skips) <= index) {
                    idx += 10 * (int)Math.Pow(2,skips);
                    skips++;
                }
                idx = index - idx;
            };

            var node = Data;
            while(skips > 0) { 
                if (node.Next is null) throw new IndexOutOfRangeException();

                node = node.Next; 
                skips--; 
            }

            return node.Data[idx];
        }

        // set {}
    }
}

internal class LANode<T>
{
    public T[] Data;
    public LANode<T>? Next;

    public LANode(int size) {
        Data = new T[size];
    }
}