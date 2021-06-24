using CryptoAnalyzer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Interfaces
{
    public interface IDataManager
    {
        Task<List<CryptoPairDTO>> GetDataAsync();
    }
}
