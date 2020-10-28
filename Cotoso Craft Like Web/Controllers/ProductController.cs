using Cotoso_Craft_Like_Web.Models;
using Cotoso_Craft_Like_Web.Pages;
using Cotoso_Craft_Like_Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Cotoso_Craft_Like_Web.Controllers
{
    [Route("[Controller]")]
    [ApiController] //标注该控制器为Api控制器，巨有Api控制器特征，如：
    //                 1.只能使用特征路由
    //                  2.参数绑定策略自动推断
    //                   3.自动的模型状态验证
    //                    4.
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private ProductServices _productServices;
        public IEnumerable<Product> Products { get; private set; }

        public ProductController(ILogger<ProductController> logger, ProductServices productServices)
        {
            _logger = logger;
            _productServices = productServices;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
           return  Products = _productServices.GetProducts();
        }

        [HttpGet]
        [Route("View")]   
        public IActionResult GetView([FromServices]ILogger<IndexModel> logger)
        {
            
            IndexModel indexModel = new IndexModel(_productServices, logger);

            return View(indexModel);
        }

        [HttpGet]
        [Route("Rating")]
        public IActionResult PutRating([FromQuery]string productId, [FromQuery] int rating)
        {
            _productServices.AddRating(productId, rating);
            return Ok();
        }
    }
}
