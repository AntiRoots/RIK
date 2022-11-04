
using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Globalization;

namespace scraper
{
    class Program 
    {

        static void Main(string[] args)
        {
            var timer = new System.Threading.Timer(
                e => GetHtml(),  
                null, 
                TimeSpan.Zero, 
                TimeSpan.FromHours(2));
            
           
            Console.ReadLine();
        }
           
            private static async void GetHtml()
            {
                 var url = "https://soov.ee/222-arvutid/listings.html";
            var httpClient= new HttpClient();
            var html = await httpClient.GetStringAsync(url);
            var htmlDoc= new HtmlDocument();
            htmlDoc.LoadHtml(html);

            var Products = htmlDoc.DocumentNode.SelectNodes("//h5/a");
            var Price = htmlDoc.DocumentNode.SelectNodes("//h2[contains(@class, 'item-price')]");


           List<string> nimi = new List<string>();

            foreach (var item in Products)
            {
                var name = item.InnerText;
                nimi.Add(name);
            }

            List<string> hind = new List<string>();

            foreach (var item in Price)
            {
                var price = item.InnerText;
                hind.Add(price);
            }

           

            for (int i = 0; i < hind.Count(); i++)
            {
                Console.WriteLine("{0}\t Hind:{1}", nimi[i],  hind[i]);

            }

        }
    }
}

