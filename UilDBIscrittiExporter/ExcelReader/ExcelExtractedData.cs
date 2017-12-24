using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UilDBIscrittiExporter.Model;

namespace UilDBIscrittiExporter.ExcelReader
{
    public class ExcelExtractedData
    {
        public IList<Worker> Workers{ get; set; }

        public int FoundFieldsNumber { get; set; }
        public int FoundRecordsNumber { get; set; }

    }
}
