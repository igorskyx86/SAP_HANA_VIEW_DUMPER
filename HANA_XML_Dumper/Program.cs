using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using HANA_XML_Dumper.Enums;
using HANA_XML_Dumper.Dumpers;
using ExcelDna.Integration;
using ExcelDna.Integration.CustomUI;
using HANA_XML_Dumper.Renderers;
using System.Runtime.InteropServices;




namespace HANA_XML_Dumper
{


    class Program
    {
        static string inFolder;
        static string outFolder;
        static string mode;
        static Dictionary<string, Type> typeContainer;


        static void Main(string[] args)
        {
            typeContainer = new Dictionary<string, Type>();
            typeContainer.Add("projections", typeof(CA_ProjectionDumper));
            typeContainer.Add("semantics", typeof(CA_SemanticsDumper));
            typeContainer.Add("variables", typeof(CA_VariablesDumper));

            if (args.Length != 2)
            {
                Console.WriteLine("Usage:\n\tHANA_XML_Dumper.exe <src-dir>");
                return;
            }

            inFolder = args[0];
            outFolder = args[1];
            string[] views = Directory.GetFiles(inFolder, "*.calculationview", SearchOption.AllDirectories);
            //StringBuilder sbOutFile = new StringBuilder();
            foreach (string file in views)
            {
                Console.WriteLine(Path.GetFileNameWithoutExtension(file));
                string outFileName = outFolder + @"\" + Path.GetFileNameWithoutExtension(file) + string.Format(".csv");
                StringBuilder sbOutFile = new StringBuilder();

                foreach (KeyValuePair<string, Type> kv in typeContainer)
                {
                    using (XmlTextReader reader = new XmlTextReader(file))
                    {
                        string viewName = Path.GetFileNameWithoutExtension(file);
                        var HANAXMLDumper = (IDumper)Activator.CreateInstance(kv.Value, new object[] { reader });
                        string hanaDump = CSVRenderer.Render(HANAXMLDumper.DumpHANAXML(viewName));
                        string description = HANAXMLDumper.Description;
                        sbOutFile.AppendLine(description);
                        sbOutFile.AppendLine(hanaDump);
                    }
                }
                File.WriteAllText(outFileName, sbOutFile.ToString(), Encoding.ASCII);
            }

            //File.WriteAllText(outFolder+@"\1.csv", sbOutFile.ToString(), Encoding.ASCII);
        }

    }
}
