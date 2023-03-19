class Program
{
    static async Task Main(string[] args)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/");

            string uri = $"weather?q=Baku&appid=5036d6148020b0e67dbcb0434da77e98";

            HttpResponseMessage response = await client.GetAsync(uri);
            string responseString = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseString);
        }
    }
}