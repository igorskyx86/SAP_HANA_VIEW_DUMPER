using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HANA_XML_Dumper.Members
{
    public class AN_SemanticsMember : MemberBase
    {
        // public string ViewName;
        public int Order;
        public string Type;
        public string Id;
        public string Description;
        public string Aggregation;
        public string LocalVariable;
        public string LabelColumn;
        public string Hidden;
        public string MappingSchema;
        public string MappingObject;
        public string ColumnFormula;
    }
}
