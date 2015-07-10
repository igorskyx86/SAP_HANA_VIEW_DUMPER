using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HANA_XML_Dumper.Enums;
using HANA_XML_Dumper.Members;


namespace HANA_XML_Dumper.Dumpers
{
    public class CA_SemanticsDumper : IDumper
    {
        private XmlReader xmlReader;
        private XmlDocument xmlDocument;
        private List<IMember> viewMembers;
        public string viewName;

        public Type MemberType
        {
            get
            {
                return typeof(CA_SemanticsMember);
            }
        }

        public string Description
        {
            get { return "Semantics:\n"; }
            set { }
        }

        public CA_SemanticsDumper(XmlReader xmlReader)
        {
            this.xmlReader = xmlReader;
            this.xmlDocument = new XmlDocument();
            viewMembers = new List<IMember>();
        }

        private void ProcessNode(XmlElement xmlParent, Enums.MemberType memberType)
        {
            foreach (XmlElement xmlElement in xmlParent)
            {
                CA_SemanticsMember member = new CA_SemanticsMember();
                member.ViewName = viewName;
                member.Id = xmlElement.Attributes["id"].Value;
                if (xmlElement.HasAttribute("order"))
                    member.Order = int.Parse(xmlElement.Attributes["order"].Value);
                member.Type = memberType.ToString();
                if (xmlElement.HasAttribute("hidden"))
                {
                    member.Hidden = xmlElement.Attributes["hidden"].Value;
                }
                else
                {
                    member.Hidden = "";
                }
                member.Hidden = member.Hidden.ToUpper().Replace("FALSE", "");
                var descr = xmlElement["descriptions"];
                if (descr != null && descr.HasAttribute("defaultDescription"))
                    member.Description = descr.Attributes["defaultDescription"].Value;

                if (xmlElement.HasAttribute("aggregationType"))
                    member.Aggregation = xmlElement.Attributes["aggregationType"].Value;

                if (xmlElement.HasAttribute("descriptionColumnName"))
                    member.LabelColumn = xmlElement.Attributes["descriptionColumnName"].Value;

                var localVariable = xmlElement["localVariable"];
                if (localVariable != null && localVariable.InnerText != null)
                    member.LocalVariable = localVariable.InnerText.Replace("&quot", "").Replace(@"""", "").Replace(@"#", "");


                var measureMapping = xmlElement["measureMapping"];
                if (measureMapping != null && measureMapping.HasAttribute("columnName"))
                    member.ColumnFormula = measureMapping.Attributes["columnName"].Value;

                var keyMapping = xmlElement["keyMapping"];
                if (keyMapping != null && keyMapping.HasAttribute("columnName"))
                    member.ColumnFormula = keyMapping.Attributes["columnName"].Value;


                var formula = xmlElement["formula"];
                if (formula != null && formula.InnerText != null)
                    member.ColumnFormula = formula.InnerText.Replace("&quot", "").Replace(@"""", "");
                viewMembers.Add(member);
            }
        }

        public List<IMember> DumpHANAXML(string viewName)
        {
            this.viewName = viewName;
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);
            var logicalModel = xmlDocument["Calculation:scenario"]["logicalModel"];
            var attributes = logicalModel["attributes"];
            if (attributes != null)
                ProcessNode(attributes, Enums.MemberType.Attribute);
            var baseMeasures = logicalModel["baseMeasures"];
            if (baseMeasures != null)
                ProcessNode(baseMeasures, Enums.MemberType.Measure);
            var calculatedMeasures = logicalModel["calculatedMeasures"];
            if (calculatedMeasures != null)
                ProcessNode(calculatedMeasures, Enums.MemberType.Calculated);
            var stringBuilder = new StringBuilder(viewMembers.Count);
            //stringBuilder.AppendLine(new SemanticsMember().ToString(Enums.MemberToStringOption.Header));
            //viewMembers.OrderBy(x => x.Order).ToList().ForEach(x => stringBuilder.AppendLine(x.ToString()));
            //return stringBuilder.ToString();
            return viewMembers.OrderBy(x => (x as CA_SemanticsMember).Order).ToList();
        }


    }
}
