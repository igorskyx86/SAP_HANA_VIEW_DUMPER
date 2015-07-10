using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HANA_XML_Dumper.Data
{
    public class FilesData
    {
        public FilesData(string fullPath)
        {
            this.FullPath = fullPath;
            string[] folders = fullPath.Split('\\');
            string path = string.Empty;
            if (folders.Length > 3)
                path = string.Format("{0}.", folders[folders.Length - 3]);
            this.ShortName = string.Format("{0}{1}", path, Path.GetFileNameWithoutExtension(fullPath));
        }
        public string FullPath;
        private string ShortName;
        public override string ToString()
        {
            return ShortName;
        }
    }
}
