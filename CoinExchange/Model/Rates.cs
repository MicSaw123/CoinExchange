namespace CoinExchange.Model
{
    public class Rates
    {
        public string currency { get; set; } = string.Empty;

        public string code { get; set; } = string.Empty;

        public float mid { get; set; }
    }
}
