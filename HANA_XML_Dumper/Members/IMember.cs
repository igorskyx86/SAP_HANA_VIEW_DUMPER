using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HANA_XML_Dumper.Enums;

namespace HANA_XML_Dumper.Members
{
    public interface IMember
    {
        string ViewName { get; set; }
        string[] ToArray(MemberToStringOption memberToStringOption);
        string ToString(MemberToStringOption memberToStringOption, string quote = @"""", string delimiter = @",");
    }
}
