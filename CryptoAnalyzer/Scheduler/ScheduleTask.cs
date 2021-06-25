using CryptoAnalyzer.Interfaces;
using CryptoAnalyzer.Models;
using CryptoAnalyzer.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Scheduler
{
    public class ScheduleTask : ScheduledProcessor
    {
        private readonly ICryptoPairService _cryptoPairService;

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory, ICryptoPairService cryptoPairService) : base(serviceScopeFactory)
        {
            _cryptoPairService = cryptoPairService;
        }

        protected override string Schedule => "*/5 * * * *";

        private static readonly HttpClient client = new HttpClient();
        public override async Task<Task> ProcessInScope(IServiceProvider serviceProvider)
        {
            string responseString = await client.GetStringAsync("https://api.exmo.com/v1.1/ticker");

            JObject json = JObject.Parse(responseString);

            foreach (var prop in json.Properties())
            {
                var pairName = prop.Name;
                var newPrice = prop.First()["last_trade"];
                
                CryptoPair cp = await _cryptoPairService.GetCryptoPairByName(pairName);
                cp.PrevPrice = cp.CurPrice;
                cp.CurPrice = (decimal) newPrice;

                CryptoPair updatedCp = await _cryptoPairService.UpdateCryptoPair(cp.Id, cp);
            }

            return Task.CompletedTask;
        }
    }
}
