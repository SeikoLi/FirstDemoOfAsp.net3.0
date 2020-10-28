using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using Cotoso_Craft_Like_Web.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.CodeAnalysis.Options;
using System.Net.Sockets;

namespace Cotoso_Craft_Like_Web.Services
{
    public class ProductServices
    {
        
        
        public ProductServices(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;  
        }
        public IWebHostEnvironment WebHostEnvironment { get; }

        private string jasonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "products.json"); }
        }

        public IEnumerable<Product> GetProducts()
        {
            
            using var jasonFileReader = File.OpenText(jasonFileName);
            {
                return JsonSerializer.Deserialize<Product[]>(
                    jasonFileReader.ReadToEnd(), 
                    new JsonSerializerOptions { 
                        PropertyNameCaseInsensitive = true 
                    });
            }               
        }

        public void AddRating(string productId, int rating)
        {
            var products = GetProducts();
            var jquery = products.First(x => x.Id == productId);  //推断：这里的jquery是引用类型变量

            if (jquery.Ratings == null)
            {
                jquery.Ratings = new int[] { rating };
            }
            else
            {
                var ratings = jquery.Ratings.ToList();
                ratings.Add(rating);
                jquery.Ratings = ratings.ToArray();
            }
            using (var outputStream = File.OpenWrite(jasonFileName))
            {
                JsonSerializer.Serialize<IEnumerable<Product>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions   //将OutputStream 转化为Utf8JsonWriter
                    {
                        SkipValidation = true,
                        Indented = true
                    }),

                 products);
            }
        }

    }
}
