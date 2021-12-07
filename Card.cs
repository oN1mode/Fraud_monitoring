using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Fund_monitoring
{
    public class Card
    {
        #region Properties
        public int Id { get; set; }

        public int Bin { get; set; }

        public string Brand { get; set; }

        public string BankName { get; set; }

        public string BinType { get; set; }

        public string BinLevel { get; set; }

        public string IsoCountry { get; set; }

        public string CountryIso { get; set; }

        public string Country2Iso { get; set; }

        public int Country3Iso { get; set; }
        #endregion


        public Card(int id, int bin, string brand, string bankName, string binType, string binLevel, string isoCountry, string countryIso, string country2Iso, int country3Iso)
        {
            Id = id;
            Bin = bin;
            Brand = brand;
            BankName = bankName;
            BinType = binType;
            BinLevel = binLevel;
            IsoCountry = isoCountry;
            CountryIso = countryIso;
            Country2Iso = country2Iso;
            Country3Iso = country3Iso;
        }
    }
}
