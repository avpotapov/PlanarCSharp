using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Planar.Library
{

    public enum TypeRegister
    {
        [XmlEnum(Name = "HOLDING")]
        Holding,

        [XmlEnum(Name = "INPUT")]
        Input
    }

    [XmlType(TypeName ="var_define")]
    public class VarDefine
    {
        public VarDefine() { }
        public VarDefine(TypeRegister typeRegister)
        {
            TypeRegister = typeRegister;
        }

        [XmlAttribute("type")]
        public TypeRegister TypeRegister { get; set; }

        [XmlElement("name")]
        [XmlText]
        public string Name { get; set; }

        public string Uid { get; set; }

    }

}
