namespace CoinExchange.Model
{
    public class Exchange
    {
        public string table { get; set; }

        public string no { get; set; }

        public string effectiveDate { get; set; }

        public List<Rates> rates { get; set; }
    }
}
