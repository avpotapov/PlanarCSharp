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
    public sealed class Libraries : Dictionary<Library.TypeLibrary, Library>
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
            Add(library.Type, library);
        }

        public void Serializere(string LibraryPath = "")
        {
            XmlSerializer librarySerializer;
            StreamWriter libraryWriter;
            String FullFileName = "";

            if (LibraryPath == "")
            {
                LibraryPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath);
                LibraryPath = Path.Combine(LibraryPath, "library");
                if (!Directory.Exists(LibraryPath))
                    Directory.CreateDirectory(LibraryPath);

            }

            foreach (var library in Values)
            {
                librarySerializer = new XmlSerializer(typeof(Library));

                switch (library.Type)
                {
                    case Library.TypeLibrary.Vendor:
                        LibraryPath = Path.Combine(LibraryPath, "vendor");
                        if (!Directory.Exists(LibraryPath))
                            Directory.CreateDirectory(LibraryPath);
                        FullFileName = Path.Combine(LibraryPath, "list.xml");
                        break;

                    case Library.TypeLibrary.Custom:
                        LibraryPath = Path.Combine(LibraryPath, "custom");
                        if (!Directory.Exists(LibraryPath))
                            Directory.CreateDirectory(LibraryPath);
                        FullFileName = Path.Combine(LibraryPath, "list.xml");
                        break;
                }

                using (libraryWriter = new StreamWriter(FullFileName))
                    librarySerializer.Serialize(libraryWriter, library);
            }
        }

        public void Deserialize(string LibraryPath = "")
        {
            if (LibraryPath == "")
            {
                LibraryPath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase).LocalPath);
                LibraryPath = Path.Combine(LibraryPath, "library");
            }
            AddLibrary(Path.Combine(LibraryPath, "vendor", "list.xml"));
            AddLibrary(Path.Combine(LibraryPath, "custom", "list.xml"));
        }

        private void AddLibrary(string FileName)
        {
            if (File.Exists(FileName))
            {
                XmlSerializer deserializer = new XmlSerializer(typeof(Library));
                using (TextReader reader = new StreamReader(FileName))
                {
                    object obj = deserializer.Deserialize(reader);
                    Add((Library)obj);
                }
            }
        }
    }


    [XmlRoot("library")]
    public class Library
    {
        public enum TypeLibrary
        {
            [XmlEnum(Name = "vendor")]
            Vendor,
            [XmlEnum(Name = "custom")]
            Custom
        }
        [XmlAttribute("type")]
        public TypeLibrary Type { get; set; }

        [XmlArray("modules")]
        [XmlArrayItem(ElementName = "module")]
        public List<Module> ModuleList { get; set; }

        [XmlIgnore]
        public Dictionary<int, Module> ModuleDict
        {
            get { return ModuleList.ToDictionary(x => x.Uid, x => x); }
            set { ModuleList = value.Values.ToList(); }

        }
        public Library() { }
        public Library(TypeLibrary typeLibrary) { Type = typeLibrary; }
    }


    public class Module
    {

        [XmlAttribute("uid")]
        public int Uid { get; set; }

        public enum TypeSignature
        {
            [XmlEnum(Name = "none")]
            None,
            [XmlEnum(Name = "auto")]
            Auto,
            [XmlEnum(Name = "rccu")]
            Rccu
        }
        [XmlAttribute("type_signature")]
        public TypeSignature Signature { get; set; }

        public enum TypeBootloader
        {
            [XmlEnum(Name = "bl1")]
            Bl1,
            [XmlEnum(Name = "bl2")]
            Bl2,
            [XmlEnum(Name = "bl3")]
            Bl3
        }
        [XmlAttribute("type_bootloader")]
        public TypeBootloader Bootloader { get; set; }

        [XmlText]
        public string Value { get; set; }
    }

}
