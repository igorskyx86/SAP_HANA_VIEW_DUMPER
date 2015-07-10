using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HANA_XML_Dumper.Members;

namespace HANA_XML_Dumper.Renderers
{
    public static class CSVRenderer
    {
        public static string Render(List<IMember> viewMembers)
        {
            StringBuilder stringBuilder = new StringBuilder(viewMembers.Count);
            if(viewMembers.Count>0)
            {
                var member = viewMembers[0];
                stringBuilder.AppendLine(member.ToString(Enums.MemberToStringOption.Header));
                viewMembers.ForEach(x => stringBuilder.AppendLine(x.ToString()));
            }
            return stringBuilder.ToString();
        }
    }
}
