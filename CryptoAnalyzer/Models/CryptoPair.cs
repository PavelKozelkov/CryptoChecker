using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Models
{
    public class CryptoPair
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [Display(Name = "PairName")]
        public string PairName { get; set; }
        [Display(Name = "PrevPrice")]
        public decimal PrevPrice { get; set; }
        [Display(Name = "CurPrice")]
        public decimal CurPrice { get; set; }
    }
}
