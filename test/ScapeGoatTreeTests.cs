using Xunit;
using OCollection;

namespace test;

public class ScapeGoatTreeTests
{
    [Theory]
    [InlineData(100,51,1234)]
    [InlineData(1000,55,7154)]
    [InlineData(100,-2,1234)]
    [InlineData(100,-5,-56234)]
    [InlineData(100,613,-19357)]
    [InlineData(100,0,-19357)]
    public void InsertedPairExists(int fillAmount, int key, int value)
    {
        var tree = new ScapeGoatTree<int, int>(0.66);

        for(int i = 0; i < fillAmount; i++) tree.Insert(i,i*2);
        tree.Insert(key, value);

        Assert.True(tree.Contains(key));
        Assert.Equal(value, tree.Lookup(key));
    }
}