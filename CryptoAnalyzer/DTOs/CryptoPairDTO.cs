using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAnalyzer.DTOs
{
    public class CryptoPairDTO
    {
        public string PairName { get; set; }
        public decimal PrevPrice { get; set; }
        public decimal CurPrice { get; set; }
        public decimal Percentage { get; set; }
    }
}
