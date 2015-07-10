using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HANA_XML_Dumper.Members;

namespace HANA_XML_Dumper.Dumpers
{
    public class CA_ProjectionDumper : IDumper
    {
        private XmlReader xmlReader;
        private XmlDocument xmlDocument;
        private List<IMember> viewMembers;

        public Type MemberType
        {
            get
            {
                return typeof(CA_ProjectionMember);
            }
        }

        public string Description { get; set; }

        private void ProcessNode(XmlElement xmlParent, Enums.MemberType memberType, string calculationViewId, string calculationViewType)
        {
            foreach (XmlElement xmlElement in xmlParent)
            {
                CA_ProjectionMember member = new CA_ProjectionMember();
                member.BlockID = calculationViewId;
                member.BlockType = calculationViewType;
                member.Id = xmlElement.Attributes["id"].Value.ToString();
                member.Type = memberType.ToString();

                if (xmlElement.HasAttribute("datatype"))
                    member.DataType = xmlElement.Attributes["datatype"].Value;

                if (xmlElement.HasAttribute("length"))
                    member.DataType = xmlElement.Attributes["length"].Value;

                var formula = xmlElement["formula"];
                if (formula != null && formula.InnerText != null)
                    member.Formula = formula.InnerText.Replace("&quot", "").Replace(@"""", "");
                viewMembers.Add(member);
            }
        }


        public CA_ProjectionDumper(XmlReader xmlReader)
        {
            this.xmlReader = xmlReader;
            this.xmlDocument = new XmlDocument();
            viewMembers = new List<IMember>();
        }

        public List<IMember> DumpHANAXML(string viewName)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);
            var rootNode = xmlDocument["Calculation:scenario"];
            //TODO: Add projection mappings
            //TODO: Add global text consts
            string quote = @"""";
            string delimiter = ",";

            string viewID = rootNode.Attributes["id"].Value.ToString();
            string viewDesctiption = "";
            string applyPrivilegeType = rootNode.Attributes["applyPrivilegeType"].Value.ToString();

            var viewDesctiptions = rootNode["descriptions"];
            if (viewDesctiptions != null)
            {
                viewDesctiption = viewDesctiptions.Attributes["defaultDescription"].Value.ToString();
            }

            Description = string.Format(
                    "{0}Calculation view ID:{0}{1}{0}{2}{0}\n{0}Description:{0}{1}{0}{3}{0}\n{0}Analytic Privilege:{0}{1}{0}{4}{0}\n",
                    quote, delimiter, viewID, viewDesctiption, applyPrivilegeType);

            var calculationViews = xmlDocument["Calculation:scenario"]["calculationViews"];
            foreach (XmlElement calculationView in calculationViews)
            {
                string calculationViewId = calculationView.Attributes["id"].Value.ToString();
                string calculationViewType = calculationView.Attributes["xsi:type"].Value.ToString();
                ProcessNode(calculationView["viewAttributes"], Enums.MemberType.Attribute, calculationViewId, calculationViewType);
                ProcessNode(calculationView["calculatedViewAttributes"], Enums.MemberType.Calculated, calculationViewId, calculationViewType);

                var filterNode = calculationView["filter"];
                if (filterNode != null)
                {
                    CA_ProjectionMember filterMember = new CA_ProjectionMember();
                    filterMember.ViewName = viewName;
                    filterMember.BlockID = calculationViewId;
                    filterMember.BlockType = calculationViewType;
                    filterMember.Type = "Filter Expression";
                    filterMember.Formula = filterNode.InnerText;
                    viewMembers.Add(filterMember);
                }

            }
            //var stringBuilder = new StringBuilder(viewMembers.Count);
            //stringBuilder.AppendLine(new ProjectionMember().ToString(Enums.MemberToStringOption.Header));
            //viewMembers.ForEach(x => stringBuilder.AppendLine(x.ToString()));
            return viewMembers;
        }

    }
}
