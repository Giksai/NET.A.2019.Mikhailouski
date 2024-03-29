using System;
using NUnit.Framework;
using SixthDayLib;

public class SortingTests
{
        [Test]
        public void NullInArraySortingTests()
        {
            //Arrange
            int[][] array = {
                new []{2,555,643,3 },
                null,
                new []{91293,12,55 },
                new []{1,2,3}
            };
            //Act
            try
            {
                array.SortRowsByMaxElement();
            }
            catch (ArgumentNullException e)
            {
                //Assert
                Assert.Pass();
            }
            catch
            {
                Assert.Fail();
            }
        Assert.Fail();
    }

    [Test]
    public void NullSortingTest()
    {
        int[][] nullVar = null;
        try
        {
            nullVar.SortRowsByMaxElement();
        }
        catch(ArgumentNullException e)
        {
            Assert.Pass();
        }
        catch
        {
            Assert.Fail();
        }
        Assert.Fail();
    }

    [Test]
    public void SortingTest()
    {
        //Arrange
        int[][] unsortedArray =
        {
           new []{234,424,5555 },
           new []{1,2,3},
           new []{0},
           new []{111,111,111}
        };
        int[][] sortedArray =
        {
            new []{0},
            new []{1,2,3},
            new []{111,111,111},
            new []{234, 424, 5555}
        };
        //Act
        unsortedArray.SortRowsByMinElement();
        //Assert
        Assert.AreEqual(unsortedArray, sortedArray);
    }
}
public class PolinomialTests
{
    [Test]
    public void PolynomialTest()
    {
        //Arrange
        Polynomial first = new Polynomial(new double[] { 14, 12, 30, 49, 5 });
        Polynomial second = new Polynomial(new double[] { 1, 2, 2, 1 });
        Polynomial expectedResult = new Polynomial(new double[] {15, 14 });
        //Act
        Polynomial result = first + second;
        //Assert
        Assert.True(result.Equals(expectedResult));
    }

    [Test]
    public void PolynomialNullTest1()
    {
        //Arrange
        Polynomial nullPolynom = null;
        Polynomial polynomial = new Polynomial(new double[] { 1, 2, 3 });
        //Act
        try
        {
            polynomial.Equals(nullPolynom);
        }
        catch(ArgumentNullException e)
        {
            Assert.Pass();
        }
        catch
        {
            Assert.Fail();
        }
        Assert.Fail();
    }

    [Test]
    public void PolynomialNullTest2()
    {
        //Arrange
        Polynomial nullPolynom = null;
        Polynomial polynomial = new Polynomial(new double[] { 1, 2, 3 });
        //Act
        try
        {
            nullPolynom.Equals(polynomial);
        }
        catch
        {
            Assert.Pass();
        }
        Assert.Fail();
    }

}
