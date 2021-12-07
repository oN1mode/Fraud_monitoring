using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Collections;

namespace Fund_monitoring
{
    public class Cards
    {
        public List<Card> DataCards { get; set; }

        public Cards()
        {
            DataCards = ParseXlsToJson.ParseDataJsonToCards();
        }

    }
}
