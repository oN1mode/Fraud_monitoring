using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Fund_monitoring
{
    public class ParseJsonToCards
    {
        public static List<Card> ParseDataJsonToCards ()
        {
            List<Card> cards = new List<Card>();
            string jStr = File.ReadAllText(HelperClassWithConst.pathToJson);

            JArray jObj = JArray.Parse(jStr);

            IList<JToken> results = jObj.Children().ToList();

            foreach (JToken result in results)
            {
                Card card = result.ToObject<Card>();
                cards.Add(card);
            }
            return cards;
        }
    }
}
