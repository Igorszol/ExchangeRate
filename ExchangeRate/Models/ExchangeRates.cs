using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ExchangeRate.Models
{
    public class ExchangeRates
    {
        public List<Rate> usdRates { get; set; }
        public List<Rate> eurRates { get; set; }

        public double usdDiff { get; set; }
        public double eurDiff { get; set; }
  
    }
}
