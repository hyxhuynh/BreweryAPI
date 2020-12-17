using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace BreweryAPI
{
    class Program
    {
        static void Main(string[] args)
        {

            // Get user's Brewery API Key
            Console.WriteLine("Type in your BreweryAPI key:");
            var apiKey = Console.ReadLine();
            var client = new HttpClient();
            
            // Random beer
            var randomBeerResponse = client.GetStringAsync($"http://api.brewerydb.com/v2/beer/random/?key={apiKey}").Result;

            // Method 1: using JToken
            JToken rBeer = JToken.Parse(randomBeerResponse);
            var rBeerName = (string)rBeer.SelectToken("data.name");
            var rBeerStyleName = (string)rBeer.SelectToken("data.style.name");
            Console.WriteLine("========================================");
            Console.WriteLine($"Random beer name: {rBeerName} \nStyle Name: {rBeerStyleName}");
            Console.WriteLine("========================================\n");

            // Method 2: deserializing an object (this required a class named RandomBeer to be created)
            //var beer = JsonConvert.DeserializeObject<RandomBeer>(randomBeerResponse);
            //var rBeerName = beer.Data.Name;
            //var rBeerID = beer.Data.ID;
            //Console.WriteLine("========================================");
            //Console.WriteLine($"Random beer name: {rBeerName} \nID: {rBeerID}");
            //Console.WriteLine("========================================\n");

            // Bear list
            var beerResponse = client.GetStringAsync($"http://api.brewerydb.com/v2/beers/?key={apiKey}").Result;
            // Deserialization: turning json file into C# objects
            var beers = JsonConvert.DeserializeObject<BeerList>(beerResponse);
            Console.WriteLine("List of beer names and IDs:");
            Console.WriteLine("------------------------------------------");
            foreach (var b in beers.Data)
            {
                Console.WriteLine($"Name: {b.Name} \nID: {b.ID} \n------------------------------------------");
            }

        }
    }
}
