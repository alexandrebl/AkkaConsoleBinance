using System.Collections.Generic;
using Akka.Actor;
using AkkaConsoleBinance.Library;
using AkkaConsoleBinance.Repository;
using BinanceCryptoCurrency.Domain;

namespace AkkaConsoleBinance.Actors {

    public class PersistActor : ReceiveActor {

        public PersistActor() {
            var tickerRepository = new TickerRepository(new ConfigurationFileApp().ConnectionString);

            Receive<IEnumerable<Ticker>>(tickers => {
                tickerRepository.Persist(tickers);
            });
        }
    }
}