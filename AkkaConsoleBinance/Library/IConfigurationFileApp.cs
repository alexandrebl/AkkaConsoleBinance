namespace AkkaConsoleBinance.Library {

    public interface IConfigurationFileApp {
        string BinanceUrl { get; }
        string ConnectionString { get; }
    }
}