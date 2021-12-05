using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fund_monitoring
{
    class HelperClassWithConst
    {
        public static readonly string pathToXls = "bins.xls";
        public static readonly string sheetName = "Grid Results";
        public static readonly string pathToJson = "cards.json";

        public static readonly string connectionString = $@"
                Provider=Microsoft.ACE.OLEDB.12.0;
                Data Source={pathToXls};
                Extended Properties=""Excel 12.0 Xml;HDR=YES""";
    }
}
