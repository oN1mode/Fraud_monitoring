using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Fund_monitoring
{
    class ParseXlsToJson
    {
        public static void Start()
        {
            ParseAndSerializationXlsToJson();
        }

        private static void ParseAndSerializationXlsToJson()
        {
            using (var connection = new OleDbConnection(HelperClassWithConst.connectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM [{HelperClassWithConst.sheetName}$]";

                using (var reader = command.ExecuteReader())
                {
                    var query = reader.Cast<DbDataRecord>().Select(row => new
                    {
                        Id = row[0],
                        Bin = row[1],
                        Brand = row[2],
                        BankName = row[3],
                        BinType = row[4],
                        BinLevel = row[5],
                        IsoCountry = row[6],
                        CountryIso = row[7],
                        Country2Iso = row[8],
                        Country3Iso = row[9]
                    });

                    string json = JsonConvert.SerializeObject(query, Formatting.Indented);
                    File.WriteAllText(HelperClassWithConst.pathToJson, json);
                }
            }
        }
    }
}
