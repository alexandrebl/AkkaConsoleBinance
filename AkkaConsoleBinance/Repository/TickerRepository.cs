using System.Collections.Generic;
using System.Security.Authentication;
using Akka.Actor;
using Akka.Event;
using BinanceCryptoCurrency.Domain;
using MongoDB.Driver;

namespace AkkaConsoleBinance.Repository {

    public class TickerRepository {
        private readonly string _connectionString;

        public TickerRepository(string connectionString) {
            _connectionString = connectionString;
        }

        public void Persist(IEnumerable<Ticker> tickers) {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(_connectionString));
            settings.SslSettings =
                new SslSettings { EnabledSslProtocols = SslProtocols.Tls12 };
            var mongoClient = new MongoClient(settings);

            var cryptoCurencyInfoDb = mongoClient.GetDatabase("CryptoCurencyInfo");

            var tickersCl = cryptoCurencyInfoDb.GetCollection<Ticker>("Tickers");

            tickersCl.InsertMany(tickers);
        }
    }
}