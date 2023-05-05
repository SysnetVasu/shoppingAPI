using API.Data;
using API.DTOs;
using API.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class CustomerController : BaseApiController
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;

        public CustomerController(StoreContext context, IMapper mapper)
        {
           _context = context;
            _mapper = mapper;
        }
       

        [HttpGet]
        public async Task<ActionResult<CustomerDto>> GetCustomers()
        {
            var customerdetails = await _context.Customers               
                 .ToListAsync();

            var data = _mapper
                .Map<IReadOnlyList<Customer>, IReadOnlyList<CustomerDto>>(customerdetails);

            return Ok(data);
        }

        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> GetCustomer(string id)
        {
            var customerDetail = await _context.Customers                
                .FirstOrDefaultAsync(p => p.Id == id);

            if (customerDetail == null) return NotFound();

            return _mapper.Map<Customer, CustomerDto>(customerDetail);
        }


    }
}
