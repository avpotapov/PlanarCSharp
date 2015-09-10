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

    public enum TypeSignature
    {
        [XmlEnum(Name = "none")]
        None,
        [XmlEnum(Name = "auto")]
        Auto,
        [XmlEnum(Name = "rccu")]
        Rccu
    }

    public enum TypeBootloader
    {
        [XmlEnum(Name = "bl1")]
        Bl1,
        [XmlEnum(Name = "bl2")]
        Bl2,
        [XmlEnum(Name = "bl3")]
        Bl3
    }

    /// <summary>
    /// Определение модуля библиотеки.
    /// Элемент коллекции Library.
    /// </summary>
    public class ModuleDefine
    {
        public ModuleDefine() { }
        public ModuleDefine(int uid)
        {
            Uid = uid;
        }

        [XmlAttribute("uid")]
        public int Uid { get; set; }

        [XmlAttribute("type_signature")]
        public TypeSignature TypeSignature { get; set; }

        [XmlAttribute("type_bootloader")]
        public TypeBootloader TypeBootloader { get; set; }

        [XmlText]
        public string Name { get; set; }

        [XmlIgnore]
        public Module Module { get; set; }
    }
}