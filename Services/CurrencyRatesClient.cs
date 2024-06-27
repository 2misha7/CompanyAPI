using System.Net;
using System.Text.Json;

namespace ApbdProject.Services;

public class CurrencyRatesClient
{
    private static readonly WebClient client = new WebClient();
    public decimal GetExchangeRate(string table, string code)
    {
        string url = $"http://api.nbp.pl/api/exchangerates/rates/{table}/{code}/";
        client.Headers.Add(HttpRequestHeader.Accept, "application/json");

        string response = client.DownloadString(url);
        
        using (JsonDocument doc = JsonDocument.Parse(response))
        {
            JsonElement root = doc.RootElement;
            JsonElement rates = root.GetProperty("rates");
            JsonElement firstRate = rates[0];
            return firstRate.GetProperty("mid").GetDecimal();
        }
    }
}