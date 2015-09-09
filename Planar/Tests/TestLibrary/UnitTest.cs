using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Planar.Library;

namespace TestLibrary
{
    [TestClass]
    public class UnitTest
    {
        Libraries libraries;

        [TestInitialize]
        public void Initialize()
        {
            libraries = Libraries.Instance;
        }

        [TestMethod]
        public void TestInitializeLibraries()
        {
            Assert.AreNotEqual(libraries, null);
        }

        [TestMethod]
        public void TestLibraries()
        {
            Assert.IsNotNull(libraries);
            Assert.AreEqual(libraries.Count, 0);

            // Добавить в коллекцию библиотек новую с указанием типа библиотеки (vendor, custom)
            libraries.Add(new Library(Library.TypeLibrary.Custom));
            libraries.Add(new Library(Library.TypeLibrary.Vendor));
            Assert.AreEqual(libraries.Count, 2);

            Library library;
            Assert.IsTrue(libraries.TryGetValue(Library.TypeLibrary.Custom, out library));
            Assert.IsNotNull(library);
            library = null;

            Assert.IsTrue(libraries.TryGetValue(Library.TypeLibrary.Vendor, out library));
            Assert.IsNotNull(library);
            library = null;

            libraries.Remove(Library.TypeLibrary.Custom);
            Assert.AreEqual(libraries.Count, 1);

            Assert.IsFalse(libraries.TryGetValue(Library.TypeLibrary.Custom, out library));
            Assert.IsNull(library);
        }

        [TestMethod]
        public void TestSerialize()
        {
            Assert.IsNotNull(libraries);
            Assert.AreEqual(libraries.Count, 0);

            // Добавить в коллекцию библиотек новую с указанием типа библиотеки (vendor, custom)
            libraries.Add(new Library(Library.TypeLibrary.Custom));
            libraries.Add(new Library(Library.TypeLibrary.Vendor));

            libraries.Serializere();
          //  Assert.IsTrue(true);
        }



    }
}
