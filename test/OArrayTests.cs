using Xunit;
using OCollection;
using System;

namespace test;

public class OArrayTests
{
    [Fact]
    public void SizeOf0Fails()
    {
        Assert.Throws<ArgumentException>(() => {
            var oa = new OArray<int>(0);
        });
    }


    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    [InlineData(10)]
    [InlineData(100)]
    public void SizeIsAlwaysAsConstructed(int size)
    {
        var oa = new OArray<int>(size);

        Assert.Equal(size, oa.Size);

        oa[size] = 2;

        Assert.Equal(size, oa.Size);

        oa.Rotate(1);

        Assert.Equal(size, oa.Size);

        oa.Rotate(-2);

        Assert.Equal(size, oa.Size);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(4)]
    [InlineData(10)]
    [InlineData(100)]
    public void AccessOutOfBoundLoopsAround(int size)
    {
        var oa = new OArray<int>(size);
        oa[-1] = 1337;

        Assert.Equal(1337, oa[size+(size-1)]);
        Assert.Equal(1337, oa[-(size+1)]);
    }
}