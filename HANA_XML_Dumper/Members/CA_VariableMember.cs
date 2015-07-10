using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HANA_XML_Dumper.Members
{
    public class CA_VariableMember : MemberBase
    {
        public string Type;
        public string Id;
        public string ParameterType;
        public string Description;
        public string DataType;
        public string DefaultValue;
        public string Length;
        public string Scale;
        public string Mandatory;
        public string MultiLine;
        public string SelectionType;
        public string HelperViewTable;
        public string HelperColumn;
        public string Filters;
        public string HelperMappings;
        public string StaticList;
    }
}
