using CryptoAnalyzer.Interfaces;
using CryptoAnalyzer.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Services
{
    public class CryptoPairService : ICryptoPairService
    {
        IGridFSBucket gridFS;   // файловое хранилище
        private readonly IMongoCollection<CryptoPair> _cryptoPairs; // коллекция в базе данных

        public CryptoPairService(IConfiguration config)
        {
            // Connects to MongoDB.
            var client = new MongoClient(config.GetConnectionString("cryptoDb"));
            // Gets the cryptoDb.
            var database = client.GetDatabase("cryptoDb");
            //Fetches the pairs collection.
            _cryptoPairs = database.GetCollection<CryptoPair>("pairs");
        }

        public async Task<List<CryptoPair>> GetCryptoPairs()
        {
            return await _cryptoPairs.Find(cp => true).ToListAsync();
        }
        public async Task<CryptoPair> GetCryptoPair(string id)
        {
            return await _cryptoPairs.Find(cp => cp.Id == id).FirstOrDefaultAsync();
        }
        public async Task<CryptoPair> UpdateCryptoPair(string id, CryptoPair p)
        {
            await _cryptoPairs.ReplaceOneAsync(cp => cp.Id == id, p);
            return p;
        }

        public async Task<CryptoPair> GetCryptoPairByName(string name)
        {
            return await _cryptoPairs.Find(cp => cp.PairName == name).FirstOrDefaultAsync();
        }
    }
}
