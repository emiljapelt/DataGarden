
namespace OCollection;

public class TreeNode<K,V> where K : IComparable<K>
{
    internal readonly K Key;
    internal V Value;
    internal int Size = 1;
    internal TreeNode<K,V>? LeftSub;
    internal TreeNode<K,V>? RightSub;

    public TreeNode(K key, V value) {
        Key = key;
        Value = value;
    }

    public List<TreeNode<K, V>> ToList()
    {
        var list = new List<TreeNode<K,V>>();
        _ToList(LeftSub, list);
        list.Add(this);
        _ToList(RightSub, list);
        return list;
    }

    private void _ToList(TreeNode<K,V>? node, List<TreeNode<K,V>> dest)
    {
        if (node is null) return;
        _ToList(node.LeftSub, dest);
        dest.Add(node);
        _ToList(node.RightSub, dest);
    }
}

public class ScapeGoatTree<K,V> where K : IComparable<K>
{
    private TreeNode<K,V>? StartPoint;
    private double Balance;

    public ScapeGoatTree(double balance) {
        if (!(0.5f < balance && balance < 1.0f)) throw new Exception("Balance must be between 0.5 and 1.0");
        Balance = balance;
    }

    public void Print() 
    {
        _Print(StartPoint, 0);
    }

    private void _Print(TreeNode<K,V>? node, int depth)
    {
        if (depth > 0) {
            for(int i = 0; i < (depth*2)-2; i++) System.Console.Write(" ");
            System.Console.Write("└ ");
        }
        if (node is null) System.Console.WriteLine("-");
        else { 
            System.Console.WriteLine($"{node.Key} -> {node.Value}");
            _Print(node.LeftSub, depth+1);
            _Print(node.RightSub, depth+1);
        }
    }

    public void Insert(K key, V value)
    {
        if (StartPoint is null) {
            StartPoint = new TreeNode<K,V>(key, value);
            return;
        }

        var depth = 0;
        var path = new List<bool>(); // true = right
        var currentNode = StartPoint;
        while(true) {
            depth++;
            currentNode.Size++;
            var comparison = currentNode.Key.CompareTo(key);
            if (comparison == 0) {
                currentNode.Value = value;
                return;
            }
            else if (comparison > 0) {
                path.Add(false);
                if(currentNode.LeftSub is null) {
                    currentNode.LeftSub = new TreeNode<K,V>(key, value);
                    break;
                }
                else currentNode = currentNode.LeftSub;
            }
            else {
                path.Add(true);
                if(currentNode.RightSub is null) { 
                    currentNode.RightSub = new TreeNode<K,V>(key, value);
                    break;
                }
                else currentNode = currentNode.RightSub;
            }
        }

        if (!(depth <= Math.Floor(Math.Log(StartPoint.Size, 1.0d/Balance)))) {
            var scapegoat = FindScapeGoat(StartPoint, path, 0);
            var parent = FindParent(StartPoint, scapegoat, path, 0);
            var rebalanced = Rebalance(scapegoat.ToList(), 0, scapegoat.Size-1);
            
            if (parent is null) StartPoint = rebalanced.resultRoot;
            else if (parent.LeftSub == scapegoat) parent.LeftSub = rebalanced.resultRoot;
            else parent.RightSub = rebalanced.resultRoot;
        }
    }

    private TreeNode<K,V> FindScapeGoat(TreeNode<K,V> from, List<bool> path, int index)
    {
        if (index >= path.Count) return from;
        var currentNode = (path[index]) ? from.RightSub : from.LeftSub ;
        if (currentNode is null) return from;

        var sizeLeft = (currentNode.LeftSub is null) ? 0 : currentNode.LeftSub.Size ;
        var sizeRight = (currentNode.RightSub is null) ? 0 : currentNode.RightSub.Size ;
        if ((sizeLeft > Balance * currentNode.Size || sizeRight > Balance * currentNode.Size)) {
            return FindScapeGoat(currentNode, path, index+1);
        }
        else return from;
    }

    private TreeNode<K,V>? FindParent(TreeNode<K,V> from, TreeNode<K,V> child, List<bool> path, int index)
    {
        if (from == child) return null;
        if (from.LeftSub == child || from.RightSub == child) return from;
        else {
            if (index >= path.Count) throw new Exception("Could not find parent");
            var nextFrom = (path[index]) ? from.RightSub : from.LeftSub ;
            if (nextFrom is null) throw new Exception("Could not find parent");
            return FindParent(nextFrom, child, path, index+1);
        }
    }

    private (int resultSize, TreeNode<K,V>? resultRoot) Rebalance(List<TreeNode<K,V>> nodes, int left, int right)
    {
        if (right < left) return (0, null);
        var index = ((right-left)/2)+left;
        var selected = nodes[index];

        var (leftsubSize, leftsub) = Rebalance(nodes, left, index-1);
        var (rightsubSize, rightsub) = Rebalance(nodes, index+1, right);
        var size = leftsubSize + rightsubSize + 1;

        selected.Size = size;
        selected.LeftSub = leftsub;
        selected.RightSub = rightsub;

        return (leftsubSize + rightsubSize + 1, selected);
    }

    public List<TreeNode<K,V>> ToList()
    {
        if (StartPoint is null) return new List<TreeNode<K,V>>();
        return StartPoint.ToList();
    }

    public int Size
    {
        get {
            if (StartPoint is null) return 0;
            return StartPoint.Size;
        }
    }

    private TreeNode<K,V>? Find(K key)
    {
        if (StartPoint is null) return null;

        var currentNode = StartPoint;
        while (true) {
            if (currentNode.Key.CompareTo(key) == 0) return currentNode;
            else if (currentNode.Key.CompareTo(key) > 0) {
                if (currentNode.LeftSub is null) return null;
                currentNode = currentNode.LeftSub;
            }
            else {
                if (currentNode.RightSub is null) return null;
                currentNode = currentNode.RightSub;
            }
        }
    }

    public bool Contains(K key)
    {
        return Find(key) is not null;
    }

    public V? Lookup(K key)
    {
        var result = Find(key);
        if (result is null) return default(V);
        return result.Value;
    }
}