using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class UserCartDto
    {
        [Required]
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public List<CartItemDto> Items { get; set; }       
    }
}
