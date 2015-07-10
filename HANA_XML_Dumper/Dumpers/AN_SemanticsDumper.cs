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
    public class AN_SemanticsDumper : IDumper
    {

        private XmlReader xmlReader;
        private XmlDocument xmlDocument;
        private List<IMember> viewMembers;
        public string viewName;

        public Type MemberType
        {
            get
            {
                return typeof(AN_SemanticsMember);
            }
        }

        public string Description
        {
            get { return "Semantics:\n"; }
            set { }
        }

        public AN_SemanticsDumper(XmlReader xmlReader)
        {
            this.xmlReader = xmlReader;
            this.xmlDocument = new XmlDocument();
            viewMembers = new List<IMember>();
        }

        private void ProcessMapping(XmlElement xmlElement, AN_SemanticsMember member)
        {
            if (xmlElement.HasAttribute("columnName"))
                member.ColumnFormula = xmlElement.Attributes["columnName"].Value;
            if (xmlElement.HasAttribute("schemaName"))
                member.MappingSchema = xmlElement.Attributes["schemaName"].Value;
            if (xmlElement.HasAttribute("columnObjectName"))
                member.MappingObject = xmlElement.Attributes["columnObjectName"].Value;
        }

        private void ProcessNode(XmlElement xmlParent, Enums.MemberType memberType)
        {
            foreach (XmlElement xmlElement in xmlParent)
            {
                AN_SemanticsMember member = new AN_SemanticsMember();
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
                if (measureMapping != null)
                    ProcessMapping(measureMapping, member);

                var keyMapping = xmlElement["keyMapping"];
                if (keyMapping != null)
                    ProcessMapping(keyMapping, member);

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
            var privateMeasureGroup = xmlDocument["Cube:cube"]["privateMeasureGroup"];

            var attributes = privateMeasureGroup["attributes"];
            if (attributes != null)
                ProcessNode(attributes, Enums.MemberType.Attribute);

            var calculatedAttributes = privateMeasureGroup["calculatedAttributes"];
            if (calculatedAttributes != null)
                ProcessNode(calculatedAttributes, Enums.MemberType.Calculated);

            var baseMeasures = privateMeasureGroup["baseMeasures"];
            if (baseMeasures != null)
                ProcessNode(baseMeasures, Enums.MemberType.Measure);

            //TODO = logical join attributes sets

            return viewMembers.OrderBy(x => (x as AN_SemanticsMember).Order).ToList();
        }

    }
}
