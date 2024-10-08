using CoinExchange.Model;

namespace CoinExchange.Controller.Interfaces.Services
{
    public interface ICoinExchange
    {
        Task<int> ExchangeCoins();

        Task<List<Rates>> GetCoins();

        Task<List<Rates>> GetCurrencyValueByDate(string date);
    }
}
