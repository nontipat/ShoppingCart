using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Function;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly shopping_cartContext _shopping_CartContext;

        public ProductsController(shopping_cartContext shopping_CartContext)
        {
            _shopping_CartContext = shopping_CartContext;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var list = GetProducts.Get.GetAll();
            if (list != null)
            {
                return Ok(new ResponseModel().ResponseSuccess("success", list));
            }
            return Ok(new ResponseModel().ResponseError("ไม่พบข้อมูล"));
        }
    }
}
