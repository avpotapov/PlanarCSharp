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

    public enum TypeLibrary
    {
        [XmlEnum(Name = "vendor")]
        Vendor,
        [XmlEnum(Name = "custom")]
        Custom
    }

    /// <summary>
    /// Библиотека (производителя или разработчика).
    /// Элемент коллекции Libraries.
    /// Является коллекцией модулей.
    /// </summary>
    [XmlRoot("library")]
    public class Library
    {
        public Library() { }

        public Library(TypeLibrary typeLibrary)
        {
            TypeLibrary = typeLibrary;
        }
        [XmlAttribute("type")]
        public TypeLibrary TypeLibrary { get; set; }

        [XmlArray("modules")]
        [XmlArrayItem(ElementName = "module")]
        private List<ModuleDefine> moduleList;

        public List<ModuleDefine> ModuleList
        {
            get
            {
                if (moduleList == null)
                    moduleList = new List<ModuleDefine>();

                return moduleList;
            }
            private set
            {
                value = moduleList;
            }
        }
    }
}
