using Xunit;
using OCollection;
using System;

namespace test;

public class LinkedOListTests
{
    [Theory]
    [InlineData(3, 2)]
    [InlineData(3, 3)]
    [InlineData(3, 4)]
    [InlineData(3, -44)]
    [InlineData(100, 72)]
    [InlineData(100, 7)]
    [InlineData(100, -712)]
    public void RotatesCorrectAmount(int size, int rotation)
    {
        var ol = new LinkedOList<int>();

        for(int i = 0; i < size; i++) ol.InsertBack(i);

        ol.Rotate(rotation);

        if (rotation >= 0) Assert.Equal(rotation % size, ol[0]);
        else Assert.Equal(size - (Math.Abs(rotation) % size), ol[0]);
    }
}