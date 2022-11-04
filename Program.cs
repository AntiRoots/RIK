
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

            Console.WriteLine("Kui soovid andmeid kaup24.ee, siis vali 1, \n kui aga soov.ee siis vali 2 \n mõlemad vali 3 \n ning vajuta enter:");
            int tahe = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Sa valisid: " + tahe);
          
            if (tahe==1)
            {
                
            
            var timer = new System.Threading.Timer(
                e => GetKaup(),  
                null, 
                TimeSpan.Zero, 
                TimeSpan.FromHours(2));
            }
            if (tahe==2){
                 var timer = new System.Threading.Timer(
                e => GetSoov(),  
                null, 
                TimeSpan.Zero, 
                TimeSpan.FromHours(2));
            }
            if(tahe==3){
                 var timer = new System.Threading.Timer(
                e => GetKaup(),  
                null, 
                TimeSpan.Zero, 
                TimeSpan.FromHours(2));
                 var timer2 = new System.Threading.Timer(
                e => GetKaup(),  
                null, 
                TimeSpan.Zero, 
                TimeSpan.FromHours(2));
            }
            
           
           Console.ReadLine();
        }
           
            private static async void GetSoov()
            {
                    var url = "https://soov.ee/222-arvutid/tuup-m%25c3%25bc%25c3%25bca/listings.html";
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
                        Console.WriteLine("{0}\t\n Hind:{1}", nimi[i],  hind[i]);

                    }

             }
             private static async void GetKaup()
            {
                    var url = "https://kaup24.ee/et/arvutid-ja-it-tehnika/sulearvutid-ja-tarvikud/sulearvutid";
                    var httpClient= new HttpClient();
                    var html = await httpClient.GetStringAsync(url);
                    var htmlDoc= new HtmlDocument();
                    htmlDoc.LoadHtml(html);

                    var Products = htmlDoc.DocumentNode.SelectNodes("//p[contains(@class, 'product-name')]");
                    var Price = htmlDoc.DocumentNode.SelectNodes("//span[contains(@class, 'price notranslate')]");


                List<string> nimi2 = new List<string>();

                    foreach (var item in Products)
                    {
                        var name = item.InnerText.TrimStart(new char[] {'\n'} );
                        nimi2.Add(name);
                    }

                    List<string> hind2 = new List<string>();

                    foreach (var item in Price)
                    {
                        var price = item.InnerText.TrimStart( new char[] {'\n'});
                        hind2.Add(price);
                    }

                

                    for (int i = 0; i < hind2.Count(); i++)
                    {
                        Console.WriteLine("{0} Hind:{1}", nimi2[i],  hind2[i]);

                    }

             }
    }
}

