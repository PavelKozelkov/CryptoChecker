using CryptoAnalyzer.DTOs;
using CryptoAnalyzer.Interfaces;
using CryptoAnalyzer.Models;
using CryptoAnalyzer.Services;
using CryptoAnalyzer.SignalR;
using CryptoAnalyzer.TimerFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {
        private IHubContext<CryptoPairHub> _hub;
        private readonly IDataManager _dataManager;

        public CryptoController(IHubContext<CryptoPairHub> hub, IDataManager dataManager)
        {
            _hub = hub;
            _dataManager = dataManager;
        }

        public IActionResult GetAsync()
        {
            var timerManager = new TimerManager(async () => await _hub.Clients.All.SendAsync("transfercryptodata", await _dataManager.GetDataAsync()));
            
            return Ok(new { Message = "Request completed" });
        }
    }
}
