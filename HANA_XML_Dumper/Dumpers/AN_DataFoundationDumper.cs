using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HANA_XML_Dumper.Members;

namespace HANA_XML_Dumper.Dumpers
{
    public class AN_DataFoundationDumper : IDumper
    {
        public string Description
        {
            get { return "Data foundation:\n"; }
            set { }
        }

        public Type MemberType
        {
            get
            {
                return typeof(AN_DataFoundationMember);
            }
        }

        public AN_DataFoundationDumper()
        {
            throw new NotImplementedException();
        }

        public List<IMember> DumpHANAXML(string viewName)
        {
            return null;
        }
    }
}
