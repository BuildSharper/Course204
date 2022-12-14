using System.Configuration;
using BuildSharper.Course204.App.Models;
using Flurl.Http;

var baseUrl = ConfigurationManager.AppSettings["WeatherStackBaseUrl"];
var apiKey = ConfigurationManager.AppSettings["WeatherStackApiKey"];
var done = false;

while (!done)
{
    Console.WriteLine("********************************************");
    Console.WriteLine("BuildSharper.com - Weather");
    Console.WriteLine("********************************************");
    Console.WriteLine("");
    Console.Write("Enter your city: ");
    var query = Console.ReadLine();

    var apiUrl = $"{baseUrl}/current?access_key={apiKey}&units=f&query={query}";

    Console.WriteLine("Fetching weather...");

    var response = await apiUrl.GetJsonAsync<WeatherStackResponse>();

    if (response != null
        && response.Current != null
        && response.Location != null)
    {   
        done = true;
        Console.WriteLine($"{response.Location.Name}, {response.Location.Region} Local Weather: {response.Current.Weather_descriptions.FirstOrDefault()} {response.Current.Temperature} degrees");
    }
    else
    {
        Console.WriteLine("Invalid city name, please try again.");
        Console.ReadKey();
        Console.Clear();
    }
}

Console.WriteLine("");
Console.WriteLine("Press any key to continue.");
Console.ReadKey();