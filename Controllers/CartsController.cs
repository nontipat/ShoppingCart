using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Function;

namespace ShoppingCart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly shopping_cartContext _shopping_CartContext;

        public CartsController(shopping_cartContext shopping_CartContext)
        {
            _shopping_CartContext = shopping_CartContext;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var list = GetCarts.Get.GetAll();
            if (list != null)
            {
                return Ok(new ResponseModel().ResponseSuccess("success", list));
            }
            return Ok(new ResponseModel().ResponseError("ไม่พบข้อมูล"));
        }

        [HttpPost]
        public IActionResult AddOrUpdate(int productId)
        {
            var result = GetCarts.Manage.AddOrUpdate(productId);
            if (result)
            {
                return Ok(new ResponseModel().ResponseSuccess());
            }
            return Ok(new ResponseModel().ResponseError());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = GetCarts.Manage.Delete(id);
            if (result)
            {
                return Ok(new ResponseModel().ResponseSuccess("success"));
            }
            return Ok(new ResponseModel().ResponseError("ลบข้อมูลไม่สำเร็จ"));
        }

        [HttpPost("deleteall")]
        public IActionResult DeleteAll(int[] productId)
        {
            int success = 0;
            int fail = 0;
            foreach (int item in productId)  
            {
                var result = GetCarts.Manage.DeleteAll(item);
                if (result)
                {
                    success += 1;
                }
                else 
                {
                    fail += 1;
                }
            }
            var data = new
            {
                success = success,
                fail = fail
            };
            return Ok(new ResponseModel().ResponseSuccess("success", data));

        }

        [HttpPost("checkout")]
        public IActionResult DeleteAllCheckout(int[] productId)
        {
            int success = 0;
            int fail = 0;
            foreach (int item in productId)
            {
                var result = GetCarts.Manage.DeleteAllCheckout(item);
                if (result)
                {
                    success += 1;
                }
                else
                {
                    fail += 1;
                }
            }
            var data = new
            {
                success = success,
                fail = fail
            };
            return Ok(new ResponseModel().ResponseSuccess("success", data));

        }
    }
}
