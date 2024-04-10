using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductController : ControllerBase
    {
        private readonly StoreContext _context;
        public ProductController(StoreContext context)
        {
            _context = context;
        }


        [HttpGet("product")]
        public ActionResult<List<Product>> GetProducts()
        {
            return _context.Products.ToList();
        }

        [HttpGet("product/{id}")]
        public ActionResult<Product> GetProductId(int id)
        {
            var product = _context.Products.Find(id);

            if (product == null)
            {
                return NotFound("Product Not found");
            }

            return product;
        }
    }


}


























//    [HttpGet("{id}")]
//         public ActionResult<Product> GetProductsData([FromQuery] NewProduct model)
//         {
//             return Ok(model);
//         }
//     }
// }
// public class NewProduct
// {
//     [Required]
//     public string Time { get; set; }
//     public string Name { get; set; }
//     [Required]
//     public decimal Price { get; set; }

// }