using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UdemyRabbitMQWeb.ExcelCreate.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public ProductController()
        {
            
        }
        public IActionResult Index()
        {
            return View();
        }


        //public IActionResult CreateProductExcel()
        //{
        //    var 
        //}
    }
}
