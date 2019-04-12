using System.Collections.Generic;
using Akka.Actor;
using AkkaConsoleBinance.Actors;
using BinanceCryptoCurrency.Domain;
using BinanceCryptoCurrency.Processor;
using Microsoft.AspNetCore.Mvc;

namespace AkkaConsoleBinance.Controllers {

    [Produces("application/json")]
    [Route("api/Ticker")]
    public class TickerController : Controller {
        private readonly IBinanceProcessor _binanceProcessor;
        private readonly ActorSystem _actorSystem;


        public TickerController(IBinanceProcessor binanceProcessor) {
            _binanceProcessor = binanceProcessor;
            _actorSystem = ActorSystem.Create("Ticker");
        }

        public IEnumerable<Ticker> Get() {
            var result = _binanceProcessor.GetTickerLast24Hs();

            var persistActor = _actorSystem.ActorOf<PersistActor>();

            persistActor.Tell(result.Tickers);

            return result.Tickers;
        }
    }
}