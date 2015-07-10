using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using HANA_XML_Dumper.Members;

namespace HANA_XML_Dumper.Dumpers
{
    public class AN_VariableDumper : IDumper
    {
        private XmlReader xmlReader;
        private XmlDocument xmlDocument;
        private List<IMember> viewMembers;

        public string Description
        {
            get { return "Variables and Input Paramaters:\n"; }
            set { }
        }

        public Type MemberType
        {
            get
            {
                return typeof(AN_VariableMember);
            }
        }

        public AN_VariableDumper(XmlReader xmlReader)
        {
            this.xmlReader = xmlReader;
            this.xmlDocument = new XmlDocument();
            viewMembers = new List<IMember>();
        }

        public List<IMember> DumpHANAXML(string viewName)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlReader);
            var localVariables = xmlDocument["Cube:cube"]["localVariables"];
            foreach (XmlElement xmlElement in localVariables)
            {
                AN_VariableMember member = new AN_VariableMember();
                member.ViewName = viewName;
                if (xmlElement.HasAttribute("id"))
                    member.Id = xmlElement.Attributes["id"].Value.ToString();
                if (xmlElement.HasAttribute("parameter"))
                    member.Type = (xmlElement.Attributes["parameter"].Value.ToString() == "true") ? "IP" : "Var";
                else
                    member.Type = "Var";

                if (member.Type == "IP")
                    member.ParameterType = "Direct";


                var descr = xmlElement["descriptions"];
                if (descr != null && descr.HasAttribute("defaultDescription"))
                    member.Description = descr.Attributes["defaultDescription"].Value;

                var variableProperties = xmlElement["variableProperties"];
                if (variableProperties != null)
                {
                    if (variableProperties.HasAttribute("datatype"))
                        member.DataType = variableProperties.Attributes["datatype"].Value.ToString();
                    if (variableProperties.HasAttribute("defaultValue"))
                        member.DefaultValue = variableProperties.Attributes["defaultValue"].Value.ToString();
                    if (variableProperties.HasAttribute("length"))
                        member.Length = variableProperties.Attributes["length"].Value.ToString();
                    if (variableProperties.HasAttribute("mandatory"))
                        member.Mandatory = variableProperties.Attributes["mandatory"].Value.ToString();
                    if (variableProperties.HasAttribute("scale"))
                        member.Scale = variableProperties.Attributes["scale"].Value.ToString();

                    var selection = variableProperties["selection"];
                    if (selection != null)
                    {
                        if (selection.HasAttribute("multiLine"))
                            member.MultiLine = selection.Attributes["multiLine"].Value.ToString();
                        if (selection.HasAttribute("type"))
                            member.SelectionType = selection.Attributes["type"].Value.ToString();

                    }
                    string staticList = "";
                    var valueDomain = variableProperties["valueDomain"];
                    if (valueDomain != null)
                    {
                        foreach (XmlElement valueXmlElement in valueDomain)
                        {
                            if (valueXmlElement.Name == "attribute")
                            {
                                if (member.Type == "IP")
                                    member.ParameterType = "Column";
                                member.HelperViewTable = "Current View";
                                if (valueXmlElement.HasAttribute("name"))
                                    member.HelperColumn = valueXmlElement.Attributes["name"].Value.ToString();
                            }

                            if (valueXmlElement.Name == "externalLikeStructureName")
                            {
                                if (member.Type == "IP")
                                    member.ParameterType = "Column";

                                member.HelperViewTable = valueXmlElement.InnerText;
                            }

                            if (valueXmlElement.Name == "externalLikeElementName")
                                member.HelperColumn = valueXmlElement.InnerText;

                            //TODO: Support multiple mappings
                            if (valueXmlElement.Name == "variableMapping")
                            {
                                string mapping = "";
                                XmlElement targetVariable = valueXmlElement["targetVariable"];
                                if (targetVariable != null)
                                {
                                    mapping = (targetVariable.HasAttribute("name")) ? targetVariable.Attributes["name"].Value.ToString() : "";
                                }
                                XmlElement localVariable = valueXmlElement["localVariable"];
                                if (localVariable != null)
                                {
                                    mapping = mapping + " = " + localVariable.InnerText;
                                }
                                member.HelperMappings = mapping.Replace("#", "");
                            }

                            if (valueXmlElement.Name == "listEntry")
                            {
                                if (member.Type == "IP")
                                    member.ParameterType = "Static List";
                                string staticListId = (valueXmlElement.HasAttribute("id")) ? valueXmlElement.Attributes["id"].Value.ToString() : "";
                                string staticListDescr = "";
                                XmlElement descriptions = valueXmlElement["descriptions"];
                                if (descriptions != null)
                                {
                                    staticListDescr = (descriptions.HasAttribute("defaultDescription")) ? descriptions.Attributes["defaultDescription"].Value.ToString() : "";
                                }

                                staticList = string.Format("{0}{1} : {2} | ", staticList, staticListId, staticListDescr);
                            }
                        }
                    }

                    member.StaticList = staticList;

                    var derivationRule = variableProperties["derivationRule"];
                    if (derivationRule != null)
                    {
                        member.ParameterType = "Derived";
                        string Filters = "";
                        foreach (XmlElement derivedXmlElement in derivationRule)
                        {

                            if (derivedXmlElement.Name == "resultColumn")
                            {
                                string Helper = (derivedXmlElement.HasAttribute("schemaName")) ? derivedXmlElement.Attributes["schemaName"].Value.ToString() + "." : "";
                                string ViewTable = (derivedXmlElement.HasAttribute("columnObjectName")) ? derivedXmlElement.Attributes["columnObjectName"].Value.ToString() : "";
                                member.HelperViewTable = Helper + ViewTable;
                                member.HelperColumn = (derivedXmlElement.HasAttribute("columnName")) ? derivedXmlElement.Attributes["columnName"].Value.ToString() : "";
                            }

                            if (derivedXmlElement.Name == "columnFilter")
                            {
                                string columnName = (derivedXmlElement.HasAttribute("columnName")) ? derivedXmlElement.Attributes["columnName"].Value.ToString() : "";
                                string strValueFilter = "";
                                XmlElement valueFilter = derivedXmlElement["valueFilter"];
                                if (valueFilter != null)
                                    strValueFilter = (valueFilter.HasAttribute("value")) ? valueFilter.Attributes["value"].Value.ToString() : "";
                                Filters = string.Format("{0}{1} = {2} | ", Filters, columnName, strValueFilter);
                            }
                        }
                        member.Filters = Filters;

                    }



                }

                viewMembers.Add(member);
            }
            // var stringBuilder = new StringBuilder(viewMembers.Count);
            // stringBuilder.AppendLine(new VariableMember().ToString(Enums.MemberToStringOption.Header));
            // viewMembers.ForEach(x => stringBuilder.AppendLine(x.ToString()));
            return viewMembers;
            //stringBuilder.ToString();

        }
    }
}