
namespace OCollection;

public class ODictionary<L,R>
{
    private HashSet<(L,R)> Data = new HashSet<(L, R)>();

    public void Add(L l, R r) {
        Data.Add((l,r));
    }

    public R LtoR(L l) {
        return Data.Where((e) => e.Item1.Equals(l)).First().Item2;
    }

    public L RtoL(R r) {
        return Data.Where((e) => e.Item2.Equals(r)).First().Item1;
    }

    public R Get(L l) {
        return LtoR(l);
    }

    public L Get(R r) {
        return RtoL(r);
    }
}