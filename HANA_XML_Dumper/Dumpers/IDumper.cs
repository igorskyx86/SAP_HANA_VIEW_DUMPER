using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HANA_XML_Dumper.Members;

namespace HANA_XML_Dumper.Dumpers
{
    interface IDumper
    {
        List<IMember> DumpHANAXML(string viewName);
        Type MemberType { get; }
        string Description { get; set; }
    }
}
