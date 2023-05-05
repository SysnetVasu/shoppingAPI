using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
   // [EnableCors("AllowOrigin")]
    public class ProductsController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public ProductsController(StoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            var products = await _context.Products
                 .Include(p => p.Category)
                 .Include(p => p.Unit)
                 .ToListAsync();

            var data = _mapper
                .Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(products);

            return Ok(data);
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(string id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Unit)
                .Where(p => p.Id == id)
                .SingleOrDefaultAsync();

            if (product == null) return NotFound();

            return _mapper.Map<Product, ProductDto>(product);
        }

        [HttpGet("categoryporducts/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetProductByCategory(string categoryId)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .Include(p=>p.Unit)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();

            if (product == null) return NotFound();

            return Ok(product);
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<Category>>> GetCategories()
        {
            return Ok(await _context.Categories.ToListAsync());
        }

    }
}
