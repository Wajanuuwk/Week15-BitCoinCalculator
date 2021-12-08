using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace BitcoinCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            BitcoinRate currentBitcoin = GetRates();
            Console.WriteLine("enter the amount of bitcoins:");
            float userCoins = float.Parse(Console.ReadLine());
            Console.WriteLine("select currency: USD/EUR/GBP");
            string userCurrency = Console.ReadLine().ToUpper();

            float currentCoinRate = 0;
            string currencycode = "";

            if(userCurrency == "EUR")
            {
                currentCoinRate = currentBitcoin.bpi.EUR.rate_float;
                currencyCode = currentBitcoin.bpi.EUR.code;
            }else if(userCurrency == "USD")
            {
                currentCoinRate = currentBitcoin.bpi.USD.rate_float;
                currencyCode = currentBitcoin.bpi.USD.code;
            }
            else if (userCurrency == "GBP")
            {
                currentCoinRate = currentBitcoin.bpi.GBP.rate_float;
                currencyCode = currentBitcoin.bpi.GBP.code;
            }

            float result = userCoins * currentCoinRate;

            Console.WriteLine($"Your bitcoins are {result} {currencyCode} worth. ");

            
            
            
            //Console.WriteLine($"current rate: {currentBitcoin.bpi.USD.code} {currentBitcoin.bpi.USD.rate_float}");
            Console.WriteLine($"{currentBitcoin.disclaimer}");



        }

        public static BitcoinRate GetRates()
        {
            string url = "https://api.coindesk.com/v1/bpi/currentprice.json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();

            BitcoinRate bitcoinData;

            using(var responseReader = new StreamReader(webStream))
            {
                var response = responseReader.ReadToEnd();
                bitcoinData = JsonConvert.DeserializeObject<BitcoinRate>(response);
            }

            return bitcoinData;




        }
    }
}
