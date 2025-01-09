using ProductUploader;
using System.Text;
using System.Text.Json;

class Program
{
    // HttpClient is intended to be reused throughout the application's lifecycle.
    // This is best practice to avoid socket exhaustion.
    private static readonly HttpClient client = new();

    static async Task Main(string[] args)
    {
        // Define the API URL you want to fetch data from
        string getUrl = "https://fakestoreapi.com/products"; 
        string postUrl = "https://localhost:7181/api/product/batch";
        // Call the async method to fetch data
        var products = await GetApiDataAsync(getUrl);
        if (products != null) await SendPostRequestAsync(postUrl, products);
    }



    private static async Task SendPostRequestAsync(string url, List<Product> products)
    {
        try
        {
            // Serialize the list of posts into JSON
            string jsonContent = JsonSerializer.Serialize(products);

            // Create HttpContent to send as the body of the POST request
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Send the POST request
            HttpResponseMessage response = await client.PostAsync(url, content);

            // Check if the response is successful
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Data successfully posted.");
            }
            else
            {
                Console.WriteLine($"Failed to post data. Status code: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions that may occur
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    // Async method to get data from the API
    private static async Task<List<Product>?> GetApiDataAsync(string apiUrl)
    {
        try
        {
            // Send GET request to the API
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true  // This allows case-insensitive property matching
            };

            // Check if the response is successful
            response.EnsureSuccessStatusCode();

            // Read the response body as a string
            string responseBody = await response.Content.ReadAsStringAsync();

            // Deserialize the JSON response into a list of Post objects
            var products = JsonSerializer.Deserialize<List<Product>>(responseBody, options);

            return products;

        }
        catch (HttpRequestException e)
        {
            // Handle errors in case of failure
            Console.WriteLine($"Request error: {e.Message}");
        }
        return null;
    }
}

