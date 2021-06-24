using CryptoAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Interfaces
{
    public interface ICryptoPairService
    {
        Task<List<CryptoPair>> GetCryptoPairs();
        Task<CryptoPair> GetCryptoPair(string id);
        Task<CryptoPair> UpdateCryptoPair(string id, CryptoPair p);
        Task<CryptoPair> GetCryptoPairByName(string name);
    }
}
