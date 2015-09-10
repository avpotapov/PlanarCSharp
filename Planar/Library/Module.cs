using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace Planar.Library
{
    [XmlRoot("module")]
    public class Module
    {

        List<string> description;
        [XmlArray("description")]
        [XmlArrayItem("para")]
        public List<string> Description
        {
            get
            {
                if (description == null)
                    description = new List<string>();
                return description;
            }

            private set
            {
                value = description;
            }
        }

        private List<List<VarDefine>> registers;

        [XmlArray("registers")]
        [XmlArrayItem("var_set")]
        public List<List<VarDefine>> Registers
        {
            get
            {
                if (registers == null)
                    registers = new List<List<VarDefine>>();
                return registers;
            }
            private set
            {
                value = registers;
            }
        }

        public void Serialize(string fileName)
        {
            XmlSerializer ModuleSerializer = new XmlSerializer(typeof(Module));
            using (StreamWriter ModuleWriter = new StreamWriter(fileName))
                ModuleSerializer.Serialize(ModuleWriter, this);
        }

        public void Deserialize(string fileName)
        {

        }
    }
}
