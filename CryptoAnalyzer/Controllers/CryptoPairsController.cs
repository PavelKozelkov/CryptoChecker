using CryptoAnalyzer.Interfaces;
using CryptoAnalyzer.Models;
using CryptoAnalyzer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoPairsController : ControllerBase
    {
        private readonly ICryptoPairService _cryptoPairService;

        public CryptoPairsController(ICryptoPairService cryptoPairService)
        {
            _cryptoPairService = cryptoPairService;
        }

        //GET: api/Cryptopairs
        [HttpGet]
        public async Task<ActionResult<List<CryptoPair>>> Get()
        {
            return Ok(await _cryptoPairService.GetCryptoPairs());
        }

        // GET: api/Cryptopairs/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<CryptoPair>> Get(string id)
        {
            var cp = await _cryptoPairService.GetCryptoPair(id);
            if (cp == null)
            {
                return NotFound();
            }

            return Ok(cp);
        }

        // PUT: api/Cryptopairs/5
        [HttpPut("{id}")]
        public async Task<ActionResult<CryptoPair>> Put(string id, [FromBody] CryptoPair cp)
        {
            var pair = await _cryptoPairService.GetCryptoPair(id);
            if (pair == null)
            {
                return NotFound();
            }
            cp.Id = pair.Id;

            await _cryptoPairService.UpdateCryptoPair(id, cp);
            return CreatedAtRoute("Get", new { id = cp.Id.ToString() }, cp);
        }
    }
}
