using Moq;
using NUnit.Framework;
using System;
using TopoChallenge.Helpers;
using TopoChallenge.Interfaces;

namespace TopoChallenge.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Constructor_NullValues_ThrowsException()
        {
            var mockDbHelper = new Mock<IDBHelper>();

            Assert.Throws<ArgumentNullException>(() => new DBHelper(null));
        }
    }
}