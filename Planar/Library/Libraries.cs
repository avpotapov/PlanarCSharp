using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection;


namespace Planar.Library
{
    /// <summary>
    /// Коллекция библиотек
    /// </summary>
    public sealed class Libraries : Dictionary<TypeLibrary, Library>
    {
        private Libraries() { }

        static private Libraries libraries;

        public static Libraries Instance
        {
            get
            {
                if (libraries == null)
                    libraries = new Libraries();
                return libraries;
            }
        }

        public void Add(Library library)
        {
            Add(library.TypeLibrary, library);
        }

        private string GetDefaultDirectory()
        {
            string libraryPath = "";

            libraryPath = Path.GetDirectoryName(new Uri(AppDomain.CurrentDomain.BaseDirectory).LocalPath);
            libraryPath = Path.Combine(libraryPath, "library");
            if (!Directory.Exists(libraryPath))
                 Directory.CreateDirectory(libraryPath);

            return libraryPath;
        }

        private string GetFileName(TypeLibrary typeLibrary, string filePath)
        {
            string fileName = "";
            switch (typeLibrary)
            {
                case TypeLibrary.Vendor:
                    fileName = Path.Combine(filePath, "vendor", "list.xml");
                    break;

                case TypeLibrary.Custom:
                    fileName = Path.Combine(filePath, "custom", "list.xml");
                    break;
            }
            if (!Directory.Exists(Path.GetDirectoryName(fileName)))
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));

            return fileName;
        }

        public void Serializere(string libraryPath = "")
        {
            XmlSerializer librarySerializer;
            StreamWriter libraryWriter;
            String fileName = "";

            if (libraryPath == "")
                libraryPath = GetDefaultDirectory();


            foreach (var library in Values)
            {
                librarySerializer = new XmlSerializer(typeof(Library));

                fileName = GetFileName(library.TypeLibrary, libraryPath);
            
                using (libraryWriter = new StreamWriter(fileName))
                    librarySerializer.Serialize(libraryWriter, library);
            }
        }

        public void Deserialize(string libraryPath = "")
        {
            String fileName = "";

            if (libraryPath == "")
                libraryPath = GetDefaultDirectory();

            fileName = GetFileName(TypeLibrary.Vendor, libraryPath);
            AddLibrary(fileName);

            fileName = GetFileName(TypeLibrary.Custom, libraryPath);
            AddLibrary(fileName);
        }

        private void AddLibrary(string fileName)
        {
            if (File.Exists(fileName))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Library));
                using (TextReader reader = new StreamReader(fileName))
                {
                    object obj = deserializer.Deserialize(reader);
                    Add((Library)obj);
                }
            }
        }
    }
}