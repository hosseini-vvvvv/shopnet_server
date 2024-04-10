using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/category")]
    public class Category : ControllerBase
    {
        private readonly StoreContext _context;
        public Category(StoreContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<ActionResult> getAllCategory()
        {
            var category = await _context.CategoryOne
            .Include(c => c.CategoryTwo)
            .ThenInclude(c => c.CategoryThree).ToListAsync();

            return Ok(category);
        }

    }
}