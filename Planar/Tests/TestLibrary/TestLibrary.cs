using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Planar.Library;

namespace TestLibrary
{
    [TestClass]
    public class TestLibrary
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
        public void TestChangeLibraries()
        {
            Assert.IsNotNull(libraries);
            Assert.AreEqual(libraries.Count, 0);

            // Добавить в коллекцию библиотек новую с указанием типа библиотеки (vendor, custom)
            libraries.Add(new Library(TypeLibrary.Custom));
            libraries.Add(new Library(TypeLibrary.Vendor));
            Assert.AreEqual(libraries.Count, 2);

            Library library;
            Assert.IsTrue(libraries.TryGetValue(TypeLibrary.Custom, out library));
            Assert.IsNotNull(library);
            library = null;

            Assert.IsTrue(libraries.TryGetValue(TypeLibrary.Vendor, out library));
            Assert.IsNotNull(library);
            library = null;

            libraries.Remove(TypeLibrary.Custom);
            Assert.AreEqual(libraries.Count, 1);

            Assert.IsFalse(libraries.TryGetValue(TypeLibrary.Custom, out library));
            Assert.IsNull(library);

            libraries.Clear();
            Assert.AreEqual(libraries.Count, 0);
        }


        private bool AddUniqueModule(Library library, int uid)
        {
            ModuleDefine module = new ModuleDefine(uid);
            if (library.ModuleList.Find(x => x.Uid == module.Uid) == null)
            {
                library.ModuleList.Add(module);
                return true;
            }
            return false;
        }

        [TestMethod]
        public void TestSerialize()
        {
            Library library;

            Assert.IsNotNull(libraries);
            Assert.AreEqual(libraries.Count, 0);

            // Добавить в коллекцию библиотек новую с указанием типа библиотеки (vendor, custom)
            libraries.Add(new Library(TypeLibrary.Custom));
            libraries.Add(new Library(TypeLibrary.Vendor));

            Assert.IsTrue(libraries.TryGetValue(TypeLibrary.Vendor, out library));

            Assert.IsTrue(AddUniqueModule(library, 10));
            Assert.IsFalse(AddUniqueModule(library, 10));
            Assert.IsTrue(AddUniqueModule(library, 20));
            Assert.IsTrue(AddUniqueModule(library, 30));
            Assert.IsFalse(AddUniqueModule(library, 30));
            Assert.IsTrue(AddUniqueModule(library, 40));

            libraries.Serializere();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestModuleSerialize()
        {
            Library library;
            Assert.IsNotNull(libraries);
            libraries.Clear();
            Assert.AreEqual(libraries.Count, 0);

            // Добавить в коллекцию библиотек новую с указанием типа библиотеки (vendor, custom)
            libraries.Add(new Library(TypeLibrary.Custom));
            libraries.Add(new Library(TypeLibrary.Vendor));

            Assert.IsTrue(libraries.TryGetValue(TypeLibrary.Vendor, out library));

            // Список модулей
            Assert.IsTrue(AddUniqueModule(library, 10));
            Assert.IsFalse(AddUniqueModule(library, 10));
            Assert.IsTrue(AddUniqueModule(library, 20));
            Assert.IsTrue(AddUniqueModule(library, 30));
            Assert.IsFalse(AddUniqueModule(library, 30));
            Assert.IsTrue(AddUniqueModule(library, 40));

            // Получить модуль по uid
            ModuleDefine m = library.ModuleList.Find(x => x.Uid == 10);
            Assert.IsNotNull(m);

            // Редактировать
            m.Name = "Test1";
            m.Module = new Module();
            m.Module.Description.Add("Строка1");
            m.Module.Description.Add("Строка2");
            m.Module.Description.Add("Строка3");

            // Получить модуль по uid
            m = library.ModuleList.Find(x => x.Uid == 20);
            Assert.IsNotNull(m);

            // Редактировать
            m.Name = "Test2";
            m.Module = new Module();
            m.Module.Description.Add("Строка4");
            m.Module.Description.Add("Строка6");
            m.Module.Description.Add("Строка8");
            m.Module.Registers.Add(new List<VarDefine>());
            m.Module.Registers.Add(new List<VarDefine>());
            m.Module.Registers[0].Add(new VarDefine(TypeRegister.Holding));
            m.Module.Registers[0].Add(new VarDefine(TypeRegister.Holding));
            m.Module.Registers[1].Add(new VarDefine(TypeRegister.Input));
            // Первый набор VarSet
            m.Module.Registers[0][0].Name = "VarDefine1";

            m.Module.Registers[0][1].Name = "VarDefine2";
            m.Module.Registers[1][0].Name = "VarDefine3";

            // Сериализация
            libraries.Serializere();
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void TestDeserialize()
        {

            Assert.IsNotNull(libraries);
            libraries.Clear();
            Assert.AreEqual(libraries.Count, 0);

            // Добавить в коллекцию библиотек новую с указанием типа библиотеки (vendor, custom)
            libraries.Add(new Library(TypeLibrary.Custom));
            libraries.Add(new Library(TypeLibrary.Vendor));

            Library library;
            Assert.IsTrue(libraries.TryGetValue(TypeLibrary.Vendor, out library));

            Assert.IsNotNull(library.ModuleList);
            Assert.AreEqual(library.ModuleList.Count, 0);

            Assert.IsTrue(AddUniqueModule(library, 10));
            Assert.IsFalse(AddUniqueModule(library, 10));
            Assert.IsTrue(AddUniqueModule(library, 20));
            Assert.IsTrue(AddUniqueModule(library, 30));
            Assert.IsFalse(AddUniqueModule(library, 30));
            Assert.IsTrue(AddUniqueModule(library, 40));

            Assert.AreEqual(library.ModuleList.Count, 4);

            libraries.Serializere();
            Assert.IsTrue(true);

            libraries.Clear();
            libraries.Deserialize();
            Assert.AreEqual(libraries.Count, 2);
            Assert.IsTrue(libraries.TryGetValue(TypeLibrary.Vendor, out library));
            Assert.AreEqual(library.ModuleList.Count, 4);
        }



    }
}
