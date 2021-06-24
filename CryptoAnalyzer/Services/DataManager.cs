using CryptoAnalyzer.DTOs;
using CryptoAnalyzer.Interfaces;
using CryptoAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Services
{
    public class DataManager : IDataManager
    {
        private readonly ICryptoPairService _cryptoPairService;

        public DataManager(ICryptoPairService cryptoPairService)
        {
            _cryptoPairService = cryptoPairService;
        }

        public async Task<List<CryptoPairDTO>> GetDataAsync()
        {
            List<CryptoPairDTO> resultDtoList = new List<CryptoPairDTO>();

            List<CryptoPair> cryptoPairs = await _cryptoPairService.GetCryptoPairs();
            foreach (CryptoPair cp in cryptoPairs)
            {
                CryptoPairDTO cpDto = new CryptoPairDTO();
                cpDto.PairName = cp.PairName;
                cpDto.PrevPrice = cp.PrevPrice;
                cpDto.CurPrice = cp.CurPrice;
                cpDto.Percentage = ((cp.CurPrice / cp.PrevPrice) - 1) * 100;

                resultDtoList.Add(cpDto);
            }

            return resultDtoList;
        }
    }
}
