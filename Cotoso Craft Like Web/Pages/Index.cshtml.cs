using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cotoso_Craft_Like_Web.Models;
using Cotoso_Craft_Like_Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Cotoso_Craft_Like_Web.Pages
{
    public class IndexModel : PageModel
    {
        public string Str = "IndexModel!";
        private readonly ILogger<IndexModel> _logger;
        private ProductServices _productServices;
        public IEnumerable<Product> Products { get; private set; }

        public IndexModel(ProductServices productServices, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _productServices = productServices;
        }

        public void OnGet()
        {
            Products = _productServices.GetProducts();
        }
    }
}
