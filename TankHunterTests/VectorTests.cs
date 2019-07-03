using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using TankHunterAiLenardArjen;

namespace TankHunterTests
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void TestToVector2Function()
        {
            //arange
            Vector vector = new Vector(5, 5);
            Vector2 vector2 = new Vector2(5, 5);

            Vector2 newVector2 = vector.ToVector2();

            //assert
            Assert.AreEqual(vector2, newVector2);
        }

        [TestMethod]
        public void TestNormalizeFunction()
        {
            //arange
            Vector vector = new Vector(5, 5);
            Vector2 vector2 = new Vector2(5, 5);

            //act
            vector.Normalize();
            vector2.Normalize();

            //assert
            Assert.AreEqual(vector2, vector.ToVector2());
        }

        [TestMethod]
        public void TestLengthSqFunction()
        {
            //arange
            Vector vector = new Vector(5, 5);
            Vector2 vector2 = new Vector2(5, 5);

            vector2.LengthSquared();
            vector.LengthSq();

            //assert
            Assert.AreEqual(vector2, vector.ToVector2());
        }

        [TestMethod]
        public void TestLengthFunction()
        {
            //arange
            Vector vector = new Vector(5, 5);
            Vector2 vector2 = new Vector2(5, 5);

            //assert
            Assert.AreEqual(vector2.Length(), vector.Length());
        }
        [TestMethod]
        public void TestDotFunction()
        {
            //arange
            Vector vector = new Vector(5, 5);
            Vector2 vector2 = new Vector2(5, 5);
            Vector vectorToDotWith = new Vector(15, 15);
            Vector2 vector2ToDotWith = new Vector2(15, 15);

            float dotVector = 0;
            float dotVector2 = 0;

            //assert
            dotVector = vector.Dot(vectorToDotWith);
            dotVector2 = Vector2.Dot(vector2, vector2ToDotWith);
            Assert.AreEqual(dotVector2, dotVector2);
        }

        [TestMethod]
        public void TestTruncateFunctionPositiveValue()
        {
            //arange
            Vector vector = new Vector(5, 5);
            Vector vectorExpected = new Vector(3, 3);
            float maxSpeed = 3;

            vector.Truncate(maxSpeed);

            //assert
            Assert.AreEqual(vectorExpected.X, vector.X) ;
            Assert.AreEqual(vectorExpected.Y, vector.Y);
        }

        [TestMethod]
        public void TestTruncateFunctionNegativeValue()
        {
            //arange
            Vector vector = new Vector(-5, -5);
            Vector vectorExpected = new Vector(-3, -3);
            float maxSpeed = 3;

            vector.Truncate(maxSpeed);

            //assert
            Assert.AreEqual(vectorExpected.X, vector.X);
            Assert.AreEqual(vectorExpected.Y, vector.Y);
        }

        [TestMethod]
        public void TestTruncateFunctionGoodValue()
        {
            //arange
            Vector vector = new Vector(-2, -1);
            Vector vectorExpected = new Vector(-2, -1);
            float maxSpeed = 3;

            vector.Truncate(maxSpeed);

            //assert
            Assert.AreEqual(vectorExpected.X, vector.X);
            Assert.AreEqual(vectorExpected.Y, vector.Y);
        }

        [TestMethod]
        public void TestDevideOverloadFunction()
        {
            //arange
            Vector vector = new Vector(10, 10);
            float vectorDevidedBy =5;
            Vector vectorExpected = new Vector(2, 2);

            //Act
            Vector vectorResult = vector / vectorDevidedBy;

            //assert
            Assert.AreEqual(vectorExpected.X, vector.X);
            Assert.AreEqual(vectorExpected.Y, vector.Y);
        }

        [TestMethod]
        public void TestDevideOverloadFunctionDivideByZero()
        {
            //arange
            Vector vector = new Vector(0, 0);
            float vectorDevidedBy = 0;
            Vector vectorExpected = new Vector(0, 0);

            //Act
            Vector vectorResult = vector / vectorDevidedBy;

            //assert
            Assert.AreEqual(vectorExpected.X, vector.X);
            Assert.AreEqual(vectorExpected.Y, vector.Y);
        }

        [TestMethod]
        public void TestDevideOverloadFunctionDivideDirectly()
        {
            //arange
            Vector vector = new Vector(0, 0);
            float vectorDevidedBy = 0;
            Vector vectorExpected = new Vector(0, 0);

            //Act
            vector /= vectorDevidedBy;

            //assert
            Assert.AreEqual(vectorExpected.X, vector.X);
            Assert.AreEqual(vectorExpected.Y, vector.Y);
        }

    }
}
