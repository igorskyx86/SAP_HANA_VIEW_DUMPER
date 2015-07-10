using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel.Design;
using HANA_XML_Dumper.Enums;

namespace HANA_XML_Dumper.Members
{
    abstract public class MemberBase : IMember
    {
        public string ViewName {get;set;}

        public string[] ToArray(MemberToStringOption memberToStringOption)
        {
            List<string> list = new List<string>();
            Type myType = this.GetType();
            FieldInfo[] myField = myType.GetFields();
            for (int i = 0; i < myField.Length; i++)
            {
                string value = string.Empty;
                if (memberToStringOption == MemberToStringOption.Body)
                {
                    var fieldValue = myField[i].GetValue(this);
                    list.Add((fieldValue!=null) ? fieldValue.ToString() : string.Empty);
                }
                else
                {
                    list.Add(myField[i].Name);
                }

            }
            return list.ToArray();
        }

        //TODO: refactor to use ToArray as base
        public string ToString(MemberToStringOption memberToStringOption, string quote = @"""", string delimiter = @",")
        {
            Type myType = this.GetType();
            FieldInfo[] myField = myType.GetFields();
            StringBuilder stringBuider = new StringBuilder(myField.Length);
            for (int i = 0; i < myField.Length; i++)
            {

                stringBuider.Append(quote);
                string value = string.Empty;
                if (memberToStringOption == MemberToStringOption.Body)
                {
                    var fieldValue = myField[i].GetValue(this);
                    stringBuider.Append(fieldValue ?? string.Empty);
                }
                else
                {
                    stringBuider.Append(myField[i].Name);
                }
                stringBuider.Append(quote);
                if (i < myField.Length - 1)
                    stringBuider.Append(delimiter);

            }
            return stringBuider.ToString();
        }

        public override string ToString()
        {
            return ToString(MemberToStringOption.Body);
        }
    }
}
