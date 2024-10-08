using CoinExchange.Controller.Interfaces.Services;
using CoinExchange.Model;
using System.Net.Http;
using System.Text.Json;

namespace CoinExchange.Controller.Services.CoinService
{
    internal class CoinService : ICoinExchange
    {
        private readonly HttpClient _http;
        private readonly string baseApiUrl = "https://api.nbp.pl/api/exchangerates/tables/";
        private readonly string format = "?format=json";

        public CoinService(HttpClient http)
        {
            _http = http;
        }

        public Task<int> ExchangeCoins()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Rates>> GetCoins()
        {
            List<Rates> coinList = new List<Rates>();
            var responseA = await _http.
                GetStringAsync(baseApiUrl + "a" + format);
            var responseB = await _http.
                GetStringAsync(baseApiUrl + "b" + format);
            if (string.IsNullOrWhiteSpace(responseA) || string.IsNullOrWhiteSpace(responseB))
            {
                return new List<Rates>();
            }

            var listA = JsonSerializer.Deserialize<List<Exchange>>(responseA);
            var listB = JsonSerializer.Deserialize<List<Exchange>>(responseB);

            if (listA is null || listB is null)
            {
                return new List<Rates>();
            }

            foreach (var coin in listA.First().rates)
            {
                coinList.Add(coin);
            }

            foreach (var coin in listB.First().rates)
            {
                coinList.Add(coin);
            }
            return coinList.OrderBy(x => x.code).ToList();
        }

        public async Task<List<Rates>> GetCurrencyValueByDate(string date)
        {
            List<Rates> currencies = new List<Rates>();
            var responseA = await _http.GetStringAsync(baseApiUrl + "a/" + date + format);
            if (string.IsNullOrWhiteSpace(responseA))
            {
                return new List<Rates>();
            }
            var listA = JsonSerializer.Deserialize<List<Exchange>>(responseA);
            foreach (var currency in listA!.First().rates)
            {
                currencies.Add(currency);
            }
            return currencies.OrderBy(x => x.code).ToList();
        }
    }
}
